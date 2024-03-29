import { Component, OnInit } from '@angular/core';
import { Team, ITeam } from '../../shared/teams/team';
import { TeamsService } from '../../services/teams/teams.service';

@Component({
  selector: 'app-team-details',
  templateUrl: './team-details.component.html',
  styleUrls: ['./team-details.component.css']
})
export class TeamDetailsComponent implements OnInit {
  team: ITeam = new Team();

  //TODO delete
  teamId = 1;

  constructor(private _teamsService: TeamsService) { 
    this._teamsService.getTeam(this.teamId)
    .subscribe(team => this.team = team);
  }

  ngOnInit() {

  }
}
