const endpoint : string = "https://localhost:7118";

import { Vampiro } from "./interfacesEntidades.js";

export let tiposVampiros : Vampiro[];

//Enum para los tipos de usuario
export enum TipoUsuario {
    Dungeon_Master = 0,
    Jugador = 1
}



//Método para mostrar los clanes en el menú desplegable
export async function mostrarClanes() {

    //Se realiza la consulta a la base de datos para obtener los vampiros
    let respuesta = await $.ajax({ 
        url: `${endpoint}/api/Vampiros/combo`,
        method: "GET",
        dataType: "json"
    });

    tiposVampiros = respuesta.resultado;

    //Se recorren los vampiros obtenidos y se agregan al menú desplegable
    realizarFiltro(respuesta.resultado, "");
}



//Función que filta lista de vampiros según el nombre del clan ingresado en el input de búsqueda para las hojas
export function realizarFiltro(vampiros : Vampiro[], nombreClan: string) {
    $("#seleccion_clanes li:gt(0)").remove(); //Se eliminan los elementos anteriores del menú desplegable, excepto el primero (input de búsqueda)

    let seleccionClanes = $("#seleccion_clanes");

    vampiros.filter((clan : Vampiro) => clan.nombre.toLowerCase().includes(nombreClan.toString().toLowerCase())).forEach((clan : Vampiro) => {
        let clanItem = `<li><a class="dropdown-item" href="/Frontend/Vistas/HojasDePersonaje/Vampiro/HojasPersonaje.html?tipo=${clan.nombre.toLowerCase()}">${clan.nombre}</a></li>`;
        seleccionClanes.append(clanItem);
    });
}