$(document).ready(async function () {
    let parametro = new URLSearchParams(window.location.search);
    let tipoVampiro : string | null = parametro.get("tipo");

    if(tipoVampiro !== null){
        let existe : boolean | null = await verificarVampiroMuestra(tipoVampiro);

        if(existe != null && existe != false){
            let tipo = $("#titulo_vampiro");
            tipo.attr("src", `..../../../../../Images/Titulos_Vampiros/${tipoVampiro.toLowerCase()}.png`);
        }

    }

    console.log("Documento listo");
});

async function verificarVampiroMuestra(tipoVampiro: string) : Promise<boolean | null> {

    try {
        const respuesta = await $.ajax({
            url: `https://localhost:7118/api/Vampiros/verificarExistencia/${encodeURIComponent(tipoVampiro)}`,
            headers: {
            "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            method: "GET",
            dataType: "json"
        });

        return respuesta.resultado;

    } catch (xhr: any) {
        console.error("Error HTTP:", xhr.status);
        console.error("Respuesta:", xhr.responseText);
        return null;
    }
}