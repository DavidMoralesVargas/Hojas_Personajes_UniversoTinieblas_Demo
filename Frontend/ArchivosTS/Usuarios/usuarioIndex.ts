const endpoint : string = "https://localhost:7118";

import { verificarToken } from "../Cuentas.js";
import { Usuario , Tipo_Usuario} from "../interfacesEntidades.js";
import { mostrarClanes, realizarFiltro, tiposVampiros} from "../Vampiro.js";

let activo : boolean = true;

declare var Swal: any;

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
    let filas : string = "";

    let usuarios : Usuario[] = await $.ajax({
        url: `${endpoint}/api/Usuarios/listarUsuarios/${activo}`,
        method: "GET",
        headers: {
            "Authorization": `Bearer ${localStorage.getItem("token")}`
        },
        dataType: "json"
    });

    usuarios.forEach(usuarios => {

        filas += `<tr>
                    <td>${usuarios.nombre_Usuario}</td>
                    <td>${Tipo_Usuario[usuarios.tipo_Usuario]}</td>
                    <td><img style="width: 60px;" src="../../Images/SinFotoVampiro.png" ></td>
                    <td>${usuarios.cronicas.length}</td>
                    <td>${usuarios.hojas_Personaje.length}</td>
                    <td>
                        <buttom class="btn ${activo ? "btn-warning" : "btn-info"} btn-sm ${activo ? "desactivar_registro" : "activar_registro"}" data-id="${usuarios.id}">
                            <i class="${activo ? "bi bi-lightbulb" : "bi bi-lightbulb-fill"}"></i> 
                            ${activo ? "Desactivar" : "Activar"}
                        </buttom>
                    </td>
                </tr>`;
    });

    $("#cuerpo_tabla").empty();

    $("#cuerpo_tabla").append(filas);

}


$('input[name="estado"]').on("change", async function(e){
    activo = e.target.id == "activos";
    await llenarTabla();
});

$("#cuerpo_tabla").on("click", ".activar_registro", function(){
    let idUsuario : string = String($(this).data("id"));
    cambiarEstadoUsuario(idUsuario);
});

$("#cuerpo_tabla").on("click", ".desactivar_registro", function(){
    let idUsuario : string = String($(this).data("id"));
    cambiarEstadoUsuario(idUsuario);
});

async function cambiarEstadoUsuario(idUsu : string){
    const respuesta = await Swal.fire({
        icon: "warning", // Cambiado a warning porque es una acción de eliminar
        title: "¡Atención!",
        text: "¿Estás seguro de cambiar el estado del usuario?",
        showCancelButton: true,      // Corregido: Button con 'n'
        confirmButtonText: '¡Modificar!',
        confirmButtonColor: '#00c3ff', // Rojo
        cancelButtonText: "Cancelar",  // Corregido: Button con 'n'
        cancelButtonColor: '#ff0000'   // Un azul o gris suele quedar mejor para cancelar
    });

    if(!respuesta.isConfirmed){
        return;
    }

    try{
        await $.ajax({
            url: `${endpoint}/api/Usuarios`,
            method: "PUT",
            contentType: "application/json", 
            headers: {
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            data: JSON.stringify({
                "activo": !activo,
                "usuario": idUsu
            }),
            dataType: "json"
        });
        
        Swal.fire({
            icon: "success",
            title : "¡Éxito!",
            text : `Estado de usuario cambiado con éxito`,
            confirmButtonText: 'Aceptar',
            confirmButtonColor: '#3085d6',
            toast : true,
            position : "bottom-start",
            timer : 3000            
        });

        $("#cuerpo_tabla").empty();
        await llenarTabla();
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


//Formulario para cuando envíen el registro de un nuevo usuario
$("#crear_vampiro").on("click", async function () {
    
    const username : string = String($("#username").val());
    const password : string = String($("#password").val());
    const passwordConfirm : string = String($("#confirmPassword").val());
    const tipoUsuario : Tipo_Usuario = String($("#tipo_usuario").val()) === "dungeon_master" ? Tipo_Usuario.Dungeon_Master : Tipo_Usuario.Jugador

    try{
        await $.ajax({
            url: `${endpoint}/api/Usuarios/RegistrarUsuario`,
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify({
                "nombre_Usuario": username,
                "tipo_Usuario" : tipoUsuario,
                "contraseña": password,
                "contraseñaConfirmacion": passwordConfirm
            }),
        });

        await llenarTabla();

        Swal.fire({
            icon: "success",
            title : "¡Éxito!",
            text : "Usuario creado con éxito",
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

    const modalElement = document.getElementById('exampleModal') as any;

    if (modalElement) {
        const bootstrapAny = (window as any).bootstrap;

        const modal =
            bootstrapAny.Modal.getInstance(modalElement) ||
            new bootstrapAny.Modal(modalElement);

        modal.hide();
    }
});


$("#exampleModal").on("hidden.bs.modal", function(){
    $("#username").val("");
    $("#password").val("");
    $("#confirmPassword").val("");
    $("#tipo_usuario").val("");
})