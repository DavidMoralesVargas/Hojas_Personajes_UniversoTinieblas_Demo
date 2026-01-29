//Tipo de dato para los vampiros
export interface Vampiro{
    id: number;
    nombre: string;
    clanes_Banes: Clan_Bane[];
    disciplina_Vampiro : disciplina_Vampiro[];
}

export interface Tipo_Depredador{
    id? : number,
    nombre : string
}

export interface Clan_Bane{
    id : number,
    bane : string,
    compulsion : string,
    vampiroId : number
}

export interface disciplina_Vampiro{
    id : number;
    vampiro : Vampiro;
    vampiroId : number;
    disciplina : Disciplina;
    disciplinaId : number;
}

export interface Disciplina{
    id : number,
    nombre_Disciplina: string;
    habilidades_Disciplina : Habilidades_Disciplina[]
}

export interface Habilidades_Disciplina{
    id? : number,
    nombre_Habilidad : string,
    nivel : number,
    tirada : string,
    enardecimiento : boolean,
    tiradaEnfrentada : string
}

export interface Usuario{
    id? : string,
    nombre_Usuario : string,
    tipo_Usuario : Tipo_Usuario,
    activo : boolean,
    foto : string,
    cronicas : Cronica[],
    hojas_Personaje : Hoja_Personaje[]
}


export enum Tipo_Usuario {
    Dungeon_Master = 0,
    Jugador = 1
}

export interface Cronica{
    id? : number,
    nombre_Cronica : string,
    pais_Cronica : string,
    fecha_Cronica : string,
    finalizado : boolean,
    dungeon_MasterId : number
    dungeon_Master : Usuario
}

export interface Hoja_Personaje{
    id? : number,
    nombre : string,
    ambicion : string,
    generacion : number,
    concepto : string,
    sire : string,
    desire : string,
    titulo : string,
    tipo_DepredadorId : number,
    cronicaId : number,
    jugadorId : number,
    tipo_VampiroId : number
}

export interface Tipo_Depredador{
    id? : number,
    nombre : string
}


export interface UsuarioEncontradoDTO{
    usuarioId : string,
    nombreUsuario : string
}





