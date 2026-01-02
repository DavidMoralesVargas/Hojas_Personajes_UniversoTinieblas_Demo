$(document).ready(async function () {
    let parametro = new URLSearchParams(window.location.search);
    let tipoVampiro : string | null = parametro.get("tipo");

    if(tipoVampiro !== null){ //Se valida si hay parámetro en la URL
        let existe : boolean | null = await verificarVampiro(tipoVampiro);  //Se llama al método para verificar si el vampiro existe

        if(existe != null && existe != false){ //Si el vampiro existe, se cambia la imagen del título
            let tipo = $("#titulo_vampiro");
            tipo.attr("src", `..../../../../../Images/Titulos_Vampiros/${tipoVampiro.toLowerCase()}.png`);
        }

    }

    console.log("Documento listo");
});

//Método para verificar si el vampiro existe en la base de datos
async function verificarVampiro(tipoVampiro: string) : Promise<boolean | null> {

    try {
        const respuesta = await $.ajax({
            url: `https://localhost:7118/api/Vampiros/verificarExistencia/${encodeURIComponent(tipoVampiro)}`,
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