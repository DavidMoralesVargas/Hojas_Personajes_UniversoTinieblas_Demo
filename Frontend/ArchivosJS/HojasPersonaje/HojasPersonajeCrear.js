var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
const queryString = window.location.search;
const urlParams = new URLSearchParams(queryString);
const clanId = String(urlParams.get('clanId'));
let vampiroBuscado;
let tiposDepredador = [];
let disciplinas_vampiro = [];
let DisciplinaAll = [];
let contador = 0;
const endpoint = "https://localhost:7118";
import { verificarToken } from "../Cuentas.js";
import { elArchivoExiste } from "../buscarFotos.js";
$(document).ready(function () {
    return __awaiter(this, void 0, void 0, function* () {
        let VampuiroEncontrado = false;
        let token = verificarToken(); //Se verifica si hay un token en el localStorage
        if (token == undefined || token == null || token == "") {
            window.location.href = "/Frontend/Vistas/index.html";
        }
        if (clanId != "") {
            VampuiroEncontrado = yield buscarClan(clanId);
        }
        if (VampuiroEncontrado) {
            yield MostrarDisciplinaClan();
        }
        ObtenerTiposDepredador();
        llenarDisciplinas();
    });
});
function ObtenerTiposDepredador() {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            tiposDepredador = yield $.ajax({
                url: `${endpoint}/api/Tipos_Depredador/combo`,
                type: 'GET',
                contentType: 'application/json',
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem('token')}`
                }
            });
            let selectDepredador = document.getElementById("tipos_depredador");
            tiposDepredador.forEach((tipo) => {
                var _a;
                let option = document.createElement("option");
                option.value = ((_a = tipo.id) === null || _a === void 0 ? void 0 : _a.toString()) || "";
                option.text = tipo.nombre;
                selectDepredador.add(option);
            });
        }
        catch (error) {
            console.error('Error al obtener los tipos de depredador:', error.responseText);
        }
    });
}
function MostrarDisciplinaClan() {
    return __awaiter(this, void 0, void 0, function* () {
        contador = 0;
        try {
            disciplinas_vampiro = yield $.ajax({
                url: `${endpoint}/api/Disciplinas_Vampiros/comboAll?idVampiro=${vampiroBuscado.id}`,
                type: 'GET',
                contentType: 'application/json',
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem('token')}`
                }
            });
            disciplinas_vampiro.forEach((disciplinaVampiro) => {
                const selectId = `disciplina-${contador + 1}`;
                const selectElement = document.getElementById(selectId);
                let option = document.createElement("option");
                option.value = (disciplinaVampiro === null || disciplinaVampiro === void 0 ? void 0 : disciplinaVampiro.id.toString()) || "";
                option.text = disciplinaVampiro.disciplina.nombre_Disciplina || "";
                option.selected = true;
                selectElement.add(option);
                contador++;
            });
        }
        catch (error) {
            console.error('Error al mostrar las disciplinas del clan:', error.responseText);
        }
    });
}
function buscarClan(clanId) {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            vampiroBuscado = yield $.ajax({
                url: `${endpoint}/api/Vampiros/${clanId}`,
                type: 'GET',
                contentType: 'application/json',
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem('token')}`
                }
            });
            if (!vampiroBuscado || vampiroBuscado == null || vampiroBuscado == undefined) {
                return false;
            }
            const foto = `/Frontend/Images/Titulos_Vampiros/${vampiroBuscado.nombre}.png`;
            elArchivoExiste(foto).then((existe) => {
                if (existe) {
                    let tipo = $("#titulo_vampiro");
                    tipo.attr("src", `..../../../../../Images/Titulos_Vampiros/${vampiroBuscado.nombre.toLowerCase()}.png`);
                }
            });
            return true;
        }
        catch (error) {
            console.error('Error al buscar el clan:', error.responseText);
            return false;
        }
    });
}
$("#btn_siguiente").on("click", function () {
    window.location.href = `/Frontend/Vistas/HojasDePersonaje/Vampiro/Biografia.html?clanId=${vampiroBuscado === null || vampiroBuscado === void 0 ? void 0 : vampiroBuscado.id}`;
});
function llenarDisciplinas() {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            DisciplinaAll = yield $.ajax({
                type: "GET",
                url: `${endpoint}/api/Disciplinas/combo`,
                dataType: "json"
            });
            const comunes = DisciplinaAll.filter((disciplina) => {
                // Retornamos true si el ID existe en el otro array
                let existe = disciplinas_vampiro.find(dv => dv.disciplinaId === disciplina.id);
                return existe === undefined;
            });
            for (let i = contador; i < 6; i++) {
                const selectId = `disciplina-${i + 1}`;
                const selectElement = document.getElementById(selectId);
                comunes.forEach((disciplina) => {
                    var _a;
                    let option = document.createElement("option");
                    option.value = ((_a = disciplina.id) === null || _a === void 0 ? void 0 : _a.toString()) || "";
                    option.text = disciplina.nombre_Disciplina;
                    selectElement.add(option);
                });
            }
        }
        catch (error) {
            console.error(error.message);
        }
    });
}
