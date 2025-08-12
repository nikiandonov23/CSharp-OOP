using System;
using System.Collections.Generic;
using System.Text;
using RobotService.Models.Contracts;
using RobotService.Utilities.Messages;

namespace RobotService.Models;

public abstract class Robot : IRobot
{

    private string model;
    private int batteryCapacity;
    private int batteryLevel;
    private int convertionCapacityIndex;
    private List<int> interfaceStandards;


    public Robot(string model, int batteryCapacity, int convertionCapacityIndex)
    {
        this.Model = model;
        this.BatteryCapacity = batteryCapacity;
        this.ConvertionCapacityIndex = convertionCapacityIndex;
        interfaceStandards = new List<int>();
        BatteryLevel = BatteryCapacity;
    }


    public string Model
    {
        get { return model; }
        private set
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(ExceptionMessages.ModelNullOrWhitespace);
            }

            model = value;
        }
    }
    public int BatteryCapacity
    {
        get { return batteryCapacity; }
        private set
        {
            if (value<0)
            {
                throw new ArgumentException(ExceptionMessages.BatteryCapacityBelowZero);
            }

            batteryCapacity = value;
        }
    }
    public int BatteryLevel
    {
        get
        {
            return batteryLevel;
        }
        private set
        {
            batteryLevel = value;
        }
    }
    public int ConvertionCapacityIndex
    {
        get { return convertionCapacityIndex; }
        private set
        {
            convertionCapacityIndex = value;
        }
    }
    public IReadOnlyCollection<int> InterfaceStandards
    {
        get { return interfaceStandards.AsReadOnly(); }
    }

    
    
    
    
    public void Eating(int minutes)
    {
        int energyToProduce = this.ConvertionCapacityIndex * minutes;

        if (this.BatteryLevel + energyToProduce > this.BatteryCapacity)
        {
            this.BatteryLevel = this.BatteryCapacity;
        }
        else
        {
            this.BatteryLevel += energyToProduce;
        }
    }
    public void InstallSupplement(ISupplement supplement)
    {
        interfaceStandards.Add(supplement.InterfaceStandard);
        this.BatteryCapacity -= supplement.BatteryUsage;
        this.BatteryLevel-=supplement.BatteryUsage;
    }
    public bool ExecuteService(int consumedEnergy)
    {
        if (this.BatteryLevel>=consumedEnergy)
        {
            this.BatteryLevel-=consumedEnergy;
            return true;
        }
        return false;

    }





    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine($"{this.GetType().Name} {Model}:");
        result.AppendLine($"--Maximum battery capacity: {BatteryCapacity}");
        result.AppendLine($"--Current battery level: {BatteryLevel}");
        if (InterfaceStandards.Count>0)
        {
            result.AppendLine($"--Supplements installed: {String.Join(" ",InterfaceStandards)}");
        }
        else if (InterfaceStandards.Count==0)
        {
            result.AppendLine($"--Supplements installed: none");
        }



        return result.ToString().Trim();
    }
}