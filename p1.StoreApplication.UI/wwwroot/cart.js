const cartElement = document.getElementById("cart")

if (!sessionStorage.getItem("cart")) {
	cartElement.innerHTML = "Your cart is empty.";
}
else {
	let cart = sessionStorage.getItem("cart");
	let data = JSON.parse(cart);
	let sub = 0.00;
	for (let x in data) {
		cartElement.innerHTML += `<p>${data[x].name}: ${data[x].description} | $${data[x].price} <button onclick=removeFromCart(${x})>Remove</button></p>`;
		sub += data[x].price;
	}
	document.getElementById("subtotal").innerHTML = `Subtotal: $${sub.toFixed(2)}`;
}

function removeFromCart(pos) {
	let cart = sessionStorage.getItem("cart");
	let data = JSON.parse(cart);
	data.splice(pos, 1);
	data.length > 0 ? sessionStorage.setItem('cart', JSON.stringify(data)) : sessionStorage.removeItem("cart");
	location.reload();
}

function emptyCart() {
	sessionStorage.removeItem("cart");
	location.reload();
}

function purchase() {
	/* do not puchase if
	 * customer is not defined
	 * store is not defined
	 * cart is empty
	 * cart is too expensive
	 * items are out of stock
	 * cart is too full
	 */
	if (!sessionStorage.getItem("user"))
		alert("ERROR: You are not logged in");
	else if (!sessionStorage.getItem("store"))
		alert("ERROR: No store is defined");
	else if (!sessionStorage.getItem("cart"))
		alert("ERROR: Cart is empty");
	else {
		let cart = sessionStorage.getItem("cart");
		let data = JSON.parse(cart);
		let sub = 0.00;
		for (let x in data)
			sub += data[x].price;
		if (sub >= 500)
			alert("ERROR: Orders are limited to $500.");
		else if (data.length > 50)
			alert("ERROR: Orders are limited to 50 items");
		else {
			const user = JSON.parse(sessionStorage.getItem("user"));
			const store = JSON.parse(sessionStorage.getItem("store"));
			const ordercart = [];
			const ordercartids = [];
			for (let product in data) {
				let productId = data[product].productId;
				if (ordercartids.indexOf(productId) < 0) {
					ordercart.push({ ProductId: productId, Quantity: 1 })
					ordercartids.push(productId)
				}
				else
					ordercart[ordercart.map(function (e) { return e.ProductId; }).indexOf(productId)].Quantity++;
			}
			console.log(ordercart);
			let d = new Date();
			d.toISOString();
			const orderData = { OrderId: -1, CustomerId: user.CustomerId, StoreId: store.StoreId, Date: d }
			fetch(`order/neworder`, {
				method: 'POST',
				headers: {
					'Accept': 'application/json',
					'Content-Type': 'application/json'
				},
				body: JSON.stringify(orderData)
			})
				.then(res => {
					if (!res.ok) {
						console.log("unable to process the order");
						throw new Error(`Network response was not ok (${res.status})`);
					}
					return res.json();
				})
				.then((res) => {
					console.log(res);
				})
				.catch(err => console.log(`Failed to set page: ${err}`));
			fetch(`order/lastorder`)
				.then(res => res.json())
				.then(lo => {
					Fetch(fetch(`orderProduct/makeOrderProduct`, {
						method: 'POST',
						headers: {
							'Accept': 'application/json',
							'Content-Type': 'application/json',
						},
						body: JSON.stringify(ordercart[0])
					})
						.then(res => {
							if (!res.ok) {
								console.log("unable to process the order");
								throw new Error(`Network response was not ok (${res.status})`);
							}
							return res.json();
						})
						.then((res) => {
							console.log(res);
						})
						.catch(err => console.log(`Failed to set page: ${err}`)));
					}
                )
			
			alert("Your order has been processed. Have a nice day!");
        }
    }
}

function goBack() {
	location.href = "shopping.html";
}