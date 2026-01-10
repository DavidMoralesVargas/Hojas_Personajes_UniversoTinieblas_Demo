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
let parametro = new URLSearchParams(window.location.search);
let tipoVampiro = parametro.get("tipo");
$(document).ready(function () {
    return __awaiter(this, void 0, void 0, function* () {
        $("#regresar_pagina").prop("href", `/Frontend/Vistas/HojasDePersonaje/Vampiro/HojasPersonaje.html?tipo=${tipoVampiro}`);
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
    });
});
