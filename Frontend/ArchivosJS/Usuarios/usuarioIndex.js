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
import { verificarToken } from "../Cuentas.js";
import { Tipo_Usuario } from "../interfacesEntidades.js";
import { mostrarClanes, realizarFiltro, tiposVampiros } from "../Vampiro.js";
let activo = true;
//Evento para cerrar sesión
$("#cerrar-sesion").on("click", function () {
    localStorage.removeItem("token");
    window.location.href = "/Frontend/Vistas/index.html";
});
//Evento que toma el valor del input de búsqueda y llama a la función para filtrar los clanes
$(".input_clanes").on("input", function () {
    console.log("Input detectado");
    let nombreClan = String($(".input_clanes").val() || ""); //Tomar nombre del input de búsqueda
    realizarFiltro(tiposVampiros, nombreClan);
});
$(document).ready(function () {
    return __awaiter(this, void 0, void 0, function* () {
        mostrarClanes(); //Se llama al método para mostrar los clanes de vampiro en el menú desplegable
        let token = verificarToken(); //Se verifica si hay un token en el localStorage
        if (token == undefined || token == null || token == "") {
            window.location.href = "/Frontend/Vistas/index.html";
        }
        yield llenarTabla();
    });
});
function llenarTabla() {
    return __awaiter(this, void 0, void 0, function* () {
        let filas = "";
        let usuarios = yield $.ajax({
            url: `${endpoint}/api/Usuarios/listarUsuarios/${activo}`,
            method: "GET",
            headers: {
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            dataType: "json"
        });
        usuarios.forEach(usuarios => {
            filas += `<tr>
                    <td>${usuarios.nombre_Usuario}</td>
                    <td>${Tipo_Usuario[usuarios.tipo_Usuario]}</td>
                    <td><img style="width: 60px;" src="../../Images/SinFotoVampiro.png" ></td>
                    <td>${usuarios.cronicas.length}</td>
                    <td>${usuarios.hojas_Personaje.length}</td>
                    <td>
                        <buttom class="btn ${activo ? "btn-warning" : "btn-info"} btn-sm ${activo ? "desactivar_registro" : "activar_registro"}" data-id="${usuarios.id}">
                            <i class="${activo ? "bi bi-lightbulb" : "bi bi-lightbulb-fill"}"></i> 
                            ${activo ? "Desactivar" : "Activar"}
                        </buttom>
                    </td>
                </tr>`;
        });
        $("#cuerpo_tabla").empty();
        $("#cuerpo_tabla").append(filas);
    });
}
$('input[name="estado"]').on("change", function (e) {
    return __awaiter(this, void 0, void 0, function* () {
        activo = e.target.id == "activos";
        yield llenarTabla();
    });
});
$("#cuerpo_tabla").on("click", ".activar_registro", function () {
    let idUsuario = String($(this).data("id"));
    cambiarEstadoUsuario(idUsuario);
});
$("#cuerpo_tabla").on("click", ".desactivar_registro", function () {
    let idUsuario = String($(this).data("id"));
    cambiarEstadoUsuario(idUsuario);
});
function cambiarEstadoUsuario(idUsu) {
    return __awaiter(this, void 0, void 0, function* () {
        const respuesta = yield Swal.fire({
            icon: "warning", // Cambiado a warning porque es una acción de eliminar
            title: "¡Atención!",
            text: "¿Estás seguro de cambiar el estado del usuario?",
            showCancelButton: true, // Corregido: Button con 'n'
            confirmButtonText: '¡Modificar!',
            confirmButtonColor: '#00c3ff', // Rojo
            cancelButtonText: "Cancelar", // Corregido: Button con 'n'
            cancelButtonColor: '#ff0000' // Un azul o gris suele quedar mejor para cancelar
        });
        if (!respuesta.isConfirmed) {
            return;
        }
        try {
            yield $.ajax({
                url: `${endpoint}/api/Usuarios`,
                method: "PUT",
                contentType: "application/json",
                headers: {
                    "Authorization": `Bearer ${localStorage.getItem("token")}`
                },
                data: JSON.stringify({
                    "activo": !activo,
                    "usuario": idUsu
                }),
                dataType: "json"
            });
            Swal.fire({
                icon: "success",
                title: "¡Éxito!",
                text: `Estado de usuario cambiado con éxito`,
                confirmButtonText: 'Aceptar',
                confirmButtonColor: '#3085d6',
                toast: true,
                position: "bottom-start",
                timer: 3000
            });
            $("#cuerpo_tabla").empty();
            yield llenarTabla();
        }
        catch (error) {
            Swal.fire({
                icon: "warning",
                title: "!Algo mal ocurrió!",
                text: `${error.responseText}`,
                confirmButtonText: 'Aceptar',
                confirmButtonColor: '#ff0000ff',
                toast: true,
                position: "bottom-start",
                timer: 3000
            });
        }
    });
}
//Formulario para cuando envíen el registro de un nuevo usuario
$("#crear_vampiro").on("click", function () {
    return __awaiter(this, void 0, void 0, function* () {
        const username = String($("#username").val());
        const password = String($("#password").val());
        const passwordConfirm = String($("#confirmPassword").val());
        const tipoUsuario = String($("#tipo_usuario").val()) === "dungeon_master" ? Tipo_Usuario.Dungeon_Master : Tipo_Usuario.Jugador;
        try {
            yield $.ajax({
                url: `${endpoint}/api/Usuarios/RegistrarUsuario`,
                method: "POST",
                contentType: "application/json",
                data: JSON.stringify({
                    "nombre_Usuario": username,
                    "tipo_Usuario": tipoUsuario,
                    "contraseña": password,
                    "contraseñaConfirmacion": passwordConfirm
                }),
            });
            yield llenarTabla();
            Swal.fire({
                icon: "success",
                title: "¡Éxito!",
                text: "Usuario creado con éxito",
                confirmButtonText: 'Aceptar',
                confirmButtonColor: '#3085d6',
                toast: true,
                position: "bottom-start",
                timer: 3000
            });
        }
        catch (error) {
            Swal.fire({
                icon: "warning",
                title: "!Algo mal ocurrió!",
                text: `${error.responseText}`,
                confirmButtonText: 'Aceptar',
                confirmButtonColor: '#ff0000ff',
                toast: true,
                position: "bottom-start",
                timer: 3000
            });
        }
        const modalElement = document.getElementById('exampleModal');
        if (modalElement) {
            const bootstrapAny = window.bootstrap;
            const modal = bootstrapAny.Modal.getInstance(modalElement) ||
                new bootstrapAny.Modal(modalElement);
            modal.hide();
        }
    });
});
$("#exampleModal").on("hidden.bs.modal", function () {
    $("#username").val("");
    $("#password").val("");
    $("#confirmPassword").val("");
    $("#tipo_usuario").val("");
});
