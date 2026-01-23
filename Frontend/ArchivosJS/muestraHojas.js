var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
import { elArchivoExiste, verificarVampiroMuestra } from "./buscarFotos.js";
const endpoint = "https://localhost:7118";
let parametro = new URLSearchParams(window.location.search);
let tipoVampiro = parametro.get("tipo");
let connection;
$(document).ready(function () {
    return __awaiter(this, void 0, void 0, function* () {
        $("#pagina_siguiente").prop("href", `/Frontend/Vistas/HojasDePersonaje/Vampiro/biografiaM.html?tipo=${tipoVampiro}`);
        if (tipoVampiro !== null) {
            let existe = yield verificarVampiroMuestra(tipoVampiro);
            if (existe != null && existe != false) {
                const foto = `/Frontend/Images/Titulos_Vampiros/${tipoVampiro}.png`;
                elArchivoExiste(foto).then((existe) => {
                    if (existe) {
                        let tipo = $("#titulo_vampiro");
                        tipo.attr("src", `..../../../../../Images/Titulos_Vampiros/${tipoVampiro.toLowerCase()}.png`);
                    }
                });
            }
        }
        yield llenarDisciplinas();
    });
});
function llenarDisciplinas() {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            let resultado = yield $.ajax({
                type: "GET",
                url: `${endpoint}/api/Disciplinas/combo`,
                dataType: "json"
            });
            console.log(resultado);
            let selects = document.querySelectorAll("select[class*='disciplina']");
            console.log(selects);
            selects.forEach((select) => {
                resultado.forEach((disciplina) => {
                    let option = document.createElement("option");
                    option.value = disciplina.id.toString();
                    option.text = disciplina.nombre_Disciplina;
                    select.appendChild(option);
                });
            });
        }
        catch (error) {
            console.error(error.message);
        }
    });
}
