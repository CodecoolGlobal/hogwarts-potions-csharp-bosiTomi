// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



let loginButton = document.querySelector('#login');
let registerButton = document.querySelector('#register');
let loginForm = document.querySelector('#login-form');
let registerForm = document.querySelector('#register-form');
let warning = document.querySelector('#warning');
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