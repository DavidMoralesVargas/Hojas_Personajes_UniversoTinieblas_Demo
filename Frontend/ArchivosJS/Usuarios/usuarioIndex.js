const endpoint = "https://localhost:7118";
import { verificarToken } from "../Cuentas.js";
import { mostrarClanes } from "../Vampiro.js";
//Evento para cerrar sesión
$("#cerrar-sesion").on("click", function () {
    localStorage.removeItem("token");
    window.location.href = "/Frontend/Vistas/index.html";
});
$(document).ready(function () {
    mostrarClanes(); //Se llama al método para mostrar los clanes de vampiro en el menú desplegable
    let token = verificarToken(); //Se verifica si hay un token en el localStorage
    console.log(token);
    if (token == undefined || token == null || token == "") {
        window.location.href = "/Frontend/Vistas/index.html";
    }
});
