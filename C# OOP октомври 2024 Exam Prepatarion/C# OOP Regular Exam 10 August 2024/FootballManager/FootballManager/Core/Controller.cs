using FootballManager.Core.Contracts;
using FootballManager.Models.Contracts;
using FootballManager.Models;
using FootballManager.Repositories;
using FootballManager.Utilities.Messages;
using System.Text;

public class Controller : IController
{
    private TeamRepository championship;
    private string[] managerTypes = new[] { "AmateurManager", "SeniorManager", "ProfessionalManager" };

    public Controller()
    {
        this.championship = new TeamRepository();
    }

    public string JoinChampionship(string teamName)
    {
        if (championship.Capacity == championship.Models.Count)
        {
            return OutputMessages.ChampionshipFull;
        }

        if (championship.Exists(teamName))
        {
            return String.Format(OutputMessages.TeamWithSameNameExisting, teamName);
        }

        Team teamToBeAdded = new Team(teamName);
        championship.Add(teamToBeAdded);
        return String.Format(OutputMessages.TeamSuccessfullyJoined, teamName);
    }

    public string SignManager(string teamName, string managerTypeName, string managerName)
    {
        if (!championship.Exists(teamName))
        {
            return String.Format(OutputMessages.TeamDoesNotTakePart, teamName);
        }

        if (!managerTypes.Contains(managerTypeName))
        {
            return String.Format(OutputMessages.ManagerTypeNotPresented, managerTypeName);
        }

        ITeam team = championship.Get(teamName);
        if (team.TeamManager != null)
        {
            return String.Format(OutputMessages.TeamSignedWithAnotherManager, teamName, team.TeamManager.Name);
        }

        foreach (var currentTeam in championship.Models)
        {
            if (currentTeam.TeamManager?.Name == managerName)
            {
                return String.Format(OutputMessages.ManagerAssignedToAnotherTeam, managerName);
            }
        }

        IManager newManager = null;
        switch (managerTypeName)
        {
            case "AmateurManager":
                newManager = new AmateurManager(managerName);
                break;

            case "SeniorManager":
                newManager = new SeniorManager(managerName);
                break;

            case "ProfessionalManager":
                newManager = new ProfessionalManager(managerName);
                break;

            default:
                return String.Format(OutputMessages.ManagerTypeNotPresented, managerTypeName);
        }

        team.SignWith(newManager);
        return String.Format(OutputMessages.TeamSuccessfullySignedWithManager, managerName, teamName);
    }

    public string MatchBetween(string teamOneName, string teamTwoName)
    {
        if (!championship.Exists(teamOneName) || !championship.Exists(teamTwoName))
        {
            return OutputMessages.OneOfTheTeamDoesNotExist;
        }

        var firstTeam = championship.Get(teamOneName);
        var secondTeam = championship.Get(teamTwoName);
        var winningTeam = firstTeam;
        var losingTeam = secondTeam;

        if (secondTeam.PresentCondition > winningTeam.PresentCondition)
        {
            winningTeam = secondTeam;
            losingTeam = firstTeam;
        }
        else if (secondTeam.PresentCondition == firstTeam.PresentCondition)
        {
            secondTeam.GainPoints(1);
            firstTeam.GainPoints(1);
            return String.Format(OutputMessages.MatchIsDraw, teamOneName, teamTwoName);
        }

        winningTeam.GainPoints(3);

        if (winningTeam.TeamManager != null)
        {
            winningTeam.TeamManager.RankingUpdate(5);
        }

        if (losingTeam.TeamManager != null)
        {
            losingTeam.TeamManager.RankingUpdate(-5);
        }

        return String.Format(OutputMessages.TeamWinsMatch, winningTeam.Name, losingTeam.Name);
    }

    public string PromoteTeam(string droppingTeamName, string promotingTeamName, string managerTypeName, string managerName)
    {
        if (!championship.Exists(droppingTeamName))
        {
            return String.Format(OutputMessages.DroppingTeamDoesNotExist, droppingTeamName);
        }

        if (championship.Exists(promotingTeamName))
        {
            return String.Format(OutputMessages.TeamWithSameNameExisting, promotingTeamName);
        }

        ITeam team = new Team(promotingTeamName);
        if (managerTypes.Contains(managerTypeName))
        {
            bool hasManagerSigned = championship.Models.Any(currentTeam => currentTeam.TeamManager?.Name == managerName);

            if (!hasManagerSigned)
            {
                IManager newManager = null;
                switch (managerTypeName)
                {
                    case "AmateurManager":
                        newManager = new AmateurManager(managerName);
                        break;

                    case "SeniorManager":
                        newManager = new SeniorManager(managerName);
                        break;

                    case "ProfessionalManager":
                        newManager = new ProfessionalManager(managerName);
                        break;
                }
                team.SignWith(newManager);
            }
        }

        foreach (var currentTeam in championship.Models)
        {
            currentTeam.ResetPoints();
        }

        championship.Remove(droppingTeamName);
        championship.Add(team);
        return String.Format(OutputMessages.TeamHasBeenPromoted, promotingTeamName);
    }

    public string ChampionshipRankings()
    {
        var rankedTeams = this.championship.Models
            .OrderByDescending(team => team.ChampionshipPoints)
            .ThenByDescending(team => team.PresentCondition)
            .ToList();

        var rankings = new StringBuilder();
        rankings.AppendLine("***Ranking Table***");

        for (int i = 0; i < rankedTeams.Count; i++)
        {
            var team = rankedTeams[i];
            rankings.AppendLine($"{i + 1}. Team: {team.Name} Points: {team.ChampionshipPoints}/{team.TeamManager?.Name ?? "No Manager"} - {team.TeamManager?.GetType().Name ?? "No Manager"} (Ranking: {team.TeamManager?.Ranking.ToString("F2") ?? "0.00"})");
        }

        return rankings.ToString().TrimEnd();
    }
}
