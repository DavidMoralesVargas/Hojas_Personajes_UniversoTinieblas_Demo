//Tipo de dato para los vampiros
export interface Vampiro{
    id: number;
    nombre: string;
    clanes_Banes: Clan_Bane[];
    disciplina_Vampiro : disciplina_Vampiro[];
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
}