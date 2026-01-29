import { elArchivoExiste, verificarVampiroMuestra } from "./buscarFotos.js";
import { Disciplina } from "./interfacesEntidades.js";

const endpoint : string = "https://localhost:7118";


let parametro = new URLSearchParams(window.location.search);
let tipoVampiro : string | null = parametro.get("tipo");


$(document).ready(async function () {

    $("#pagina_siguiente").prop("href", `/Frontend/Vistas/HojasDePersonaje/Vampiro/biografiaM.html?tipo=${tipoVampiro}`);

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

    await llenarDisciplinas();

});

async function llenarDisciplinas(){
    try{
        let resultado : Disciplina[] = await $.ajax({
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
    catch(error:any){
        console.error(error.message);
    }
}

