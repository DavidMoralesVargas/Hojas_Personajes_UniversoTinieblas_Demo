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
let idEditar;
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
        yield listarDisciplina();
        if (sessionStorage.getItem("mostrarAlerta") === "true") {
            Swal.fire({
                icon: "success",
                title: "¡Éxito!",
                text: "Cambios en vampiro registrado con éxito",
                confirmButtonText: 'Aceptar',
                confirmButtonColor: '#3085d6',
                toast: true,
                position: "bottom-start",
                timer: 3000
            });
            sessionStorage.removeItem("mostrarAlerta");
        }
    });
});
//Carga la lista de disciplinas
function listarDisciplina() {
    return __awaiter(this, void 0, void 0, function* () {
        let filas = "";
        let resultado = yield $.ajax({
            url: `${endpoint}/api/Disciplinas`,
            method: "GET",
            headers: {
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            dataType: "json"
        });
        resultado.forEach(fila => {
            filas += `<tr>
                    <td>${fila.nombre_Disciplina}</td>
                    <td><a href="../../Vistas/HabilidadesDisciplinas/habilidadesDisciplinas.html?disciplina=${fila.id}">${fila.habilidades_Disciplina.length}</a></td>
                    <td>
                        <buttom data-bs-toggle="modal" data-bs-target="#exampleModal" class="btn btn-warning btn-sm btn-editar"><i class="bi bi-pen"></i> Editar</buttom>
                        <buttom class="btn btn-danger btn-eliminar btn-sm" data-id="${fila.id}"><i class="bi bi-trash"></i> Eliminar</buttom>
                    </td>
                    <td hidden>${fila.id}</td>
                </tr>`;
        });
        $("#cuerpo_tabla").append(filas);
    });
}
//Evento para el botón eliminar disciplinas
$("#cuerpo_tabla").on("click", ".btn-eliminar", function () {
    return __awaiter(this, void 0, void 0, function* () {
        let id = $(this).data("id");
        const respuesta = yield Swal.fire({
            icon: "warning", // Cambiado a warning porque es una acción de eliminar
            title: "¡Atención!",
            text: "¿Estás seguro de que deseas eliminar este vampiro?",
            showCancelButton: true, // Corregido: Button con 'n'
            confirmButtonText: 'Eliminar',
            confirmButtonColor: '#ff0000', // Rojo
            cancelButtonText: "Cancelar", // Corregido: Button con 'n'
            cancelButtonColor: '#3085d6' // Un azul o gris suele quedar mejor para cancelar
        });
        if (!respuesta.isConfirmed) {
            return;
        }
        try {
            yield $.ajax({
                method: "DELETE",
                dataType: "json",
                headers: {
                    "Authorization": `Bearer ${localStorage.getItem("token")}`
                },
                url: `${endpoint}/api/Disciplinas/${id}`
            });
            $("#cuerpo_tabla").empty();
            yield listarDisciplina();
            Swal.fire({
                icon: "success",
                title: "¡Éxito!",
                text: "Disciplina eliminada con éxito",
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
    });
});
//Evento para abrir modal de edición de registro
$("#cuerpo_tabla").on("click", ".btn-editar", function () {
    return __awaiter(this, void 0, void 0, function* () {
        var _a, _b;
        const fila = String((_a = $(this).closest('tr').find("td")[0]) === null || _a === void 0 ? void 0 : _a.textContent);
        idEditar = Number((_b = $(this).closest('tr').find("td")[3]) === null || _b === void 0 ? void 0 : _b.textContent);
        $("#nombre").val(fila);
    });
});
//Limpiar el input de nombres cuando el modal se cierra
$('#exampleModal').on('hidden.bs.modal', function () {
    $("#nombre").val("");
});
//Evento para editar la disciplina
$("#editar_disciplina").on("click", function () {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            yield $.ajax({
                method: "PUT",
                dataType: "json",
                contentType: "application/json",
                headers: {
                    "Authorization": `Bearer ${localStorage.getItem("token")}`
                },
                url: `${endpoint}/api/Disciplinas`,
                data: JSON.stringify({
                    "id": idEditar,
                    "nombre_Disciplina": $("#nombre").val()
                }),
            });
            Swal.fire({
                icon: "success",
                title: "¡Éxito!",
                text: "¡Registro actualizado con éxito!",
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
        $("#cuerpo_tabla").empty();
        yield listarDisciplina();
        const modalElement = document.getElementById('exampleModal');
        if (modalElement) {
            const bootstrapAny = window.bootstrap;
            const modal = bootstrapAny.Modal.getInstance(modalElement) ||
                new bootstrapAny.Modal(modalElement);
            modal.hide();
        }
    });
});
