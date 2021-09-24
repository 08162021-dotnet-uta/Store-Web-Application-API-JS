const button = document.querySelector("#registerButton");

button.addEventListener("click", (e) => {
    e.preventDefault();
    const fname = document.getElementById("firstName").value;
    const lname = document.getElementById("lastName").value;
    const userData = { CustomerId: -1, FirstName: fname, LastName: lname }
    fetch(`customer/register`, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(userData)
    })
        .then(res => {
            if (!res.ok) {
                console.log("unable to register the user");
                throw new Error(`Network response was not ok (${res.status}`);
            }
            return res.json();
        })
        .then((data) => {
            console.log(data);
            sessionStorage.setItem('user', JSON.stringify(data));
            location.href = "welcome-user.html";
        })
        .catch(err => console.log(`Failed to set page: ${err}`));
});