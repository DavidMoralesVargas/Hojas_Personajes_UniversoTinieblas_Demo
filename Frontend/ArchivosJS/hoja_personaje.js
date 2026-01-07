"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
$(document).ready(function () {
    return __awaiter(this, void 0, void 0, function* () {
        let parametro = new URLSearchParams(window.location.search);
        let tipoVampiro = parametro.get("tipo");
        if (tipoVampiro !== null) { //Se valida si hay parámetro en la URL
            let existe = yield verificarVampiro(tipoVampiro); //Se llama al método para verificar si el vampiro existe
            if (existe != null && existe != false) { //Si el vampiro existe, se cambia la imagen del título
                let tipo = $("#titulo_vampiro");
                tipo.attr("src", `..../../../../../Images/Titulos_Vampiros/${tipoVampiro.toLowerCase()}.png`);
            }
        }
        console.log("Documento listo");
    });
});
//Método para verificar si el vampiro existe en la base de datos
function verificarVampiro(tipoVampiro) {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            const respuesta = yield $.ajax({
                url: `https://localhost:7118/api/Vampiros/verificarExistencia/${encodeURIComponent(tipoVampiro)}`,
                method: "GET",
                dataType: "json"
            });
            return respuesta.resultado;
        }
        catch (xhr) {
            console.error("Error HTTP:", xhr.status);
            console.error("Respuesta:", xhr.responseText);
            return null;
        }
    });
}
