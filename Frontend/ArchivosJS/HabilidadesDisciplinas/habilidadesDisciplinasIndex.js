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
let habilidadDisciplina;
const params = new URLSearchParams(window.location.search);
const id = params.get('disciplina');
//Evento para cerrar sesión
$("#cerrar-sesion").on("click", function () {
    localStorage.removeItem("token");
    window.location.href = "/Frontend/Vistas/index.html";
});
//Evento que toma el valor del input de búsqueda y llama a la función para filtrar los clanes
$(".input_clanes").on("input", function () {
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
        yield buscarDisciplina();
        yield llenarTabla();
    });
});
function buscarDisciplina() {
    return __awaiter(this, void 0, void 0, function* () {
        let resultado = yield $.ajax({
            url: `${endpoint}/api/Disciplinas/${id}`,
            method: "GET",
            headers: {
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            dataType: "json"
        });
        let disciplina = resultado.resultado;
        $("#titulo").html(disciplina.nombre_Disciplina);
    });
}
function llenarTabla() {
    return __awaiter(this, void 0, void 0, function* () {
        let filas = "";
        let resultado = yield $.ajax({
            url: `${endpoint}/api/HabilidadesDisciplinas/disciplina/${id}`,
            method: "GET",
            headers: {
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            dataType: "json"
        });
        resultado.forEach(fila => {
            filas += `<tr>
                    <td>${fila.nombre_Habilidad}</td>
                    <td>${fila.nivel}</td>
                    <td>${fila.tirada}</td>
                    <td>${fila.enardecimiento === true ? "Si" : "No"}</td>
                    <td>${fila.tiradaEnfrentada || "N/A"}</td>
                    <td>
                        <buttom data-bs-toggle="modal" data-bs-target="#exampleModal" class="btn btn-warning btn-sm btn-editar" data-id="${fila.id}"><i class="bi bi-pen"></i> Editar</buttom>
                        <buttom class="btn btn-danger btn-eliminar btn-sm" data-id="${fila.id}"><i class="bi bi-trash"></i> Eliminar</buttom>
                    </td>
                    <td hidden>${fila.id}</td>
                </tr>`;
        });
        $("#cuerpo_tabla").append(filas);
    });
}
$("#btn-crear").on("click", function () {
    $("#exampleModalLabel").html("Crear nueva habilidad");
    $("#tipo_accion").val("crear");
});
$("#cuerpo_tabla").on("click", ".btn-editar", function (e) {
    $("#exampleModalLabel").html("Editar habilidad de disciplina");
    $("#tipo_accion").val("editar");
    llenarCamposEditar(e.target.dataset.id);
});
$('input[name="tirada_enfrentada"]').on("change", function () {
    const valor = $('input[name="tirada_enfrentada"]:checked').val();
    if (valor == "true") {
        $(".input_enfrentado").prop("hidden", false);
    }
    else {
        $(".input_enfrentado").prop("hidden", true);
        $(".tirada_enfrentada_dato").val("");
    }
});
$("#guardar_cambios").on("click", function () {
    return __awaiter(this, void 0, void 0, function* () {
        let tipo_accion = String($("#tipo_accion").val());
        if (tipo_accion == "crear") {
            yield crearHabilidad();
        }
        else {
            yield EditarHabilidad();
        }
    });
});
$('#exampleModal').on('hidden.bs.modal', function () {
    $(".nombre-habilidad").val("");
    $(".nivel-habilidad").val("");
    $(".dados-habilidades").val("");
    $('input[name="enardecimiento"][value="false"]').prop("checked", true);
    $('input[name="tirada_enfrentada"][value="false"]').prop("checked", true);
    $(".tirada_enfrentada_dato").val("");
    $("#tipo_accion").val("");
    $(".input_enfrentado").prop("hidden", true);
});
function crearHabilidad() {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            yield $.ajax({
                url: `${endpoint}/api/HabilidadesDisciplinas`,
                method: "POST",
                contentType: "application/json",
                headers: {
                    "Authorization": `Bearer ${localStorage.getItem("token")}`
                },
                data: JSON.stringify({
                    "nombre_Habilidad": String($(".nombre-habilidad").val()),
                    "nivel": Number($(".nivel-habilidad").val()),
                    "tirada": String($(".dados-habilidades").val()),
                    "enardecimiento": $('input[name="enardecimiento"]:checked').val() == "true",
                    "tiradaEnfrentada": String($(".tirada_enfrentada_dato").val()),
                    "disciplinaId": id
                }),
                dataType: "json"
            });
            $('#exampleModal').modal('hide');
            $("#cuerpo_tabla").empty();
            yield llenarTabla();
            Swal.fire({
                icon: "success",
                title: "¡Éxito!",
                text: "¡Registro creado con éxito!",
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
}
function EditarHabilidad() {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            yield $.ajax({
                url: `${endpoint}/api/HabilidadesDisciplinas`,
                method: "PUT",
                contentType: "application/json",
                headers: {
                    "Authorization": `Bearer ${localStorage.getItem("token")}`
                },
                data: JSON.stringify({
                    "nombre_Habilidad": String($(".nombre-habilidad").val()),
                    "nivel": Number($(".nivel-habilidad").val()),
                    "tirada": String($(".dados-habilidades").val()),
                    "enardecimiento": $('input[name="enardecimiento"]:checked').val() == "true",
                    "tiradaEnfrentada": String($(".tirada_enfrentada_dato").val()),
                    "disciplinaId": id,
                    "id": habilidadDisciplina.id
                }),
                dataType: "json"
            });
            $('#exampleModal').modal('hide');
            $("#cuerpo_tabla").empty();
            yield llenarTabla();
            Swal.fire({
                icon: "success",
                title: "¡Éxito!",
                text: "¡Registro editado con éxito!",
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
}
function llenarCamposEditar(idRegistro) {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            habilidadDisciplina = yield $.ajax({
                url: `${endpoint}/api/HabilidadesDisciplinas/${idRegistro}`,
                method: "GET",
                contentType: "application/json",
                headers: {
                    "Authorization": `Bearer ${localStorage.getItem("token")}`
                },
                data: JSON.stringify({
                    "nombre_Habilidad": String($(".nombre-habilidad").val()),
                    "nivel": Number($(".nivel-habilidad").val()),
                    "tirada": String($(".dados-habilidades").val()),
                    "enardecimiento": $('input[name="tirada_enfrentada"]:checked').val() == "true",
                    "tiradaEnfrentada": String($(".tirada_enfrentada_dato").val()),
                    "disciplinaId": id
                }),
                dataType: "json"
            });
            $(".nombre-habilidad").val(habilidadDisciplina.nombre_Habilidad);
            $(".nivel-habilidad").val(habilidadDisciplina.nivel);
            $(".dados-habilidades").val(habilidadDisciplina.tirada);
            if (habilidadDisciplina.enardecimiento) {
                $('input[name="enardecimiento"][value="true"]').prop("checked", true);
            }
            else {
                $('input[name="enardecimiento"][value="false"]').prop("checked", true);
            }
            if (habilidadDisciplina.tiradaEnfrentada == "") {
                $('input[name="tirada_enfrentada"][value="false"]').prop("checked", true);
                $(".input_enfrentado").prop("hidden", true);
            }
            else {
                $('input[name="tirada_enfrentada"][value="true"]').prop("checked", true);
                $(".input_enfrentado").prop("hidden", false);
                $(".tirada_enfrentada_dato").val(habilidadDisciplina.tiradaEnfrentada);
            }
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
$("#cuerpo_tabla").on("click", ".btn-eliminar", function () {
    return __awaiter(this, void 0, void 0, function* () {
        let id = $(this).data("id");
        const respuesta = yield Swal.fire({
            icon: "warning", // Cambiado a warning porque es una acción de eliminar
            title: "¡Atención!",
            text: "¿Estás seguro de que deseas eliminar esta habilidad?",
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
                url: `${endpoint}/api/HabilidadesDisciplinas/${id}`
            });
            $("#cuerpo_tabla").empty();
            yield llenarTabla();
            Swal.fire({
                icon: "success",
                title: "¡Éxito!",
                text: "Habilidad eliminada con éxito",
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
