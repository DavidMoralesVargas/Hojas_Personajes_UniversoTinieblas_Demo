var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
import { elArchivoExiste, verificarVampiroMuestra } from "./buscarFotos.js";
let parametro = new URLSearchParams(window.location.search);
let tipoVampiro = parametro.get("tipo");
let connection;
$(document).ready(function () {
    return __awaiter(this, void 0, void 0, function* () {
        $("#pagina_siguiente").prop("href", `/Frontend/Vistas/HojasDePersonaje/Vampiro/biografiaM.html?tipo=${tipoVampiro}`);
        if (tipoVampiro !== null) {
            let existe = yield verificarVampiroMuestra(tipoVampiro);
            if (existe != null && existe != false) {
                const foto = `/Frontend/Images/Titulos_Vampiros/${tipoVampiro}.png`;
                elArchivoExiste(foto).then((existe) => {
                    if (existe) {
                        let tipo = $("#titulo_vampiro");
                        tipo.attr("src", `..../../../../../Images/Titulos_Vampiros/${tipoVampiro.toLowerCase()}.png`);
                    }
                });
            }
        }
        // configuracionSignalr();
    });
});
// function configuracionSignalr(): void {
//     console.log(localStorage.getItem("token"))
//     connection = new signalR.HubConnectionBuilder()
//         .withUrl("https://localhost:7118/chat", {
//             accessTokenFactory: () => localStorage.getItem("token")
//         })
//         .withAutomaticReconnect()
//         .build();
//     connection.on("EnviarMensaje", (mensaje: string) => {
//         console.log(mensaje);
//     });
//     connection.start()
//         .then(() => console.log("Conectado a SignalR"))
//         .catch((err:any) => console.error("Error SignalR:", err));
// }
