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
import { mostrarClanes, realizarFiltro, tiposVampiros } from "../Vampiro.js";
let tiposDepredador = [];
let idEditar;
let tipoDepredador;
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
        try {
            let respuesta = yield $.ajax({
                type: "GET",
                url: `${endpoint}/api/Tipos_Depredador`,
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem('token')}`
                },
                dataType: "json"
            });
            tiposDepredador = respuesta.resultado;
            console.log(tiposDepredador);
            let tabla = $("#cuerpo_tabla_tipos_depredador");
            tabla.empty();
            tabla.html(tiposDepredador.map(tipo => `
            <tr>
                <td>${tipo.nombre}</td>
                <td>
                    <button class="btn btn-warning me-2 editar" data-id="${tipo.id}" data-evento="editar" data-bs-toggle="modal" data-bs-target="#crearEditarModal"><i class="bi bi-pencil-square"></i> Editar</button>
                    <button class="btn btn-danger" data-id="${tipo.id}"><i class="bi bi-trash"></i> Eliminar</button>
                </td>
            </tr>
        `).join(""));
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
//Evento para crear el título del modal al dar clic en el botón de crear
const botonCrear = document.querySelector(".crear");
botonCrear.addEventListener("click", function () {
    const tituloModal = document.getElementById("TituloModal");
    tituloModal.textContent = "Crear Tipo de Depredador";
    $("#accion").val("crear");
});
//Evento para editar el titulo del modal al dar clic en el botón de editar
$(document).on("click", ".editar", function () {
    return __awaiter(this, void 0, void 0, function* () {
        const tituloModal = document.getElementById("TituloModal");
        tituloModal.textContent = "Editar Tipo de Depredador";
        $("#accion").val("editar");
        idEditar = $(this).data("id");
        yield BuscarTipoDepredadorPorID(idEditar);
        //Llenar los campos del modal con la información del tipo de depredador
        $("#nombre_tipo_depredador").val(tipoDepredador.nombre);
    });
});
function BuscarTipoDepredadorPorID(id) {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            let dato = yield $.ajax({
                type: "GET",
                url: `${endpoint}/api/Tipos_Depredador/${id}`,
                dataType: "json",
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem('token')}`
                }
            });
            tipoDepredador = dato.resultado;
            console.log(tipoDepredador);
        }
        catch (error) {
            console.error(error.responseText);
        }
    });
}
function guardarCambios(accion) {
    return __awaiter(this, void 0, void 0, function* () {
        let crear = accion === "crear";
        try {
            yield $.ajax({
                type: crear ? "POST" : "PUT",
                url: `${endpoint}/api/Tipos_Depredador`,
                data: JSON.stringify({
                    id: crear ? 0 : idEditar,
                    nombre: String($("#nombre_tipo_depredador").val())
                }),
                contentType: "application/json",
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem('token')}`
                }
            });
            const modalElement = document.getElementById('crearEditarModal');
            if (modalElement) {
                const bootstrapAny = window.bootstrap;
                const modal = bootstrapAny.Modal.getInstance(modalElement) ||
                    new bootstrapAny.Modal(modalElement);
                modal.hide();
            }
            Swal.fire({
                icon: "success",
                title: crear
                    ? "¡Tipo de depredador creado exitosamente!"
                    : "¡Tipo de depredador editado exitosamente!",
                toast: true,
                position: "bottom-start",
                timer: 3000,
                confirmButtomText: 'Aceptar',
                confirmButtomColor: '#3085d6'
            });
            $("#cuerpo_tabla_tipos_depredador").empty();
            yield llenarTabla();
        }
        catch (error) {
            console.error(error);
            Swal.fire("Error", "No se pudo guardar", "error");
        }
    });
}
let botonGuardar = document.getElementById("Guardar");
botonGuardar.addEventListener("click", function () {
    let accion = String($("#accion").val());
    guardarCambios(accion);
});
//Modal para limpiar el campo nombre al cerrar el modal
$('#crearEditarModal').on('hidden.bs.modal', function () {
    $("#nombre_tipo_depredador").val("");
});
//Evento para eliminar registro
$(document).on("click", ".btn-danger", function () {
    return __awaiter(this, void 0, void 0, function* () {
        let idEliminar = $(this).data("id");
        console.log("ID a eliminar: " + idEliminar);
        let confirmacion = yield Swal.fire({
            icon: "info",
            title: "Estás seguro de eliminar este tipo de depredador?",
            confirmButtomText: 'Aceptar',
            confirmButtomColor: '#3085d6',
            showCancelButton: true,
            cancelButtonText: 'Cancelar',
            cancelButtonColor: '#ff0000ff',
            confirmButtonText: 'Aceptar',
        });
        if (!confirmacion.isConfirmed) {
            return;
        }
        yield $.ajax({
            type: "DELETE",
            url: `${endpoint}/api/Tipos_Depredador/${idEliminar}`,
            dataType: "json",
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            }
        });
        Swal.fire({
            icon: "success",
            title: "¡Tipo de depredador eliminado exitosamente!",
            toast: true,
            position: "bottom-start",
            timer: 3000,
            confirmButtomText: 'Aceptar',
            confirmButtomColor: '#3085d6'
        });
        $("#cuerpo_tabla_tipos_depredador").empty();
        yield llenarTabla();
    });
});
