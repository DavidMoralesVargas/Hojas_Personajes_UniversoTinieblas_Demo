const endpoint : string = "https://localhost:7118";



//Enum para los tipos de usuario
export enum TipoUsuario {
    Dungeon_Master = 0,
    Jugador = 1
}

//Definir interface para el token decodificado
interface JwtPayload {
    sub?: string;
    unique_name?: string;
    email?: string;
    role?: string | string[];
    exp?: number;
    iat?: number;
    [key: string]: any; // Permite campos adicionales personalizados
}



/**
 * Decodifica el payload de un JWT y lo devuelve como un objeto tipado.
 */
function parseJwt(token: string): JwtPayload | null {
    if (!token) return null;

    try {
        const base64Url = token.split('.')[1];
        const base64 = base64Url!.replace(/-/g, '+').replace(/_/g, '/');
        
        // Decodificación compatible con caracteres especiales (UTF-8)
        const jsonPayload = decodeURIComponent(
            atob(base64)
                .split('')
                .map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
                .join('')
        );

        return JSON.parse(jsonPayload) as JwtPayload;
    } catch (error) {
        console.error("Error decodificando el JWT:", error);
        return null;
    }
}


export function verificarToken() : string | undefined{
    let token = localStorage.getItem("token");
    if(token != null && token != undefined && token != ""){

        $("#login").attr("style", "display: none !important");
        $("#elemento-cerrar-sesion").removeAttr("hidden");

        let rol : string = parseJwt(token)?.["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

        if(rol === TipoUsuario[0]){
            $(".lista-administracion").removeAttr("hidden");
        }
        return rol;
    }
}


