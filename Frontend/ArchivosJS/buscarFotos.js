var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
/**
 * Verifica si un archivo existe en una ruta relativa del proyecto
 * @param path Ruta al archivo (ej: '/assets/img/foto.png')
 */
export function elArchivoExiste(path) {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            // Usamos cache: 'no-cache' para evitar falsos positivos de archivos antiguos
            const respuesta = yield fetch(path, {
                method: 'HEAD',
                cache: 'no-cache'
            });
            // respuesta.ok es true si el código es 200-299
            // Si es 404 (No encontrado), devolverá false
            return respuesta.ok;
        }
        catch (error) {
            // Esto solo ocurre si el servidor no responde o hay error de DNS
            console.warn("No se pudo conectar con el servidor para verificar el archivo.");
            return false;
        }
    });
}
export function verificarVampiroMuestra(tipoVampiro) {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            const respuesta = yield $.ajax({
                url: `https://localhost:7118/api/Vampiros/verificarExistencia/${encodeURIComponent(tipoVampiro)}`,
                method: "GET",
                dataType: "json"
            });
            return respuesta.resultado;
        }
        catch (xhr) {
            console.error("Error HTTP:", xhr.status);
            console.error("Respuesta:", xhr.responseText);
            return null;
        }
    });
}
