using System;
using System.Text;
using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;

namespace EDriveRent.Models;

public abstract class Vehicle:IVehicle
{
    private string brand;
    private string model;
    private double maxMileage;
    private string licensePlateNumber;
    private int batteryLevel=100;
    private bool isDamaged=false;


    public Vehicle(string brand, string model, double maxMileage, string licensePlateNumber)
    {
        this.Brand = brand;
        this.Model = model;
        this.MaxMileage = maxMileage;
        this.LicensePlateNumber = licensePlateNumber;
    }


    public string Brand
    {
        get { return brand; }
        private set
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(ExceptionMessages.BrandNull);
            }

            brand = value;
        }
    }
    public string Model
    {
        get { return model; }
        private set
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(ExceptionMessages.ModelNull);
            }

            model = value;
        }
    }
    public double MaxMileage
    {
        get
        {
            return maxMileage;
        }
        private set
        {
            maxMileage = value;
        }
    }
    public string LicensePlateNumber
    {
        get { return licensePlateNumber; }
        private set
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(ExceptionMessages.LicenceNumberRequired);
            }

            licensePlateNumber = value;
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
    public bool IsDamaged
    {
        get
        {
            return isDamaged;
        }
        private set
        {
            isDamaged = value;
        }
    }




    public void Drive(double mileage)
    {
       double percentigeToReduce=  mileage / MaxMileage * 100;
       if (this.GetType().Name== "CargoVan")
       {
          this.BatteryLevel -= (int)Math.Round(percentigeToReduce*1.05);
       }
       else
       {
           this.BatteryLevel -= (int)Math.Round(percentigeToReduce);
       }
    }
    public void Recharge()
    {
        this.BatteryLevel = 100;
    }
    public void ChangeStatus()
    {
        IsDamaged = !IsDamaged;
    }


    public override string ToString()
    {
        StringBuilder result = new StringBuilder();


        if (IsDamaged==false)
        {
            result.AppendLine(
                $"{Brand} {Model} License plate: {LicensePlateNumber} Battery: {BatteryLevel}% Status: OK");
        }
        else
        {
            result.AppendLine($"{Brand} {Model} License plate: {LicensePlateNumber} Battery: {BatteryLevel}% Status: damaged");
        }

        return result.ToString().Trim();
    }
}