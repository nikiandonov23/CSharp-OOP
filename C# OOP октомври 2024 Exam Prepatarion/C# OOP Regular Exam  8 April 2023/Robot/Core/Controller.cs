using System;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using RobotService.Core.Contracts;
using RobotService.Models;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using RobotService.Utilities.Messages;

namespace RobotService.Core;

public class Controller : IController
{
    private SupplementRepository supplements = new SupplementRepository();
    private RobotRepository robots = new RobotRepository();
    private string[] validTypeRobots = new[] { nameof(DomesticAssistant), nameof(IndustrialAssistant) };
    private string[] validSupplementTypes = new[] { nameof(SpecializedArm), nameof(LaserRadar) };









    public string CreateRobot(string model, string typeName)   //dava 28
    {
        if (!validTypeRobots.Contains(typeName))
        {
            return String.Format(OutputMessages.RobotCannotBeCreated, typeName);
        }

        IRobot robot = null;
        switch (typeName)
        {
            case $"{nameof(DomesticAssistant)}":
                robot = new DomesticAssistant(model);
                break;

            case $"{nameof(IndustrialAssistant)}":
                robot = new IndustrialAssistant(model);
                break;
        }
        robots.AddNew(robot);
        return String.Format(OutputMessages.RobotCreatedSuccessfully, typeName, model);
    }
    public string CreateSupplement(string typeName)
    {
        if (!validSupplementTypes.Contains(typeName))
        {
            return String.Format(OutputMessages.SupplementCannotBeCreated, typeName);
        }

        ISupplement supplement = null;
        switch (typeName)
        {
            case $"{nameof(SpecializedArm)}":
                supplement = new SpecializedArm();
                break;

            case $"{nameof(LaserRadar)}":
                supplement = new LaserRadar();
                break;
        }
        supplements.AddNew(supplement);
        return String.Format(OutputMessages.SupplementCreatedSuccessfully, typeName);
    } //дава 20
    public string UpgradeRobot(string model, string supplementTypeName)
    {
        var currentSupplement = supplements.Models()
            .FirstOrDefault(x => x.GetType().Name == supplementTypeName);

        if (currentSupplement == null)
        {
            return String.Format(OutputMessages.SupplementCannotBeCreated, supplementTypeName);
        }

        int interfaceValue = currentSupplement.InterfaceStandard;

        var robotsNotSupportingInterface = robots.Models()
            .Where(x => !x.InterfaceStandards.Contains(interfaceValue) && x.Model == model)
            .ToList();

        if (!robotsNotSupportingInterface.Any())
        {
            return String.Format(OutputMessages.AllModelsUpgraded, model);
        }

        var robotToUpgrade = robotsNotSupportingInterface.First();

        robotToUpgrade.InstallSupplement(currentSupplement);

        supplements.RemoveByName(supplementTypeName);

        return String.Format(OutputMessages.UpgradeSuccessful, model, supplementTypeName);
    }    //dava 19




    public string PerformService(string serviceName, int intefaceStandard, int totalPowerNeeded)
    {
        var robotsSupportingInterface = robots.Models()
            .Where(x => x.InterfaceStandards.Contains(intefaceStandard));

        if (!robotsSupportingInterface.Any())
        {
            return string.Format(OutputMessages.UnableToPerform, intefaceStandard);
        }

        var orderedRobots = robotsSupportingInterface.OrderByDescending(x => x.BatteryLevel).ToList();
        var batterySum = orderedRobots.Sum(x => x.BatteryLevel);

        if (batterySum < totalPowerNeeded)
        {
            return string.Format(OutputMessages.MorePowerNeeded, serviceName, $"{totalPowerNeeded - batterySum}");
        }

        int counter = 0;

        foreach (var robot in orderedRobots)
        {
            if (totalPowerNeeded <= 0)
            {
                break; 
            }

            if (robot.BatteryLevel >= totalPowerNeeded)
            {
                robot.ExecuteService(totalPowerNeeded); 
                counter++;
                break; 
            }
            else
            {
                totalPowerNeeded -= robot.BatteryLevel; 
                robot.ExecuteService(robot.BatteryLevel); 
                counter++; 
            }
        }

        return string.Format(OutputMessages.PerformedSuccessfully, serviceName, counter);

    }  //dava mnogo :D





    public string RobotRecovery(string model, int minutes)
    {
        var robotsToBeFeeded = robots.Models()
            .Where(x => x.Model == model && x.BatteryLevel < x.BatteryCapacity / 2)
            .ToList();

        foreach (var robot in robotsToBeFeeded)
        {
            robot.Eating(minutes);
        }

        return String.Format(OutputMessages.RobotsFed, robotsToBeFeeded.Count);
    }

   

    public string Report()
    {
        var robotsToBeReported=robots.Models()
            .OrderByDescending(x=>x.BatteryLevel)
            .ThenBy(x=>x.BatteryCapacity)
            .ToList();

        StringBuilder result = new StringBuilder();
        foreach (var robot in robotsToBeReported)
        {
            result.AppendLine(robot.ToString().Trim());
        }

        return result.ToString().Trim();
    }
}