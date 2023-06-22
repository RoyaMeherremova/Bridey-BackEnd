$(document).ready(function () {
    const togglePassword = document.querySelector("#login .area-password .eyes");
    const password = document.querySelector("#login .area-password input");

    togglePassword.addEventListener("click", function () {
        const type = password.getAttribute("type") === "password" ? "text" : "password";
        password.setAttribute("type", type);

        this.classList.toggle("bi-eye");
    });

    // prevent form submit
    const form = document.querySelector("form");
    form.addEventListener('submit', function (e) {
        e.preventDefault();
    });
    
})