using System;
using System.Linq;
using System.Text;
using Handball.Core.Contracts;
using Handball.Models;
using Handball.Models.Contracts;
using Handball.Repositories;
using Handball.Utilities.Messages;

namespace Handball.Core;

public class Controller : IController
{
    private PlayerRepository players;
    private TeamRepository teams;
    private string[] validPlayerTypes = new[] { nameof(CenterBack), nameof(ForwardWing), nameof(Goalkeeper) };
    public Controller()    // CTOR  Dali ne moje i bez nego .....
    {
        players = new PlayerRepository();
        teams = new TeamRepository();
    }


    public string NewTeam(string name)
    {

        if (teams.ExistsModel(name))
        {
            return String.Format(OutputMessages.TeamAlreadyExists, name, nameof(TeamRepository));
        }

        ITeam newTeam = new Team(name);
        teams.AddModel(newTeam);
        return String.Format(OutputMessages.TeamSuccessfullyAdded, name, nameof(TeamRepository));

    }
    public string NewPlayer(string typeName, string name)
    {
        if (!validPlayerTypes.Contains(typeName))
        {
            return String.Format(OutputMessages.InvalidTypeOfPosition, typeName);
        }

        if (players.ExistsModel(name))
        {
            return String.Format(OutputMessages.PlayerIsAlreadyAdded, name, nameof(PlayerRepository),
                players.GetModel(name).GetType().Name);
        }

        IPlayer newPlayer = null;

        if (typeName == nameof(CenterBack))
        {
            newPlayer = new CenterBack(name);
        }
        else if (typeName == nameof(ForwardWing))
        {
            newPlayer = new ForwardWing(name);
        }
        else if (typeName == nameof(Goalkeeper))
        {
            newPlayer = new Goalkeeper(name);

        }
        players.AddModel(newPlayer);
        return String.Format(OutputMessages.PlayerAddedSuccessfully, name);
    }
    public string NewContract(string playerName, string teamName)
    {
        if (!players.ExistsModel(playerName))
        {
            return String.Format(OutputMessages.PlayerNotExisting, playerName, nameof(PlayerRepository));
        }

        if (!teams.ExistsModel(teamName))
        {
            return String.Format(OutputMessages.TeamNotExisting, teamName, nameof(TeamRepository));

        }

        if (players.GetModel(playerName).Team != null)
        {
            return String.Format(OutputMessages.PlayerAlreadySignedContract, playerName,
                players.GetModel(playerName).Team);
        }

        var player = players.GetModel(playerName);
        player.JoinTeam(teamName);
        teams.GetModel(teamName).SignContract(player);
        return String.Format(OutputMessages.SignContract, playerName, teamName);
    }






    public string NewGame(string firstTeamName, string secondTeamName)
    {
        var first = teams.GetModel(firstTeamName);
        var second = teams.GetModel(secondTeamName);
        if (first.OverallRating > second.OverallRating)
        {
            first.Win();
            second.Lose();

            return String.Format(OutputMessages.GameHasWinner, firstTeamName, secondTeamName);

        }
        if (first.OverallRating < second.OverallRating)
        {
            first.Lose();
            second.Win();
            return String.Format(OutputMessages.GameHasWinner, secondTeamName, firstTeamName);

        }

        if (first.OverallRating==second.OverallRating)
        {
            first.Draw();
            second.Draw();
           
        }

        return String.Format(OutputMessages.GameIsDraw, firstTeamName, secondTeamName);

    }

    public string PlayerStatistics(string teamName)
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine($"***{teamName}***");

        var playersFromThatTeam = teams.GetModel(teamName).Players.ToList();
        var sortedPlayers=playersFromThatTeam
            .OrderByDescending(p => p.Rating)
            .ThenBy(x=>x.Name)
            .ToList();
        foreach (var pl in sortedPlayers)
        {
            result.AppendLine(pl.ToString().TrimEnd());
        }

        return result.ToString().Trim();
    }







    public string LeagueStandings()
    {
        StringBuilder result = new StringBuilder();
        var sortedTeams = teams.Models
            .OrderByDescending(x => x.PointsEarned)
            .ThenByDescending(x=>x.OverallRating)
            .ThenBy(x => x.Name)
            .ToList();
        result.AppendLine("***League Standings***");
        foreach (var t in sortedTeams)
        {
            result.AppendLine(t.ToString().TrimEnd());
        }
        return result.ToString().Trim();
    }
}