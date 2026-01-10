import { elArchivoExiste, verificarVampiroMuestra } from "./buscarFotos.js";

let parametro = new URLSearchParams(window.location.search);
let tipoVampiro : string | null = parametro.get("tipo");

$(document).ready(async function () {

    $("#regresar_pagina").prop("href", `/Frontend/Vistas/HojasDePersonaje/Vampiro/HojasPersonaje.html?tipo=${tipoVampiro}`);

    if(tipoVampiro !== null){
        let existe : boolean | null = await verificarVampiroMuestra(tipoVampiro);

        if(existe != null && existe != false){

            const foto = `/Frontend/Images/Titulos_Vampiros/${tipoVampiro}.png`;

            elArchivoExiste(foto).then((existe: boolean) => {
                if (existe) {
                    let tipo = $("#titulo_vampiro");
                    tipo.attr("src", `..../../../../../Images/Titulos_Vampiros/${tipoVampiro.toLowerCase()}.png`);
                }
            });
        }
    }

});
