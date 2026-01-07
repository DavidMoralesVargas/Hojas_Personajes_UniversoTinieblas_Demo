const endpoint : string = "https://localhost:7118";

import { Vampiro, Disciplina, Clan_Bane } from "../interfacesEntidades";
import { mostrarClanes, realizarFiltro, tiposVampiros } from "../Vampiro.js";
import { verificarToken } from "../Cuentas.js";

const params = new URLSearchParams(window.location.search);
const id = params.get('vampiro');

let vampiro : Vampiro;
let noSeleccionado : Disciplina[] = [];
let Seleccionado : Disciplina[] = [];

//Evento para cerrar sesión
$("#cerrar-sesion").on("click", function() {
    localStorage.removeItem("token");
    window.location.href = "/Frontend/Vistas/index.html";
});

//Evento que toma el valor del input de búsqueda y llama a la función para filtrar los clanes
$(".input_clanes").on("input", function() {
    let nombreClan : string  = String($(".input_clanes").val() || ""); //Tomar nombre del input de búsqueda
    realizarFiltro(tiposVampiros, nombreClan);
});

async function mostrarDisciplinas(){
    let registros : any; 

    try{
        registros = await $.ajax({
            url: `${endpoint}/api/Disciplinas`,
            method: "GET",
            headers : {
                "Authorization" : `Bearer ${localStorage.getItem("token")}`
            },
            dataType: "json"
        });


        noSeleccionado = registros.resultado.filter((itemA : Disciplina) => {
            return !Seleccionado.some((item : Disciplina) => item.id === itemA.id);
        });

        crearNoSeleccionados(noSeleccionado);

    }catch(error){

    }
}

$(document).ready(async function () {
    mostrarClanes();  //Se llama al método para mostrar los clanes de vampiro en el menú desplegable
    let token : string | undefined = verificarToken(); //Se verifica si hay un token en el localStorage

    if(token == undefined || token == null || token == ""){
        window.location.href = "/Frontend/Vistas/index.html";
    }
    console.log(localStorage.getItem("token"))

    await buscarVampiro();   
    await mostrarDisciplinas();

});

function crearNoSeleccionados(disciplina : Disciplina[]) : void{
    const htmlString = disciplina.map((registro: Disciplina) => {
        return `<button type="button" class="list-group-item list-group-item-action border-0 py-1 small BtnNoSeleccionar" data-id="${registro.id}">
                    ${registro.nombre_Disciplina}
                </button>`; // Reemplaza con tu HTML
    }).join('');

    $(".noSeleccionados").html(htmlString);
}

function crearSeleccionados(disciplina : Disciplina[]) : void{
    const htmlString = disciplina.map((registro: Disciplina) => {
        return `<button type="button" class="list-group-item list-group-item-action border-0 py-1 small BtnSeleccionar" data-id="${registro.id}">
                    ${registro.nombre_Disciplina}
                </button>`; // Reemplaza con tu HTML
    }).join('');

    $(".Seleccionados").html(htmlString);
}

$(".noSeleccionados").on("click", ".BtnNoSeleccionar", function(){
    const id : number = $(this).data("id");

    // 1. Buscar el objeto en el arreglo original
    const itemEncontrado = noSeleccionado.find(reg => reg.id === id);

    if (itemEncontrado) {
        // 2. Agregar al arreglo de seleccionados
        Seleccionado.push(itemEncontrado);

        // 3. Quitar del arreglo de no seleccionados
        noSeleccionado = noSeleccionado.filter(reg => reg.id !== id);


        // 4. Refrescar AMBAS listas en el HTML
        crearSeleccionados(Seleccionado)
        crearNoSeleccionados(noSeleccionado)
    }
});

$(".Seleccionados").on("click", ".BtnSeleccionar", function(){
    const id : number = $(this).data("id");

    // 1. Buscar el objeto en el arreglo original
    const itemEncontrado = Seleccionado.find(reg => reg.id === id);

    if (itemEncontrado) {
        // 2. Agregar al arreglo de seleccionados
        noSeleccionado.push(itemEncontrado);

        // 3. Quitar del arreglo de no seleccionados
        Seleccionado = Seleccionado.filter(reg => reg.id !== id);

        // 4. Refrescar AMBAS listas en el HTML
        crearSeleccionados(Seleccionado)
        crearNoSeleccionados(noSeleccionado)
    }
});


$("#guardar").on("click", async function(){
    const nombreVampiro : string = String($("#nombre_vampiro").val());
    const baneVampiro : string = String($("#bane_vampiro").val());
    const compulsionVampiro : string = String($("#compulsion_vampiro").val());
    const bane_compulsion : Partial<Clan_Bane> = {
        bane : baneVampiro,
        compulsion : compulsionVampiro
    }

    let respuesta = await $.ajax({
        url: `${endpoint}/api/Vampiros/vampiroAll`,
        method: "PUT",
        contentType: "application/json", 
        headers: {
            "Authorization": `Bearer ${localStorage.getItem("token")}`
        },
        data: JSON.stringify({
            "vampiroId" : vampiro.id,
            "nombreVampiro": nombreVampiro,
            "clanBane": bane_compulsion,
            "disciplinas": Seleccionado
        }),
        dataType: "json"
    });

    window.location.href = "../../Vistas/Vampiros/VampiroIndex.html";
    sessionStorage.setItem("mostrarAlerta", "true");
});






async function buscarVampiro() : Promise<void>{
    if(id == null || id == ""){
        window.location.href = "../../Vistas/Vampiros/VampiroIndex.html";
        return;
    }

    vampiro= await $.ajax({
        method : "GET",
        dataType: "json",
        headers : {
            "Authorization" : `Bearer ${localStorage.getItem("token")}`
        },
        url: `${endpoint}/api/Vampiros/${id}`        
    });

    $("#nombre_vampiro").val(vampiro.nombre);
    $("#compulsion_vampiro").val(String(vampiro.clanes_Banes[0]?.compulsion));
    $("#bane_vampiro").val(String(vampiro.clanes_Banes[0]?.bane));

    Seleccionado = vampiro.disciplina_Vampiro.map(x => x.disciplina);

    crearSeleccionados(Seleccionado);
}


