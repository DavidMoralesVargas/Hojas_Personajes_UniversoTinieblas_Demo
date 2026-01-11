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
let noSeleccionado = [];
let Seleccionado = [];
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
        console.log(localStorage.getItem("token"));
        yield mostrarDisciplinas();
    });
});
function mostrarDisciplinas() {
    return __awaiter(this, void 0, void 0, function* () {
        let registros;
        try {
            registros = yield $.ajax({
                url: `${endpoint}/api/Disciplinas`,
                method: "GET",
                headers: {
                    "Authorization": `Bearer ${localStorage.getItem("token")}`
                },
                dataType: "json"
            });
            noSeleccionado = registros;
            crearNoSeleccionados(noSeleccionado);
        }
        catch (error) {
        }
    });
}
function crearNoSeleccionados(disciplina) {
    const htmlString = disciplina.map((registro) => {
        return `<button type="button" class="list-group-item list-group-item-action border-0 py-1 small BtnNoSeleccionar" data-id="${registro.id}">
                    ${registro.nombre_Disciplina}
                </button>`; // Reemplaza con tu HTML
    }).join('');
    $(".noSeleccionados").html(htmlString);
}
function crearSeleccionados(disciplina) {
    const htmlString = disciplina.map((registro) => {
        return `<button type="button" class="list-group-item list-group-item-action border-0 py-1 small BtnSeleccionar" data-id="${registro.id}">
                    ${registro.nombre_Disciplina}
                </button>`; // Reemplaza con tu HTML
    }).join('');
    $(".Seleccionados").html(htmlString);
}
$(".noSeleccionados").on("click", ".BtnNoSeleccionar", function () {
    const id = $(this).data("id");
    // 1. Buscar el objeto en el arreglo original
    const itemEncontrado = noSeleccionado.find(reg => reg.id === id);
    if (itemEncontrado) {
        // 2. Agregar al arreglo de seleccionados
        Seleccionado.push(itemEncontrado);
        // 3. Quitar del arreglo de no seleccionados
        noSeleccionado = noSeleccionado.filter(reg => reg.id !== id);
        // 4. Refrescar AMBAS listas en el HTML
        crearSeleccionados(Seleccionado);
        crearNoSeleccionados(noSeleccionado);
    }
});
$(".Seleccionados").on("click", ".BtnSeleccionar", function () {
    const id = $(this).data("id");
    // 1. Buscar el objeto en el arreglo original
    const itemEncontrado = Seleccionado.find(reg => reg.id === id);
    if (itemEncontrado) {
        // 2. Agregar al arreglo de seleccionados
        noSeleccionado.push(itemEncontrado);
        // 3. Quitar del arreglo de no seleccionados
        Seleccionado = Seleccionado.filter(reg => reg.id !== id);
        // 4. Refrescar AMBAS listas en el HTML
        crearSeleccionados(Seleccionado);
        crearNoSeleccionados(noSeleccionado);
    }
});
$("#guardar").on("click", function () {
    return __awaiter(this, void 0, void 0, function* () {
        const nombreVampiro = String($("#nombre_vampiro").val());
        const baneVampiro = String($("#bane_vampiro").val());
        const compulsionVampiro = String($("#compulsion_vampiro").val());
        const bane_compulsion = {
            bane: baneVampiro,
            compulsion: compulsionVampiro
        };
        let respuesta = yield $.ajax({
            url: `${endpoint}/api/Vampiros/vampiroAll`,
            method: "POST",
            contentType: "application/json",
            headers: {
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            data: JSON.stringify({
                "nombreVampiro": nombreVampiro,
                "clanBane": bane_compulsion,
                "disciplinas": Seleccionado
            }),
            dataType: "json"
        });
        window.location.href = "../../Vistas/Vampiros/VampiroIndex.html";
        sessionStorage.setItem("mostrarAlerta", "true");
    });
});
