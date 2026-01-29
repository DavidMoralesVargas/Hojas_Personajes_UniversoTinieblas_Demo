const queryString = window.location.search;
const urlParams = new URLSearchParams(queryString);
const clanId : string = String(urlParams.get('clanId'));
declare var Swal: any;

let vampiroBuscado : Vampiro;


const endpoint : string = "https://localhost:7118";

import { verificarToken } from "../Cuentas.js";
import { elArchivoExiste } from "../buscarFotos.js";
import { Vampiro } from "../interfacesEntidades.js";

$(document).ready(async function () {
    let token : string | undefined = verificarToken(); //Se verifica si hay un token en el localStorage

    if(token == undefined || token == null || token == ""){
        window.location.href = "/Frontend/Vistas/index.html";
    }

    if(clanId != ""){
        await buscarClan(clanId);
    }
});



async function buscarClan(clanId: string) {
    try{
        vampiroBuscado = await $.ajax({
            url: `${endpoint}/api/Vampiros/${clanId}`,
            type: 'GET',
            contentType: 'application/json',
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            }
        });

        if(!vampiroBuscado || vampiroBuscado == null || vampiroBuscado == undefined){
            return;
        }

        const foto = `/Frontend/Images/Titulos_Vampiros/${vampiroBuscado.nombre}.png`;
        
        elArchivoExiste(foto).then((existe: boolean) => {
            if (existe) {
                let tipo = $("#titulo_vampiro");
                tipo.attr("src", `..../../../../../Images/Titulos_Vampiros/${vampiroBuscado.nombre.toLowerCase()}.png`);
            }
        });

    }catch(error:any){
        console.error('Error al buscar el clan:', error.responseText);
    }
}


$("#btn_regresar").on("click", function(){
    window.location.href = `/Frontend/Vistas/HojasDePersonaje/Vampiro/hoja_personaje.html?clanId=${clanId}`;
});
