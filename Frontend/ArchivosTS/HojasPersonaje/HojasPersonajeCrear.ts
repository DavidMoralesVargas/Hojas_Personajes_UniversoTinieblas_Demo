const queryString = window.location.search;
const urlParams = new URLSearchParams(queryString);
const clanId : string = String(urlParams.get('clanId'));
declare var Swal: any;

let vampiroBuscado : Vampiro;
let tiposDepredador : Tipo_Depredador[] = [];
let disciplinas_vampiro : disciplina_Vampiro[] = [];
let DisciplinaAll : Disciplina[] = [];
let contador : number = 0;

const endpoint : string = "https://localhost:7118";

import { verificarToken } from "../Cuentas.js";
import { elArchivoExiste } from "../buscarFotos.js";
import { Vampiro, Tipo_Depredador, Disciplina, disciplina_Vampiro } from "../interfacesEntidades.js";


$(document).ready(async function () {
    let VampuiroEncontrado : boolean = false;

    let token : string | undefined = verificarToken(); //Se verifica si hay un token en el localStorage

    if(token == undefined || token == null || token == ""){
        window.location.href = "/Frontend/Vistas/index.html";
    }

    if(clanId != ""){
        VampuiroEncontrado = await buscarClan(clanId);
    }

    if(VampuiroEncontrado){
        await MostrarDisciplinaClan();
    }

    ObtenerTiposDepredador();
    llenarDisciplinas();
});

async function ObtenerTiposDepredador(){
    try{
        tiposDepredador = await $.ajax({
            url: `${endpoint}/api/Tipos_Depredador/combo`,
            type: 'GET',
            contentType: 'application/json',
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            }
        });

        let selectDepredador = document.getElementById("tipos_depredador") as HTMLSelectElement;

        tiposDepredador.forEach((tipo: Tipo_Depredador) => {
            let option = document.createElement("option");
            option.value = tipo.id?.toString() || "";
            option.text = tipo.nombre;
            selectDepredador.add(option);
        });

    }catch(error:any){
        console.error('Error al obtener los tipos de depredador:', error.responseText);
    }
}

async function MostrarDisciplinaClan(){
    contador = 0;

    try{
        disciplinas_vampiro = await $.ajax({
            url: `${endpoint}/api/Disciplinas_Vampiros/comboAll?idVampiro=${vampiroBuscado.id}`,
            type: 'GET',
            contentType: 'application/json',
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            }
        });

        disciplinas_vampiro.forEach((disciplinaVampiro) => {
            const selectId = `disciplina-${contador + 1}`;
            const selectElement = document.getElementById(selectId) as HTMLSelectElement;
            let option = document.createElement("option");
            option.value = disciplinaVampiro?.id.toString() || "";
            option.text = disciplinaVampiro.disciplina.nombre_Disciplina || "";
            option.selected = true;
            selectElement.add(option);
            contador++;
        });
    }catch(error:any){
        console.error('Error al mostrar las disciplinas del clan:', error.responseText);
    }
}



async function buscarClan(clanId: string) : Promise<boolean> {
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
            return false;
        }

        const foto = `/Frontend/Images/Titulos_Vampiros/${vampiroBuscado.nombre}.png`;
        
        elArchivoExiste(foto).then((existe: boolean) => {
            if (existe) {
                let tipo = $("#titulo_vampiro");
                tipo.attr("src", `..../../../../../Images/Titulos_Vampiros/${vampiroBuscado.nombre.toLowerCase()}.png`);
            }
        });

        return true;

    }catch(error:any){
        console.error('Error al buscar el clan:', error.responseText);
        return false;
    }
}


$("#btn_siguiente").on("click", function(){
    window.location.href = `/Frontend/Vistas/HojasDePersonaje/Vampiro/Biografia.html?clanId=${vampiroBuscado?.id}`;
});


async function llenarDisciplinas(){
    try{
        DisciplinaAll = await $.ajax({
            type: "GET",
            url: `${endpoint}/api/Disciplinas/combo`,
            dataType: "json"
        });
        const comunes = DisciplinaAll.filter((disciplina: Disciplina) => {
            // Retornamos true si el ID existe en el otro array
            let existe : disciplina_Vampiro | undefined = disciplinas_vampiro.find(dv => dv.disciplinaId === disciplina.id);
            return existe === undefined;
            
        });

        for(let i : number = contador; i < 6; i++){
            const selectId = `disciplina-${i + 1}`;
            const selectElement = document.getElementById(selectId) as HTMLSelectElement;

            comunes.forEach((disciplina: Disciplina) => {
                let option = document.createElement("option");
                option.value = disciplina.id?.toString() || "";
                option.text = disciplina.nombre_Disciplina;
                selectElement.add(option);
            });
        }

    }
    catch(error:any){
        console.error(error.message);
    }
}




