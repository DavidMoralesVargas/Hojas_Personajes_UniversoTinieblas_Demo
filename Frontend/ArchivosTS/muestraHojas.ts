import { elArchivoExiste, verificarVampiroMuestra } from "./buscarFotos.js";

let parametro = new URLSearchParams(window.location.search);
let tipoVampiro : string | null = parametro.get("tipo");

declare const signalR: any;

let connection: any;

$(document).ready(async function () {

    $("#pagina_siguiente").prop("href", `/Frontend/Vistas/HojasDePersonaje/Vampiro/biografiaM.html?tipo=${tipoVampiro}`);

    if(tipoVampiro !== null){
        let existe : boolean | null = await verificarVampiroMuestra(tipoVampiro);

        if(existe != null && existe != false){

            const foto = `/Frontend/Images/Titulos_Vampiros/${tipoVampiro}.png`;

            elArchivoExiste(foto).then((existe: boolean) => {
                if (existe) {
                    let tipo = $("#titulo_vampiro");
                    tipo.attr("src", `..../../../../../Images/Titulos_Vampiros/${tipoVampiro.toLowerCase()}.png`);
                }
            });
        }
    }

    // configuracionSignalr();

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