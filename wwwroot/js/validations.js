console.log("Validations.js loaded");

// Variable global para controlar el estado del formulario
window.formIsValid = true;

// Función para mostrar mensajes de error
function setErrorText(elementId, message) {
    const element = document.getElementById(elementId);
    if (element) {
        element.innerText = message;
    }
}

// Función para validar un campo individual
function validateField(condition, elementId, errorMessage) {
    if (condition) {
        setErrorText(elementId, "");
    } else {
        setErrorText(elementId, errorMessage);
        window.formIsValid = false;  // Marcar el formulario como inválido si alguna validación falla
    }
}

// Funciones de validación para cada campo
window.validarCedula = function (cedula) {
    validateField(cedula.length === 10 && /^\d+$/.test(cedula), "cedulaError", "El número de cédula debe contener 10 dígitos.");
    console.log("formIsValid:", window.formIsValid);
}

window.validarNombre = function (nombre) {
    validateField(/^[a-zA-Z\s]+$/.test(nombre), "nombreError", "El nombre solo debe contener letras y espacios.");
    console.log("formIsValid:", window.formIsValid);
}

window.validarApellidos = function (apellidos) {
    validateField(/^[a-zA-Z\s]+$/.test(apellidos), "apellidosError", "Los apellidos solo pueden contener letras.");
    console.log("formIsValid:", window.formIsValid);
}

window.validarCorreo = function (correo) {
    const correoRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    validateField(correoRegex.test(correo), "correoError", "Ingrese un correo valido.");
    console.log("formIsValid:", window.formIsValid);
}

window.validarCelular = function (celular) {
    validateField(celular.length === 10 && /^\d+$/.test(celular), "celularError", "El número de celular debe contener 10 dígitos.");
    console.log("formIsValid:", window.formIsValid);
}

window.validarTelefono = function (telefono) {
    validateField(telefono.length >= 7 && /^\d+$/.test(telefono), "telefonoError", "El número de teléfono debe contener solo dígitos.");
    console.log("formIsValid:", window.formIsValid);
}

// Función para validar todos los campos
window.validarTodosLosCampos = function () {
    window.formIsValid = true;  // Reinicia la validez antes de validar todos los campos

    // Llama a todas las funciones de validación con los valores actuales
    validarCedula(document.getElementById('cedula').value);
    validarNombre(document.getElementById('nombre').value);
    validarApellidos(document.getElementById('apellidos').value);
    validarCorreo(document.getElementById('correo').value);
    validarCelular(document.getElementById('celular').value);
    validarTelefono(document.getElementById('telefono').value);

    console.log("Validación completa - formIsValid:", window.formIsValid);
}
