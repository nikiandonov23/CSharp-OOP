using System.Text;
using HighwayToPeak.Core.Contracts;
using HighwayToPeak.Models;
using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Repositories;
using HighwayToPeak.Utilities.Messages;

namespace HighwayToPeak.Core;

public class Controller : IController
{
    private PeakRepository peaks = new PeakRepository();
    private ClimberRepository climbers = new ClimberRepository();
    private BaseCamp baseCamp = new BaseCamp();
    private string[] acceptedValuePeaks = new[] { "Extreme", "Hard", "Moderate" };



    public string AddPeak(string name, int elevation, string difficultyLevel)
    {
        if (peaks.Get(name) != null)
        {
            return String.Format(OutputMessages.PeakAlreadyAdded, name);
        }

        if (!acceptedValuePeaks.Contains(difficultyLevel))
        {
            return String.Format(OutputMessages.PeakDiffucultyLevelInvalid, difficultyLevel);
        }

        IPeak currentPeak = new Peak(name, elevation, difficultyLevel);
        peaks.Add(currentPeak);
        return String.Format(OutputMessages.PeakIsAllowed, name, nameof(PeakRepository));
    }       //dava 28
    public string NewClimberAtCamp(string name, bool isOxygenUsed)
    {
        if (climbers.Get(name) != null)
        {
            return String.Format(OutputMessages.ClimberCannotBeDuplicated, name, nameof(ClimberRepository));
        }

        IClimber currentClimber = null;
        if (isOxygenUsed)
        {
            currentClimber = new OxygenClimber(name);
        }

        if (!isOxygenUsed)
        {
            currentClimber = new NaturalClimber(name);
        }

        climbers.Add(currentClimber);
        baseCamp.ArriveAtCamp(currentClimber.Name);
        return String.Format(OutputMessages.ClimberArrivedAtBaseCamp, name);


    }   // 18 точки
    public string AttackPeak(string climberName, string peakName)
    {
        if (climbers.Get(climberName) == null)
        {
            return String.Format(OutputMessages.ClimberNotArrivedYet, climberName);
        }

        if (peaks.Get(peakName) == null)
        {
            return String.Format(OutputMessages.PeakIsNotAllowed, peakName);
        }
        


        if (!baseCamp.Residents.Contains(climberName))
        {
            return String.Format(OutputMessages.ClimberNotFoundForInstructions, climberName, peakName);
        }

        var peakToAttack = peaks.Get(peakName);
        var climberWhoAttacks = climbers.Get(climberName);

        if (peakToAttack.DifficultyLevel == "Extreme" && climberWhoAttacks.GetType().Name == nameof(NaturalClimber))
        {
            return String.Format(OutputMessages.NotCorrespondingDifficultyLevel, climberName, peakName);
        }

        baseCamp.LeaveCamp(climberName);  //climber leaves the baseCamp
        
        climberWhoAttacks.Climb(peakToAttack);
        if (climberWhoAttacks.Stamina <= 0)
        {
            return String.Format(OutputMessages.NotSuccessfullAttack, climberName);
        }
        baseCamp.ArriveAtCamp(climberName);
        return String.Format(OutputMessages.SuccessfulAttack, climberName, peakName);
    }





    public string CampRecovery(string climberName, int daysToRecover)
    {
        if (!baseCamp.Residents.Contains(climberName))
        {
            return String.Format(OutputMessages.ClimberIsNotAtBaseCamp, climberName);
        }

        if ( climbers.Get(climberName).Stamina==10)
        {
            return String.Format(OutputMessages.NoNeedOfRecovery, climberName);
        }

        climbers.Get(climberName).Rest(daysToRecover);
        return String.Format(OutputMessages.ClimberRecovered, climberName, daysToRecover);
    }

    public string BaseCampReport()
    {
        StringBuilder result = new StringBuilder();
        if (baseCamp.Residents.Count>0)
        {
            result.AppendLine("BaseCamp residents:");

            foreach (var name in baseCamp.Residents)
            {
                string climberName = name;
                var climberDetails = climbers.Get(name);
                result.AppendLine($"Name: {climberDetails.Name}, Stamina: {climberDetails.Stamina}, Count of Conquered Peaks: {climberDetails.ConqueredPeaks.Count}");
            }

            return result.ToString().Trim();
        }

        result.AppendLine("BaseCamp is currently empty.");
        return result.ToString().Trim();
    }

    public string OverallStatistics()
    {
       
        var sortedClimbers = climbers.All
            .OrderByDescending(x => x.ConqueredPeaks.Count) 
            .ThenBy(x => x.Name) 
            .ToList();



        
        var result = new StringBuilder();
        result.AppendLine("***Highway-To-Peak***");

        

        foreach (var climber in sortedClimbers)
        {
            result.AppendLine(climber.ToString()); 



           
            var sortedPeaks = climber.ConqueredPeaks
                .Select(peakName => peaks.Get(peakName)) 
                .OrderByDescending(p => p.Elevation) 
                .ToList();

          




            foreach (var peak in sortedPeaks)
            {
                result.AppendLine(peak.ToString()); 
            }
        }

        return result.ToString().Trim(); 
    }

}