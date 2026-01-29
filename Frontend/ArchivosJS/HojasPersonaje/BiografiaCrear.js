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
const endpoint = "https://localhost:7118";
import { verificarToken } from "../Cuentas.js";
import { elArchivoExiste } from "../buscarFotos.js";
$(document).ready(function () {
    return __awaiter(this, void 0, void 0, function* () {
        let token = verificarToken(); //Se verifica si hay un token en el localStorage
        if (token == undefined || token == null || token == "") {
            window.location.href = "/Frontend/Vistas/index.html";
        }
        if (clanId != "") {
            yield buscarClan(clanId);
        }
    });
});
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
                return;
            }
            const foto = `/Frontend/Images/Titulos_Vampiros/${vampiroBuscado.nombre}.png`;
            elArchivoExiste(foto).then((existe) => {
                if (existe) {
                    let tipo = $("#titulo_vampiro");
                    tipo.attr("src", `..../../../../../Images/Titulos_Vampiros/${vampiroBuscado.nombre.toLowerCase()}.png`);
                }
            });
        }
        catch (error) {
            console.error('Error al buscar el clan:', error.responseText);
        }
    });
}
$("#btn_regresar").on("click", function () {
    window.location.href = `/Frontend/Vistas/HojasDePersonaje/Vampiro/hoja_personaje.html?clanId=${clanId}`;
});
