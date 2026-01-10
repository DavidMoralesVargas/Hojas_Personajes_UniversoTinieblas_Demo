const endpoint : string = "https://localhost:7118";

import { verificarToken } from "../Cuentas.js";
import { mostrarClanes, realizarFiltro, tiposVampiros } from "../Vampiro.js";
import { Disciplina, Habilidades_Disciplina } from "../interfacesEntidades.js";

declare var Swal: any;

let habilidadDisciplina : Habilidades_Disciplina;

const params = new URLSearchParams(window.location.search);

const id = params.get('disciplina');


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


$(document).ready(async function () {
    mostrarClanes();  //Se llama al método para mostrar los clanes de vampiro en el menú desplegable
    let token : string | undefined = verificarToken(); //Se verifica si hay un token en el localStorage

    if(token == undefined || token == null || token == ""){
        window.location.href = "/Frontend/Vistas/index.html";
    }

    await buscarDisciplina();
    await llenarTabla();
});


async function buscarDisciplina(){
    let resultado = await $.ajax({
        url: `${endpoint}/api/Disciplinas/${id}`,
        method: "GET",
        headers: {
            "Authorization": `Bearer ${localStorage.getItem("token")}`
        },
        dataType: "json"
    });

    let disciplina : Disciplina = resultado.resultado;

    $("#titulo").html(disciplina.nombre_Disciplina);
}

async function llenarTabla(){
let filas : string = "";

    let resultado : Habilidades_Disciplina[] = await $.ajax({
        url: `${endpoint}/api/HabilidadesDisciplinas/disciplina/${id}`,
        method: "GET",
        headers : {
            "Authorization" : `Bearer ${localStorage.getItem("token")}`
        },
        dataType: "json"
    });

    resultado.forEach(fila => {
        filas += `<tr>
                    <td>${fila.nombre_Habilidad}</td>
                    <td>${fila.nivel}</td>
                    <td>${fila.tirada}</td>
                    <td>${fila.enardecimiento === true ? "Si" : "No"}</td>
                    <td>${fila.tiradaEnfrentada || "N/A"}</td>
                    <td>
                        <buttom data-bs-toggle="modal" data-bs-target="#exampleModal" class="btn btn-warning btn-sm btn-editar" data-id="${fila.id}"><i class="bi bi-pen"></i> Editar</buttom>
                        <buttom class="btn btn-danger btn-eliminar btn-sm" data-id="${fila.id}"><i class="bi bi-trash"></i> Eliminar</buttom>
                    </td>
                    <td hidden>${fila.id}</td>
                </tr>`;
    });

    $("#cuerpo_tabla").append(filas);
}

$("#btn-crear").on("click", function(){
    $("#exampleModalLabel").html("Crear nueva habilidad")
    $("#tipo_accion").val("crear");
});

$("#cuerpo_tabla").on("click", ".btn-editar", function(e){
    $("#exampleModalLabel").html("Editar habilidad de disciplina");
    $("#tipo_accion").val("editar");
    llenarCamposEditar(e.target.dataset.id);
});


$('input[name="tirada_enfrentada"]').on("change", function(){
    const valor = $('input[name="tirada_enfrentada"]:checked').val();
    if(valor == "true"){
        $(".input_enfrentado").prop("hidden", false);
    }else{
        $(".input_enfrentado").prop("hidden", true);
        $(".tirada_enfrentada_dato").val("");
    }
});

$("#guardar_cambios").on("click", async function(){
    let tipo_accion : string = String($("#tipo_accion").val());

    if(tipo_accion == "crear"){
        await crearHabilidad();
    }else{
        await EditarHabilidad();
    }
});

$('#exampleModal').on('hidden.bs.modal', function () {
    $(".nombre-habilidad").val("");
    $(".nivel-habilidad").val("");
    $(".dados-habilidades").val("");
    $('input[name="enardecimiento"][value="false"]').prop("checked", true);
    $('input[name="tirada_enfrentada"][value="false"]').prop("checked", true);
    $(".tirada_enfrentada_dato").val("");
    $("#tipo_accion").val("");
    $(".input_enfrentado").prop("hidden", true);
});


