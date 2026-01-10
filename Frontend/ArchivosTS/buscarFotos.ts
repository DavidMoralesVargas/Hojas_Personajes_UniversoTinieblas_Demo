/**
 * Verifica si un archivo existe en una ruta relativa del proyecto
 * @param path Ruta al archivo (ej: '/assets/img/foto.png')
 */
export async function elArchivoExiste(path: string): Promise<boolean> {
    try {
        // Usamos cache: 'no-cache' para evitar falsos positivos de archivos antiguos
        const respuesta: Response = await fetch(path, { 
            method: 'HEAD',
            cache: 'no-cache' 
        });
        
        // respuesta.ok es true si el código es 200-299
        // Si es 404 (No encontrado), devolverá false
        return respuesta.ok; 
        
    } catch (error: unknown) {
        // Esto solo ocurre si el servidor no responde o hay error de DNS
        console.warn("No se pudo conectar con el servidor para verificar el archivo.");
        return false;
    }
}


export async function verificarVampiroMuestra(tipoVampiro: string) : Promise<boolean | null> {

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