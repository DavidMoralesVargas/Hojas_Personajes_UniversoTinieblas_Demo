const endpoint : string = "https://localhost:7118";

import { Vampiro } from "../interfacesEntidades";
import { mostrarClanes, realizarFiltro, tiposVampiros } from "../Vampiro.js";
import { verificarToken } from "../Cuentas.js";
declare var Swal: any;

let listaVampiros : Vampiro[];

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


$(document).ready(function () {
    mostrarClanes();  //Se llama al método para mostrar los clanes de vampiro en el menú desplegable
    let token : string | undefined = verificarToken(); //Se verifica si hay un token en el localStorage

    if(token == undefined || token == null || token == ""){
        window.location.href = "/Frontend/Vistas/index.html";
    }
    mostrarVampiros();

    if(sessionStorage.getItem("mostrarAlerta") === "true"){
        Swal.fire({
            icon: "success",
            title : "¡Éxito!",
            text : "Cambios en vampiro registrado con éxito",
            confirmButtonText: 'Aceptar',
            confirmButtonColor: '#3085d6',
            toast : true,
            position : "bottom-start",
            timer : 3000
        });

        sessionStorage.removeItem("mostrarAlerta");
    }
});


async function mostrarVampiros() : Promise<void>{

    let vampiros = await $.ajax({ 
        url: `${endpoint}/api/Vampiros`,
        method: "GET",
        headers : {
            "Authorization" : `Bearer ${localStorage.getItem("token")}`
        },
        dataType: "json"
    });

    listaVampiros = vampiros.resultado;
    let filas : string = ``;
    listaVampiros.forEach(vampiro => {

        filas += `<tr>
                    <td>${vampiro.nombre}</td>
                    <td><strong>Bane:</strong> ${vampiro.clanes_Banes[0]?.bane || "Sin debilidad de clan"} <br> <strong>Compulsión:</strong> ${vampiro.clanes_Banes[0]?.compulsion || "Sin compulsión de clan"}</td>
                    <td>${vampiro.disciplina_Vampiro.length || '0'}</td>
                    <td>
                        <a href="../../Vistas/Vampiros/VampiroEditar.html?vampiro=${vampiro.id}" class="btn btn-warning btn-sm"><i class="bi bi-pen"></i> Editar</a>
                        <buttom class="btn btn-danger btn-eliminar btn-sm" data-id="${vampiro.id}"><i class="bi bi-trash"></i> Eliminar</buttom>
                    </td>
                </tr>`;
    });

    $("#body_vampiros").append(filas);
}

$("#body_vampiros").on("click", ".btn-eliminar", async function(){
    let id : number = $(this).data("id");

    const respuesta = await Swal.fire({
        icon: "warning", // Cambiado a warning porque es una acción de eliminar
        title: "¡Atención!",
        text: "¿Estás seguro de que deseas eliminar este vampiro?",
        showCancelButton: true,      // Corregido: Button con 'n'
        confirmButtonText: 'Eliminar',
        confirmButtonColor: '#ff0000', // Rojo
        cancelButtonText: "Cancelar",  // Corregido: Button con 'n'
        cancelButtonColor: '#3085d6'   // Un azul o gris suele quedar mejor para cancelar
    });

    if(!respuesta.isConfirmed){
        return;
    }

    try{

        await $.ajax({
            method : "DELETE",
            dataType: "json",
            headers : {
                "Authorization" : `Bearer ${localStorage.getItem("token")}`
            },
            url: `${endpoint}/api/Vampiros/${id}`
        });

        $("#body_vampiros").empty();

        await mostrarVampiros();

        Swal.fire({
            icon: "success",
            title : "¡Éxito!",
            text : "Vampiro eliminado con éxito",
            confirmButtonText: 'Aceptar',
            confirmButtonColor: '#3085d6',
            toast : true,
            position : "bottom-start",
            timer : 3000            
        });
    }
    catch(error : any){
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
});