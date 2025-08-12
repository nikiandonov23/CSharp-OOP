using System.Text;
using NauticalCatchChallenge.Core.Contracts;
using NauticalCatchChallenge.Models;
using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Repositories;
using NauticalCatchChallenge.Utilities.Messages;

namespace NauticalCatchChallenge.Core;

public class Controller : IController
{
    private DiverRepository divers;
    private FishRepository fish;
    private string[] validDiverTypes = new[] { nameof(ScubaDiver), nameof(FreeDiver) };
    private string[] validFishTypes = new[] { nameof(ReefFish), nameof(PredatoryFish), nameof(DeepSeaFish) };


    public Controller()
    {
        this.divers = new DiverRepository();
        this.fish = new FishRepository();
    }

    public string DiveIntoCompetition(string diverType, string diverName)
    {
        if (!validDiverTypes.Contains(diverType))  //ako divera ne e validen
        {
            return String.Format(OutputMessages.DiverTypeNotPresented, diverType);
        }

        if (divers.GetModel(diverName)!=null)
        {
            return String.Format(OutputMessages.DiverNameDuplication, diverName, nameof(DiverRepository));
        }

        IDiver diver = null;
        if (diverType==nameof(ScubaDiver))
        {
            diver = new ScubaDiver(diverName);
        }
        else if (diverType == nameof(FreeDiver))
        {
            diver = new FreeDiver(diverName);
        }
        divers.AddModel(diver);
        return String.Format(OutputMessages.DiverRegistered, diverName, nameof(DiverRepository));

    }



    public string SwimIntoCompetition(string fishType, string fishName, double points)
    {
        if (!validFishTypes.Contains(fishType))
        {
            return String.Format(OutputMessages.FishTypeNotPresented, fishType);
        }

        if (fish.GetModel(fishName)!=null)
        {
            return String.Format(OutputMessages.FishNameDuplication, fishName, nameof(FishRepository));
        }

        IFish currentFish = null;
        if (fishType==nameof(ReefFish))
        {
            currentFish = new ReefFish(fishName, points);
        }
        if (fishType == nameof(PredatoryFish))
        {
            currentFish = new PredatoryFish(fishName, points);
        }
        if (fishType == nameof(DeepSeaFish))
        {
            currentFish = new DeepSeaFish(fishName, points);
        }
        fish.AddModel(currentFish);
        return String.Format(OutputMessages.FishCreated, fishName);
    }

    public string ChaseFish(string diverName, string fishName, bool isLucky)
    {
        if (divers.GetModel(diverName)==null)
        {
            return String.Format(OutputMessages.DiverNotFound, nameof(DiverRepository), diverName);
        }

        if (fish.GetModel(fishName)==null)
        {
            return String.Format(OutputMessages.FishNotAllowed, fishName);
        }

        if (divers.GetModel(diverName).HasHealthIssues==true)
        {
            return string.Format(OutputMessages.DiverHealthCheck, diverName);
        }

        if (divers.GetModel(diverName).OxygenLevel<fish.GetModel(fishName).TimeToCatch)
        {
            divers.GetModel(diverName).Miss(fish.GetModel(fishName).TimeToCatch);
            return String.Format(OutputMessages.DiverMisses, diverName, fishName);
        }

        else if (divers.GetModel(diverName).OxygenLevel==fish.GetModel(fishName).TimeToCatch)
        {
            if (isLucky)
            {
                divers.GetModel(diverName).Hit(fish.GetModel(fishName));
                return String.Format(OutputMessages.DiverHitsFish, diverName, fish.GetModel(fishName).Points,fishName);
            }
            divers.GetModel(diverName).Miss(fish.GetModel(fishName).TimeToCatch);
            return String.Format(OutputMessages.DiverMisses, diverName, fishName);
        }

        else 
        {
            divers.GetModel(diverName).Hit(fish.GetModel(fishName));
            return String.Format(OutputMessages.DiverHitsFish, diverName, fish.GetModel(fishName).Points, fishName);
        }
    }

    public string HealthRecovery()
    {
        List<IDiver> diversToRecover = divers.Models.Where(x => x.HasHealthIssues == true).ToList();

        foreach (var element in diversToRecover)
        {
            element.UpdateHealthStatus();
            element.RenewOxy();
        }

        return string.Format(OutputMessages.DiversRecovered, diversToRecover.Count);
    }

    public string DiverCatchReport(string diverName)
    {
        IDiver diverToBeReported = divers.GetModel(diverName);
        StringBuilder result=new StringBuilder();

        result.AppendLine($"Diver [ Name: {diverToBeReported.Name}, Oxygen left: {diverToBeReported.OxygenLevel}, Fish caught: {diverToBeReported.Catch.Count}, Points earned: {diverToBeReported.CompetitionPoints} ]");
        result.AppendLine("Catch Report:");

        

        foreach (var currentfFish in diverToBeReported.Catch)
        {

            string model = fish.GetModel(currentfFish).ToString();
            result.AppendLine(model);

        }

        return result.ToString().Trim();
    }

    public string CompetitionStatistics()
    {
        var orderedDivers = divers.Models
            .OrderByDescending(x => x.CompetitionPoints)
            .ThenByDescending(x => x.Catch.Count)
            .ThenBy(x => x.Name).ToList();

        StringBuilder result = new StringBuilder();
        result.AppendLine("**Nautical-Catch-Challenge**");
        foreach (var diverr in orderedDivers)
        {
            if (diverr.HasHealthIssues==false)
            {
                result.AppendLine(diverr.ToString());
            }
        }
        return result.ToString().Trim();
    }
}