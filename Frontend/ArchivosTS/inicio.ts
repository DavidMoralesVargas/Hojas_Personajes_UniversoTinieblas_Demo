$(document).ready(function () {
    mostrarClanes();  //Se llama al método para mostrar los clanes de vampiro en el menú desplegable
});

let tiposVampiros : Vampiro[];

//Tipo de dato para los vampiros
interface Vampiro{
    id: number;
    nombre: string;
}


//Método para mostrar los clanes en el menú desplegable
async function mostrarClanes() {

    //Se realiza la consulta a la base de datos para obtener los vampiros
    let respuesta = await $.ajax({
        url: "https://localhost:7118/api/Vampiros",
        method: "GET",
        dataType: "json"
    });

    tiposVampiros = respuesta.resultado;

    //Se recorren los vampiros obtenidos y se agregan al menú desplegable
    realizarFiltro(respuesta.resultado, "");
}


$(".input_clanes").on("input", function() {
    console.log("Input detectado");
    let nombreClan : string  = String($(".input_clanes").val() || ""); //Tomar nombre del input de búsqueda
    realizarFiltro(tiposVampiros, nombreClan);
});


function realizarFiltro(vampiros : Vampiro[], nombreClan: string) {
    $("#seleccion_clanes li:gt(0)").remove(); //Se eliminan los elementos anteriores del menú desplegable, excepto el primero (input de búsqueda)

    let seleccionClanes = $("#seleccion_clanes");

    vampiros.filter((clan : Vampiro) => clan.nombre.toLowerCase().includes(nombreClan.toString().toLowerCase())).forEach((clan : Vampiro) => {
        let clanItem = `<li><a class="dropdown-item" href="./HojasDePersonaje/Vampiro/HojasPersonaje.html?tipo=${clan.nombre.toLowerCase()}">${clan.nombre}</a></li>`;
        seleccionClanes.append(clanItem);
    });
}