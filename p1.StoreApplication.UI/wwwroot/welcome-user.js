const welcomespan = document.getElementById("welcomespan")

if (!sessionStorage.getItem("user")) {
    location.href = "index.html";
}
else {
    let user = sessionStorage.getItem("user");
    let user2 = JSON.parse(user);
    welcomespan.innerHTML = user2.firstName + " " + user2.lastName;
}

(function () {
	fetch("store/Storelist")
		.then(res => res.json())
		.then(data => {
			console.log(data);
			for (let x = 0; x < data.length; x++) {
				let option = document.createElement("option");
				option.innerHTML = `${data[x].name}: ${data[x].city}, ${data[x].state}`;
				option.value = data[x].storeId;
				document.getElementById('storelist').appendChild(option);
			}
		});
})();

const storebutton = document.querySelector("#storebutton");

storebutton.addEventListener("click", (e) => {
    const storeId = document.getElementById("storelist").value;

    fetch(`store/selected/${storeId}`)
        .then(res => {
            if (!res.ok) {
                console.log("unable to select the store");
                throw new Error(`Network response was not ok (${res.status}`);
            }
            return res.json();
        })
        .then(data => {
            console.log(data);
            sessionStorage.setItem('store', JSON.stringify(data));
            let store = sessionStorage.getItem('store');
            console.log(store);
            location.href = "shopping.html";
        })
        .catch(err => console.log(`There was an error ${err}`));
});

const orderbutton = document.querySelector("#orderbutton");

orderbutton.addEventListener("click", (e) => {
    location.href = "list-of-orders.html";
});

const logoutbutton = document.querySelector("#logoutbutton");

logoutbutton.addEventListener("click", (e) => {
    sessionStorage.removeItem("user");
    location.reload();
});