(function () {
	const user = JSON.parse(sessionStorage.getItem("user"));
	fetch(`order/customer/${user.firstName}/${user.lastName}/Orderlist`)
		.then(res => {
			if (!res.ok) {
				console.log("unable to process the order");
				throw new Error(`Network response was not ok (${res.status})`);
			}
			return res.json();
		})
		.then(data => {
			console.log(data)
			const loo = document.querySelector('.listoforders');
			for (let x = 0; x < data.length; x++) {
				loo.innerHTML += `<p>Order ID: ${data[x].orderId} | Store ID: ${data[x].storeId} | Order Date: ${data[x].orderDate}</p>`;
			}
		})
		.catch(err => {
			const loo = document.querySelector('.listoforders');
			loo.innerHTML = `There are no orders for ${user.firstName} ${user.LastName}.`;
			console.log(`There was an error ${err}`);
		});
})();

function goBack() {
	location.href = "welcome-user.html";
}