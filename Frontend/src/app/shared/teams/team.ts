export interface ITeam{
    id: number;
    name: string;
    balance: number;
    availableBalance: number; 
}

export class Team implements ITeam {
    id: number;
    name: string;
    balance: number;
    availableBalance: number;
}