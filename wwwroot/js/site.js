// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



let loginButton = document.querySelector('#login');
let registerButton = document.querySelector('#register');
let loginForm = document.querySelector('#login-form');
let registerForm = document.querySelector('#register-form');
let warning = document.querySelector('#warning');

function showIngredients() {
    let ingredientsList = document.querySelector('#ingredients');
    let backToClassButton = document.querySelector('.back-to-class');
    let createButton = document.querySelector('#create');
    if (ingredientsList.style.display === "none") {
        ingredientsList.style.display = "block";
        backToClassButton.style.display = "none";
        createButton.style.display = "none";
        document.getElementById("createDiv").style.zIndex = -999;
    } else {
        ingredientsList.style.display = "none";
        backToClassButton.style.display = "block";
        createButton.style.display = "block";
        document.getElementById("createDiv").style.zIndex = 999;
    }
}

function Alert() {
    if (document.getElementById("create").hasAttribute("disabled")) {
        alert("Select 5 ingredients");
    }
};

function SearchIngredient() {
    var ingredients = document.getElementsByTagName("option");
    var searchText = document.getElementById('searchBar').value.toLowerCase();

    for (var i = 0; i < ingredients.length; i++) {
        if (!ingredients[i].value.toLowerCase().includes(searchText)) {
            ingredients[i].style.display = "none";
        }
        else {
            ingredients[i].style.display = "block";
        };
    }
    return false;
};


loginButton.addEventListener('click', () => {
    registerForm.setAttribute('hidden', 'hidden');
    loginForm.removeAttribute('hidden');
    warning.innerHTML = "";
});
registerButton.addEventListener('click', () => {
    loginForm.setAttribute('hidden', 'hidden');
    registerForm.removeAttribute('hidden');
    warning.innerHTML = "";
});