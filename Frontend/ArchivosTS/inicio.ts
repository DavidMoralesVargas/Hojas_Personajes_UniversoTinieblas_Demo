const endpoint : string = "https://localhost:7118";

import { verificarToken, TipoUsuario, verificarUsuarioExistente } from "./Cuentas.js";
import { mostrarClanes, realizarFiltro, tiposVampiros } from "./Vampiro.js";
import { Cronica, Hoja_Personaje, UsuarioEncontradoDTO, Vampiro } from "./interfacesEntidades.js";

declare const signalR: any;
declare var Swal: any;
let idEdicion : number;
let nombreEdicion : string;

let usuarioEncontrado : UsuarioEncontradoDTO;

//Evento para cuando la página esté cargada
$(document).ready(async function () {
    configuracionSignalr();
    mostrarClanes();  //Se llama al método para mostrar los clanes de vampiro en el menú desplegable
    let rol : string | undefined  = verificarToken(); //Se verifica si hay un token en el localStorage

    if(rol == "Dungeon_Master"){
        $(".btn_crear").prop("hidden", false);
    }

    usuarioEncontrado = await verificarUsuarioExistente();
    await listarCronicas();

});


async function buscarHojaPersonajePorCronica(idCronica : number, idUsuario : string) : Promise<Hoja_Personaje | undefined>{

    try{
        let hojaPersonaje : Hoja_Personaje = await $.ajax({
            url: `${endpoint}/api/HojasPersonajes/buscarPorCronicaId?cronicaId=${idCronica}&idUsuario=${idUsuario}`,
            method: "GET", 
            headers: {
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            dataType: "json"
        });
        
        return hojaPersonaje;
    }
    catch(error : any){
        console.error(error.responseText)
    }
}

async function armarCronicaHTML(cronica : Cronica) : Promise<void>{
    let elemento : string;

    elemento = 
    `
    <div style="margin-left: 2%;" class="mb-5 cronica">
                <div style="
                        max-width:240px;
                        padding:1rem;
                        background:#ffffff;
                        border-radius:8px;
                        box-shadow:0 4px 10px rgba(0,0,0,0.08);
                "
                    class="contenedor">
                    <div class="d-flex">
                        <h3 class="mb-3 titulo_cronica" style="font-size:1.1rem;">
                        ${cronica.nombre_Cronica}
                        </h3>
                        ${verificarToken() == "Dungeon_Master" ? `
                        <button style="margin-left: auto;" class="btn btn-warning editarCronica" data-id="${cronica.id}">
                            <i class="bi bi-pencil-square"></i>
                        </button>
                        ` : ``}

                    </div>
                    
                    <p style="font-size:1.1rem;">País: ${cronica.pais_Cronica}</p>
                    <p style="font-size:1.1rem;">Creador crónica: ${cronica.dungeon_Master.nombre_Usuario}</p>
                    <input hidden value=${cronica.id} class="idCronica">
                    <input hidden value=${cronica.finalizado} class="finalizadoCronica">
                    <input hidden value=${cronica.pais_Cronica} class="paisCronica">
                    

                    ${await buscarHojaPersonajePorCronica(cronica.id || 0 , usuarioEncontrado?.usuarioId || "") === undefined ? 
                        `
                        <button data-bs-toggle="modal" data-bs-target="#CrearHojaModal" class="btn btn-primary btn-sm w-100 btn_crear_hoja">
                            Crear Hoja de Personaje
                        </button>
                        ` 
                        : 
                        `
                        <a class="btn btn-success btn-sm w-100 btn_ingresar_cronica">
                            Ingresar
                        </a>
                        `}
            </div>
        </div>
    `;
    const contenedor = document.createElement("div");
        contenedor.innerHTML = elemento;
        document.querySelector(".lista_cronica")?.appendChild(contenedor);
}

async function listarCronicas(){
    let resultados : Cronica[] = await $.ajax({
        url: `${endpoint}/api/Cronicas`,
        method: "GET", 
        headers: {
            "Authorization": `Bearer ${localStorage.getItem("token")}`
        },
        dataType: "json"
    });
    
    if(resultados.length == 0){
        return;
    }
    resultados.forEach(async (cronica : Cronica) => {
        await armarCronicaHTML(cronica);
        
    });
}


//Evento para volver a la versión antes de edición de una crónica
$(".lista_cronica").on("click", ".cancelar_edicion", function(e){
    const input = e.target.closest(".d-flex").querySelector(".mi-input");
    const boton = e.target.closest(".d-flex").querySelector(".cancelar_edicion");

    const nuevoElemento = document.createElement("h3");
    nuevoElemento.className = "mb-3 titulo_cronica";
    nuevoElemento.style.fontSize = "1.1rem";
    nuevoElemento.textContent = nombreEdicion;

    boton.className = "btn btn-warning editarCronica";
    boton.dataset.id = idEdicion;
    boton.querySelector("i").className = "bi bi-pencil-square";

    input.replaceWith(nuevoElemento);
}); 


//Evento para completar la edición de una crónica
$(".lista_cronica").on("click", ".guardar_edicion", async function(e){

    const contenedor = e.target.closest(".cronica");

    try{
        await $.ajax({
            url: `${endpoint}/api/Cronicas`,
            method: "PUT",
            contentType: "application/json", 
            headers: {
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            data: JSON.stringify({
                "id" : String(contenedor.querySelector(".idCronica").value),
                "nombre_Cronica" : String(contenedor.querySelector(".mi-input").value),
                "pais_Cronica" : String(contenedor.querySelector(".paisCronica").value),
                "finalizado" : String(contenedor.querySelector(".finalizadoCronica").value) == "true"
            }),
            dataType: "json"
        });
        
        
        Swal.fire({
            icon: "success",
            title : "¡Éxito!",
            text : "Cambios en Crónica realizado con éxito",
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

    const input = e.target.closest(".d-flex").querySelector(".mi-input");
    const boton = e.target.closest(".d-flex").querySelector(".guardar_edicion");

    const nuevoElemento = document.createElement("h3");
    nuevoElemento.className = "mb-3 titulo_cronica";
    nuevoElemento.style.fontSize = "1.1rem";
    nuevoElemento.textContent = input.value;

    boton.className = "btn btn-warning editarCronica";
    boton.dataset.id = idEdicion;
    boton.querySelector("i").className = "bi bi-pencil-square";

    input.replaceWith(nuevoElemento);
});



//Evento para cambiar el titulo de la cronica por un input
$(".lista_cronica").on("click", ".editarCronica", function(e){
    idEdicion = e.target.dataset.id;

    const titulo = e.target.closest(".contenedor").querySelector(".titulo_cronica");
    const nombreTitulo = titulo.textContent.trim();

    const inputNuevo = document.createElement("input");
    inputNuevo.type = "text";
    inputNuevo.className = "form-control mi-input";
    inputNuevo.value = nombreTitulo;

    const boton = e.target.closest(".editarCronica");
    boton.className = "btn btn-danger cancelar_edicion";
    const iconoBoton = boton.querySelector("i")
    iconoBoton.className = "bi bi-x-octagon-fill";

    nombreEdicion = nombreTitulo;

    titulo.replaceWith(inputNuevo)
});


//Evento para cambiar de estilo el botón de guardar o cancelar.
$(".lista_cronica").on("input", ".mi-input", function(e){
    let nuevoTexto : string = String(e.target.value);
    let boton;

    if(nuevoTexto == nombreEdicion){
        boton = e.target.closest(".d-flex").querySelector(".guardar_edicion");
        boton.className = "btn btn-danger cancelar_edicion";
        const iconoBoton = boton.querySelector("i")
        iconoBoton.className = "bi bi-x-octagon-fill";
        return;
    }

    boton = e.target.closest(".d-flex").querySelector(".cancelar_edicion");
    boton.className = "btn btn-primary guardar_edicion";
    const iconoBoton = boton.querySelector("i")
    iconoBoton.className = "bi bi-bookmark";
});


//Evento que toma el valor del input de búsqueda y llama a la función para filtrar los clanes
$(".input_clanes").on("input", function() {
    console.log("Input detectado");
    let nombreClan : string  = String($(".input_clanes").val() || ""); //Tomar nombre del input de búsqueda
    realizarFiltro(tiposVampiros, nombreClan);
});


//Evento para ocultar el formulario de login al abrir el modal de registro
$("#exampleModal").on("show.bs.modal", function () {
    $("#login").attr("style", "display: none !important");
});

//Evento para mostrar el formulario de login al cerrar el modal de registro
$("#exampleModal").on("hidden.bs.modal", function () {
    $("#login").show();
});


//Evento para cerrar sesión
$("#cerrar-sesion").on("click", function() {
    localStorage.removeItem("token");
    window.location.reload();
});

//Método para añadir el token al localStorage
function AñadirTokenLocalStorage(token: string){
    localStorage.setItem("token", token);
}


$(".enviar_cronica").on("click", async function(){
    try{
        $.ajax({
            url: `${endpoint}/api/Cronicas`,
            method: "POST",
            contentType: "application/json", 
            headers: {
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            data: JSON.stringify({
                "nombre_Cronica": String($("#nombre_cronica").val()),
                "pais_Cronica": String($("#pais_cronica").val()),
            }),
            dataType: "json"
        });

        
    }catch(error : any){
        console.error(error.responseText)
        Swal.fire({
            icon: "error",
            title : "¡Falló!",
            text : `Ocurrió un error`,
            confirmButtonText: 'Aceptar',
            confirmButtonColor: '#ff0022',
            toast : true,
            position : "bottom-start",
            timer : 3000            
        });
    }


    const modalElement = document.getElementById('ModalCrearCronica') as any;

    if (modalElement) {
        const bootstrapAny = (window as any).bootstrap;

        const modal =
            bootstrapAny.Modal.getInstance(modalElement) ||
            new bootstrapAny.Modal(modalElement);

        modal.hide();
    }
});

async function buscarCronica(id : number) : Promise<void>{
    try{
        let cronica : Cronica = await $.ajax({
            url: `${endpoint}/api/Cronicas/${id}`,
            method: "GET",
            headers: {
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            dataType: "json"
        });

        await armarCronicaHTML(cronica);
    }catch(error : any){
        console.error(error.responseText)
    }
}


$("#ModalCrearCronica").on("hidden.bs.modal", function(){
    $("#nombre_cronica").val("");
    $("#pais_cronica").val("");
});


function configuracionSignalr(): void {
    console.log(localStorage.getItem("token"))
    let connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:7118/chat", {
            accessTokenFactory: () => localStorage.getItem("token")
        })
        .withAutomaticReconnect()
        .build();

    connection.on("NuevaCronicaCreada", async (id : number) => {
        console.log(id);
        await buscarCronica(id);
    });

    connection.start()
        .then(() => console.log("Conectado a SignalR"))
        .catch((err:any) => console.error("Error SignalR:", err));
}


//Formulario para cuando envíen el registro de un nuevo usuario
$("#formulario_registro").on("submit", async function (event) {
    event.preventDefault(); // Evitar el envío del formulario
    console.log("Entrando al submit del formulario de registro");
    try{
        // Obtener los valores de los campos del formulario
        const username = $("#username").val();
        const password = $("#password").val();
        const passwordConfirm = $("#confirmPassword").val();

        let respuesta = await $.ajax({
            url: `${endpoint}/api/Usuarios/RegistrarUsuario`,
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify({
                "nombre_Usuario": username,
                "tipo_Usuario": TipoUsuario.Jugador,
                "contraseña": password,
                "contraseñaConfirmacion": passwordConfirm
            }),
        });

        AñadirTokenLocalStorage(respuesta.token);
        window.location.reload();
    }catch(error){
        // Casteamos el error a JQuery.jqXHR para tener autocompletado
        const err = error as JQuery.jqXHR;

        console.error("Código de estado:", err.status); // Ejemplo: 400

        if (err.status === 400) {
            // El BadRequest de .NET suele venir en 'responseJSON'
            // Lo tipamos como 'any' o un record para leer los mensajes
            const detallesError = err.responseJSON;
            
            console.log("Mensaje exacto del servidor:", detallesError);
            
            // Si quieres acceder a los errores de validación específicos de ASP.NET Core:
            if (detallesError && detallesError.errors) {
                console.table(detallesError.errors); 
            }
        } else {
            console.error("Error no controlado:", err.responseText);
        }
    }
});


//Formulario para cuando envíen el login de un usuario
$("#formulario_login").on("submit", async function (event) {
    event.preventDefault(); // Evitar el envío del formulario
    console.log("Entrando al submit del formulario de login");
    try{
        // Obtener los valores de los campos del formulario
        const username = $("#nombre_usuario").val();
        const password = $("#contraseña").val();

        let respuesta = await $.ajax({
            url: `${endpoint}/api/Usuarios/Login`,
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify({
                "email": username,
                "password": password
            }),
        });
        console.log("Respuesta del servidor:", respuesta);
        AñadirTokenLocalStorage(respuesta.token);
        window.location.reload();
    }catch(error){
        Swal.fire({
            icon: "error",
            title : "¡Error!",
            text : "Error al iniciar sesión. Verifica tus credenciales e inténtalo de nuevo.",
            confirmButtonText: 'Aceptar',
            confirmButtonColor: '#ff002b',
            toast : true,
            position : "bottom-start",
            timer : 3000
        });
    }
});





//Evento para llenar select para seleccionar un vampiro para la hoja de personaje
$("#CrearHojaModal").on("shown.bs.modal" ,async function(){
    const modal = $(this);

    let respuesta = await $.ajax({
        url: `${endpoint}/api/Vampiros/combo`,
        method: "GET",
        dataType: "json"
    });

    let clanesVampiro : Vampiro[] = respuesta.resultado;
    let options : string = "";

    options = clanesVampiro.map((clan : Vampiro) => {
        return `<option value="${clan.id}">${clan.nombre}</option>`;
    }).join("");

    modal.find("#seleccion_clanes").append(options);
});

//Evento para ir a crear el personaje una vez seleccionado el clan
$("#ir_a_crear").on("click", function(){
    const modal = $(this).closest(".modal");
    const select = modal.find("#seleccion_clanes");
    const valor = select.val();

    if(valor == ""){
        Swal.fire({
            icon: "warning",
            title : "¡Atención!",
            text : "Debes seleccionar un clan para continuar",
            confirmButtonText: 'Aceptar',
            confirmButtonColor: '#ffcc00',
            timer : 3000            
        });
        return;
    }

    window.location.href = `../Vistas/HojasDePersonaje/Vampiro/hoja_personaje.html?clanId=${valor}`;
});
