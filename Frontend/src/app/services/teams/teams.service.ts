import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Team, ITeam } from '../../shared/teams/team';

@Injectable()
export class TeamsService {
  teamsControllerUrl = "http://localhost:49906/api/teams"

  constructor(private _http: HttpClient)
  {
  }

  getTeam(id: number): Observable<ITeam> {
    let getTeamUrl = this.teamsControllerUrl + "/" + id;
    return this._http.get<ITeam>(getTeamUrl)
      .do(data =>{
        let team: ITeam = new Team();
        team.id = data.id,
        team.name = data.name;
        team.balance = data.balance;
        team.availableBalance = data.availableBalance;
        return team;
      })
      .catch(this.handleError);
  }

  private handleError(err: HttpErrorResponse){
    console.error(err.message);
    alert("An error occured, see console.");
    return Observable.throw(err.message);
  }
}
