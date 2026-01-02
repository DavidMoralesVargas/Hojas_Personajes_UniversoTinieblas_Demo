"use strict";
$(document).ready(async function () {
    let parametro = new URLSearchParams(window.location.search);
    let tipoVampiro = parametro.get("tipo");
    if (tipoVampiro !== null) {
        let existe = await verificarVampiroMuestra(tipoVampiro);
        if (existe != null && existe != false) {
            let tipo = $("#titulo_vampiro");
            tipo.attr("src", `..../../../../../Images/Titulos_Vampiros/${tipoVampiro.toLowerCase()}.png`);
        }
    }
    console.log("Documento listo");
});
async function verificarVampiroMuestra(tipoVampiro) {
    try {
        const respuesta = await $.ajax({
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
}
