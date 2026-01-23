const endpoint : string = "https://localhost:7118";

import { verificarToken } from "../Cuentas.js";
import { Tipo_Depredador } from "../interfacesEntidades.js";
import { mostrarClanes, realizarFiltro, tiposVampiros} from "../Vampiro.js";


declare var Swal: any;
let tiposDepredador : Tipo_Depredador[] = [];
let idEditar : number;
let tipoDepredador : Tipo_Depredador;

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
    
    await llenarTabla();
});



async function llenarTabla(){
    try{
        let respuesta = await $.ajax({
            type: "GET",
            url: `${endpoint}/api/Tipos_Depredador`,
            dataType: "json"
        });

        tiposDepredador = respuesta.resultado;
        console.log(tiposDepredador);

        let tabla = $("#cuerpo_tabla_tipos_depredador");
        tabla.empty();
        tabla.html(tiposDepredador.map(tipo => `
            <tr>
                <td>${tipo.nombre}</td>
                <td>
                    <button class="btn btn-warning me-2 editar" data-id="${tipo.id}" data-evento="editar" data-bs-toggle="modal" data-bs-target="#crearEditarModal"><i class="bi bi-pencil-square"></i> Editar</button>
                    <button class="btn btn-danger" data-id="${tipo.id}"><i class="bi bi-trash"></i> Eliminar</button>
                </td>
            </tr>
        `).join(""));

    }catch(error:any){
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


//Evento para crear el título del modal al dar clic en el botón de crear
const botonCrear = document.querySelector(".crear")!;
botonCrear.addEventListener("click", function(){
    const tituloModal = document.getElementById("TituloModal")!;
    tituloModal.textContent = "Crear Tipo de Depredador";
    $("#accion").val("crear");
});


//Evento para editar el titulo del modal al dar clic en el botón de editar
$(document).on("click", ".editar", async function(){
    const tituloModal = document.getElementById("TituloModal")!;
    tituloModal.textContent = "Editar Tipo de Depredador";
    $("#accion").val("editar");

    idEditar = $(this).data("id");
    
    await BuscarTipoDepredadorPorID(idEditar);

    //Llenar los campos del modal con la información del tipo de depredador
    $("#nombre_tipo_depredador").val(tipoDepredador.nombre);
});


async function BuscarTipoDepredadorPorID(id: number){
    try{
        let dato = await $.ajax({
            type: "GET",
            url: `${endpoint}/api/Tipos_Depredador/${id}`,
            dataType: "json"
        });
        tipoDepredador = dato.resultado;
        console.log(tipoDepredador);
    }
    catch(error:any){
        console.error(error.responseText);
    }
}


async function guardarCambios(accion: string) {
    let crear = accion === "crear";

    try {
        await $.ajax({
            type: crear ? "POST" : "PUT",
            url: `${endpoint}/api/Tipos_Depredador`,
            data: JSON.stringify({
                id: crear ? 0 : idEditar,
                nombre: String($("#nombre_tipo_depredador").val())
            }),
            contentType: "application/json"
        });

        const modalElement = document.getElementById('crearEditarModal') as any;

        if (modalElement) {
            const bootstrapAny = (window as any).bootstrap;

            const modal =
                bootstrapAny.Modal.getInstance(modalElement) ||
                new bootstrapAny.Modal(modalElement);

            modal.hide();
        }

        Swal.fire({
            icon: "success",
            title: crear
                ? "¡Tipo de depredador creado exitosamente!"
                : "¡Tipo de depredador editado exitosamente!",
            toast: true,
            position: "bottom-start",
            timer: 3000,
            confirmButtomText: 'Aceptar',
            confirmButtomColor: '#3085d6'
        });

        
        $("#cuerpo_tabla_tipos_depredador").empty();

        await llenarTabla();

    } catch (error: any) {
        console.error(error);
        Swal.fire("Error", "No se pudo guardar", "error");
    }
}


let botonGuardar = document.getElementById("Guardar")!;
botonGuardar.addEventListener("click", function(){
    let accion = String($("#accion").val());
    guardarCambios(accion);
});


//Modal para limpiar el campo nombre al cerrar el modal
$('#crearEditarModal').on('hidden.bs.modal', function () {
    $("#nombre_tipo_depredador").val("");
});



//Evento para eliminar registro
$(document).on("click", ".btn-danger", async function(){
    let idEliminar = $(this).data("id");
    console.log("ID a eliminar: " + idEliminar);

    let confirmacion = await Swal.fire({
            icon: "info",
            title: "Estás seguro de eliminar este tipo de depredador?",
            confirmButtomText: 'Aceptar',
            confirmButtomColor: '#3085d6',
            showCancelButton: true,
            cancelButtonText: 'Cancelar',
            cancelButtonColor: '#ff0000ff',
            confirmButtonText: 'Aceptar',
    });

    if(!confirmacion.isConfirmed){
        return;
    }

    await $.ajax({
        type: "DELETE",
        url: `${endpoint}/api/Tipos_Depredador/${idEliminar}`,
        dataType: "json"
    });

    Swal.fire({
        icon: "success",
        title: "¡Tipo de depredador eliminado exitosamente!",
        toast: true,
        position: "bottom-start",
        timer: 3000,
        confirmButtomText: 'Aceptar',
        confirmButtomColor: '#3085d6'
    });

    $("#cuerpo_tabla_tipos_depredador").empty();

    await llenarTabla();
});

