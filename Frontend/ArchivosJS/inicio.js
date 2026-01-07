var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
const endpoint = "https://localhost:7118";
import { verificarToken, TipoUsuario } from "./Cuentas.js";
import { mostrarClanes, realizarFiltro, tiposVampiros } from "./Vampiro.js";
//Evento para cuando la página esté cargada
$(document).ready(function () {
    mostrarClanes(); //Se llama al método para mostrar los clanes de vampiro en el menú desplegable
    let rol = verificarToken(); //Se verifica si hay un token en el localStorage
});
//Evento que toma el valor del input de búsqueda y llama a la función para filtrar los clanes
$(".input_clanes").on("input", function () {
    console.log("Input detectado");
    let nombreClan = String($(".input_clanes").val() || ""); //Tomar nombre del input de búsqueda
    realizarFiltro(tiposVampiros, nombreClan);
});
//Evento para ocultar el formulario de login al abrir el modal de registro
$("#exampleModal").on("show.bs.modal", function () {
    $("#login").attr("style", "display: none !important");
});
//Evento para mostrar el formulario de login al cerrar el modal de registro
$("#exampleModal").on("hidden.bs.modal", function () {
    $("#login").show();
});
//Evento para cerrar sesión
$("#cerrar-sesion").on("click", function () {
    localStorage.removeItem("token");
    window.location.reload();
});
//Método para añadir el token al localStorage
function AñadirTokenLocalStorage(token) {
    localStorage.setItem("token", token);
}
//Formulario para cuando envíen el registro de un nuevo usuario
$("#formulario_registro").on("submit", function (event) {
    return __awaiter(this, void 0, void 0, function* () {
        event.preventDefault(); // Evitar el envío del formulario
        console.log("Entrando al submit del formulario de registro");
        try {
            // Obtener los valores de los campos del formulario
            const username = $("#username").val();
            const password = $("#password").val();
            const passwordConfirm = $("#confirmPassword").val();
            let respuesta = yield $.ajax({
                url: `${endpoint}/api/Usuarios/RegistrarUsuario`,
                method: "POST",
                contentType: "application/json",
                data: JSON.stringify({
                    "nombre_Usuario": username,
                    "tipo_Usuario": TipoUsuario.Jugador,
                    "contraseña": password,
                    "contraseñaConfirmacion": passwordConfirm
                }),
            });
            AñadirTokenLocalStorage(respuesta.token);
            window.location.reload();
        }
        catch (error) {
            // Casteamos el error a JQuery.jqXHR para tener autocompletado
            const err = error;
            console.error("Código de estado:", err.status); // Ejemplo: 400
            if (err.status === 400) {
                // El BadRequest de .NET suele venir en 'responseJSON'
                // Lo tipamos como 'any' o un record para leer los mensajes
                const detallesError = err.responseJSON;
                console.log("Mensaje exacto del servidor:", detallesError);
                // Si quieres acceder a los errores de validación específicos de ASP.NET Core:
                if (detallesError && detallesError.errors) {
                    console.table(detallesError.errors);
                }
            }
            else {
                console.error("Error no controlado:", err.responseText);
            }
        }
    });
});
//Formulario para cuando envíen el login de un usuario
$("#formulario_login").on("submit", function (event) {
    return __awaiter(this, void 0, void 0, function* () {
        event.preventDefault(); // Evitar el envío del formulario
        console.log("Entrando al submit del formulario de login");
        try {
            // Obtener los valores de los campos del formulario
            const username = $("#nombre_usuario").val();
            const password = $("#contraseña").val();
            let respuesta = yield $.ajax({
                url: `${endpoint}/api/Usuarios/Login`,
                method: "POST",
                contentType: "application/json",
                data: JSON.stringify({
                    "email": username,
                    "password": password
                }),
            });
            console.log("Respuesta del servidor:", respuesta);
            AñadirTokenLocalStorage(respuesta.token);
            window.location.reload();
        }
        catch (error) {
            // Casteamos el error a JQuery.jqXHR para tener autocompletado
            const err = error;
            console.error("Código de estado:", err.status); // Ejemplo: 400
            if (err.status === 400) {
                // El BadRequest de .NET suele venir en 'responseJSON'
                // Lo tipamos como 'any' o un record para leer los mensajes
                const detallesError = err.responseJSON;
                console.log("Mensaje exacto del servidor:", detallesError);
                // Si quieres acceder a los errores de validación específicos de ASP.NET Core:
                if (detallesError && detallesError.errors) {
                    console.table(detallesError.errors);
                }
            }
            else {
                console.error("Error no controlado:", err.responseText);
            }
        }
    });
});
