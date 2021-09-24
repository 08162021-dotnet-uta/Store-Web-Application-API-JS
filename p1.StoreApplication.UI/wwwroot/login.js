const button = document.querySelector("#loginbutton");

button.addEventListener("click", (e) => {
    const fname = document.getElementById("firstName").value;
    const lname = document.getElementById("lastName").value;

    fetch(`customer/login/${fname}/${lname}`)
        .then(res => {
            if (!res.ok) {
                console.log("unable to login to the user");
                throw new Error(`Network response was not ok (${res.status}`);
            }
            return res.json();
        })
        .then(data => {
            console.log(data);
            sessionStorage.setItem('user', JSON.stringify(data));
            let user = sessionStorage.getItem('user');
            console.log(user);
            location.href = "welcome-user.html";
        })
        .catch(err => console.log(`There was an error ${err}`));
});