const endpoint : string = "https://localhost:7118";

import { verificarToken } from "../Cuentas.js";
import { mostrarClanes, realizarFiltro, tiposVampiros } from "../Vampiro.js";
import { Habilidades_Disciplina } from "../interfacesEntidades.js";


declare var Swal: any;
let contador = 1;


//Evento para cerrar sesión
$("#cerrar-sesion").on("click", function() {
    localStorage.removeItem("token");
    window.location.href = "/Frontend/Vistas/index.html";
});

//Evento que toma el valor del input de búsqueda y llama a la función para filtrar los clanes
$(".input_clanes").on("input", function() {
    console.log("Input detectado");
    let nombreClan : string  = String($(".input_clanes").val() || ""); //Tomar nombre del input de búsqueda
    realizarFiltro(tiposVampiros, nombreClan);
});


$(document).ready(async function () {
    mostrarClanes();  //Se llama al método para mostrar los clanes de vampiro en el menú desplegable
    let token : string | undefined = verificarToken(); //Se verifica si hay un token en el localStorage

    if(token == undefined || token == null || token == ""){
        window.location.href = "/Frontend/Vistas/index.html";
    }

});


$("#añadir").on("click", function () {
    const original = document.querySelector(".elemento_copias") as HTMLElement;
    const clon = original.cloneNode(true) as HTMLElement;

    const index = contador++;

    // 🔹 ENARDECIMIENTO
    clon
        .querySelectorAll<HTMLInputElement>('input.enardecimiento')
        .forEach(radio => {
            radio.name = `enardecimiento_${index}`;
            radio.checked = false;
        });

    // 🔹 Input oculto de enfrentada
    const inputEnfrentado = clon.querySelector<HTMLElement>('.input_enfrentado');
    if (inputEnfrentado) {
        inputEnfrentado.hidden = true;
    }

    $(".contenedor_habilidades").append(clon);
});


$(document).on('change', 'input[name="tirada_enfrentada"]', function (e: JQuery.ChangeEvent) {
    const target = e.target as HTMLInputElement;
    const valor: string = target.value;

    const input_hidden = target.closest(".enfrentado")?.querySelector(".input_enfrentado");

    if(valor == "true"){
        input_hidden?.removeAttribute("hidden");
    }else{
        input_hidden?.setAttribute("hidden", "");
    }

});



$("#crear").on("click", async function () {

    const habilidades : Habilidades_Disciplina[]  = Array.from(
        document.querySelectorAll<HTMLElement>('.elemento_copias')
    ).map(bloque => {

        const nombre = (bloque.querySelector<HTMLInputElement>('.nombre-habilidad'))?.value ?? '';
        const nivel = Number((bloque.querySelector<HTMLInputElement>('.nivel-habilidad'))?.value ?? 0);
        const dados = (bloque.querySelector<HTMLInputElement>('.dados-habilidades'))?.value ?? '';

        const enardecimiento = Boolean(bloque.querySelector<HTMLInputElement>(
            'input[name^="enardecimiento_"]:checked'
        )?.value === "true");


        const tiradaEnfrentadaDato = (bloque.querySelector<HTMLInputElement>(
            '.tirada_enfrentada_dato'
        ))?.value ?? '';

        return {
            "nombre_Habilidad" : nombre,
            "nivel" : nivel,
            "tirada" : dados,
            "enardecimiento" : enardecimiento == true,
            "tiradaEnfrentada" : tiradaEnfrentadaDato
        };
    }).filter(hab => 
        // Filtra solo si hay algo en el bloque (puedes definir tu criterio)
        hab.nombre_Habilidad || hab.nivel > 0 || hab.tirada || hab.tiradaEnfrentada
    );;

    await enviarDatos(habilidades);
});

async function enviarDatos(habilidad_disciplina : Habilidades_Disciplina[]){

    let nombre_disciplina : string = String($("#nombre_disciplina").val());

    try{
        await $.ajax({
            url: `${endpoint}/api/Disciplinas/all`,
            method: "POST",
            contentType: "application/json", 
            headers: {
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            data: JSON.stringify({
                "nombre_Disciplina": nombre_disciplina,
                "habilidades": habilidad_disciplina,
            }),
            dataType: "json"
        });
        window.location.href = "../../Vistas/Disciplinas/DisciplinasIndex.html";
        sessionStorage.setItem("mostrarAlerta", "true");

    }catch(error : any){
        Swal.fire({
            icon: "warning",
            title : "!Algo mal ocurrió!",
            text : `${error.responseText}`,
            confirmButtonText: 'Aceptar',
            confirmButtonColor: '#ff0000ff',
            toast : true,
            position : "bottom-start",
            timer : 3000            
        });
    }
}
