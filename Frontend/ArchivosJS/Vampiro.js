var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
const endpoint = "https://localhost:7118";
export let tiposVampiros;
//Enum para los tipos de usuario
export var TipoUsuario;
(function (TipoUsuario) {
    TipoUsuario[TipoUsuario["Dungeon_Master"] = 0] = "Dungeon_Master";
    TipoUsuario[TipoUsuario["Jugador"] = 1] = "Jugador";
})(TipoUsuario || (TipoUsuario = {}));
//Método para mostrar los clanes en el menú desplegable
export function mostrarClanes() {
    return __awaiter(this, void 0, void 0, function* () {
        //Se realiza la consulta a la base de datos para obtener los vampiros
        let respuesta = yield $.ajax({
            url: `${endpoint}/api/Vampiros/combo`,
            method: "GET",
            dataType: "json"
        });
        tiposVampiros = respuesta.resultado;
        //Se recorren los vampiros obtenidos y se agregan al menú desplegable
        realizarFiltro(respuesta.resultado, "");
    });
}
//Función que filta lista de vampiros según el nombre del clan ingresado en el input de búsqueda para las hojas
export function realizarFiltro(vampiros, nombreClan) {
    $("#seleccion_clanes li:gt(0)").remove(); //Se eliminan los elementos anteriores del menú desplegable, excepto el primero (input de búsqueda)
    let seleccionClanes = $("#seleccion_clanes");
    vampiros.filter((clan) => clan.nombre.toLowerCase().includes(nombreClan.toString().toLowerCase())).forEach((clan) => {
        let clanItem = `<li><a class="dropdown-item" href="/Frontend/Vistas/HojasDePersonaje/Vampiro/HojasPersonaje.html?tipo=${clan.nombre.toLowerCase()}">${clan.nombre}</a></li>`;
        seleccionClanes.append(clanItem);
    });
}
