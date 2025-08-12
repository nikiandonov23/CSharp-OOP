using System;
using RobotService.Models.Contracts;
using RobotService.Utilities.Messages;

namespace RobotService.Models;

public abstract class Supplement:ISupplement
{
    private int interfaceStandard;
    private int batteryUsage;


    protected Supplement(int interfaceStandard, int batteryUsage)
    {
        this.InterfaceStandard = interfaceStandard;
        this.BatteryUsage = batteryUsage;
    }


    public int InterfaceStandard
    {
        get { return interfaceStandard; }
        private set
        {
            interfaceStandard = value;
        }
    }
    public int BatteryUsage
    {
        get { return batteryUsage; }
        private set
        {

            batteryUsage = value;
        }
    }
}