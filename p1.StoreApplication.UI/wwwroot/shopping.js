const storespan = document.getElementById("storespan")

if (!sessionStorage.getItem("store")) {
	location.href = "welcome-user.html";
}
else {
	let store = sessionStorage.getItem("store");
	let store2 = JSON.parse(store);
	storespan.innerHTML = store2.name;
}

(function () {
	let store = sessionStorage.getItem('store');
	let store2 = JSON.parse(store);
	fetch(`product/${store2.storeId}/Productlist`)
		.then(res => res.json())
		.then(data => {
			console.log(data)
			const productList = document.querySelector('#productlist');
			for (let x = 0; x < data.length; x++) {
				productList.innerHTML += `<p>${data[x].name}: ${data[x].description} | $${data[x].price} <button onclick=addToCart(${data[x].productId})>Add to Cart</button></p>`;
			}
		});
})();

function addToCart(id) {
	const cart = sessionStorage.getItem('cart') ? JSON.parse(sessionStorage.getItem('cart')) || [] : [];
	fetch(`product/selected/${id}`)
		.then(res => res.json())
		.then(data => {
			console.log(data);
			cart.push(data);
			console.log(cart);
			sessionStorage.setItem('cart', JSON.stringify(cart));
			alert("Item added to cart!");
		});
}

function viewCart() {
	location.href = "cart.html";
}

function leaveStore() {
	sessionStorage.removeItem('cart');
	sessionStorage.removeItem('store');
	location.href = "welcome-user.html";
}