async function crearHabilidad() {
    try{
        await $.ajax({
            url: `${endpoint}/api/HabilidadesDisciplinas`,
            method: "POST",
            contentType: "application/json", 
            headers: {
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            data: JSON.stringify({
                "nombre_Habilidad": String($(".nombre-habilidad").val()),
                "nivel": Number($(".nivel-habilidad").val()),
                "tirada": String($(".dados-habilidades").val()),
                "enardecimiento" : $('input[name="enardecimiento"]:checked').val() == "true",
                "tiradaEnfrentada" : String($(".tirada_enfrentada_dato").val()),
                "disciplinaId" : id
            }),
            dataType: "json"
        });

        ($('#exampleModal') as any).modal('hide');
        $("#cuerpo_tabla").empty();
        await llenarTabla();

        Swal.fire({
            icon: "success",
            title : "¡Éxito!",
            text : "¡Registro creado con éxito!",
            confirmButtonText: 'Aceptar',
            confirmButtonColor: '#3085d6',
            toast : true,
            position : "bottom-start",
            timer : 3000
        });
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


async function EditarHabilidad() {
    try{
        await $.ajax({
            url: `${endpoint}/api/HabilidadesDisciplinas`,
            method: "PUT",
            contentType: "application/json", 
            headers: {
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            data: JSON.stringify({
                "nombre_Habilidad": String($(".nombre-habilidad").val()),
                "nivel": Number($(".nivel-habilidad").val()),
                "tirada": String($(".dados-habilidades").val()),
                "enardecimiento" : $('input[name="enardecimiento"]:checked').val() == "true",
                "tiradaEnfrentada" : String($(".tirada_enfrentada_dato").val()),
                "disciplinaId" : id,
                "id" : habilidadDisciplina.id
            }),
            dataType: "json"
        });

        ($('#exampleModal') as any).modal('hide');
        $("#cuerpo_tabla").empty();
        await llenarTabla();

        Swal.fire({
            icon: "success",
            title : "¡Éxito!",
            text : "¡Registro editado con éxito!",
            confirmButtonText: 'Aceptar',
            confirmButtonColor: '#3085d6',
            toast : true,
            position : "bottom-start",
            timer : 3000
        });
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


async function llenarCamposEditar(idRegistro : number){
    try{
        habilidadDisciplina = await $.ajax({
            url: `${endpoint}/api/HabilidadesDisciplinas/${idRegistro}`,
            method: "GET",
            contentType: "application/json", 
            headers: {
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            data: JSON.stringify({
                "nombre_Habilidad": String($(".nombre-habilidad").val()),
                "nivel": Number($(".nivel-habilidad").val()),
                "tirada": String($(".dados-habilidades").val()),
                "enardecimiento" : $('input[name="tirada_enfrentada"]:checked').val() == "true",
                "tiradaEnfrentada" : String($(".tirada_enfrentada_dato").val()),
                "disciplinaId" : id
            }),
            dataType: "json"
        });

        $(".nombre-habilidad").val(habilidadDisciplina.nombre_Habilidad);
        $(".nivel-habilidad").val(habilidadDisciplina.nivel);
        $(".dados-habilidades").val(habilidadDisciplina.tirada);
        if(habilidadDisciplina.enardecimiento){
            $('input[name="enardecimiento"][value="true"]').prop("checked", true)
        }else{
            $('input[name="enardecimiento"][value="false"]').prop("checked", true)
        }

        if(habilidadDisciplina.tiradaEnfrentada == ""){
            $('input[name="tirada_enfrentada"][value="false"]').prop("checked", true);
            $(".input_enfrentado").prop("hidden", true);
        }else{
            $('input[name="tirada_enfrentada"][value="true"]').prop("checked", true);
            $(".input_enfrentado").prop("hidden", false);
            $(".tirada_enfrentada_dato").val(habilidadDisciplina.tiradaEnfrentada);
        }

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


$("#cuerpo_tabla").on("click", ".btn-eliminar", async function(){
    let id : number = $(this).data("id");

    const respuesta = await Swal.fire({
        icon: "warning", // Cambiado a warning porque es una acción de eliminar
        title: "¡Atención!",
        text: "¿Estás seguro de que deseas eliminar esta habilidad?",
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
            url: `${endpoint}/api/HabilidadesDisciplinas/${id}`
        });

        $("#cuerpo_tabla").empty();

        await llenarTabla();

        Swal.fire({
            icon: "success",
            title : "¡Éxito!",
            text : "Habilidad eliminada con éxito",
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






