using System.Text;
using CarDealership.Core.Contracts;
using CarDealership.Models;
using CarDealership.Models.Contracts;
using CarDealership.Utilities.Messages;

namespace CarDealership.Core;

public class Controller : IController
{
    private IDealership dealership = new Dealership();
    private string[] validCusumerTypes = new[] { nameof(IndividualClient), nameof(LegalEntityCustomer) };
    private string[] validVehicleTypes = new[] { nameof(Truck), nameof(SUV), nameof(SaloonCar) };






    public string AddCustomer(string customerTypeName, string customerName)    //dava 30
    {
        if (!validCusumerTypes.Contains(customerTypeName))
        {
            return String.Format(OutputMessages.InvalidType, customerTypeName);
        }

        if (dealership.Customers.Get(customerName) != null)
        {
            return String.Format(OutputMessages.CustomerAlreadyAdded, customerName);
        }

        ICustomer newCustomer = null;
        switch (customerTypeName)
        {
            case $"{nameof(IndividualClient)}":
                newCustomer = new IndividualClient(customerName);
                break;

            case $"{nameof(LegalEntityCustomer)}":
                newCustomer = new LegalEntityCustomer(customerName);
                break;
        }

        dealership.Customers.Add(newCustomer);
        return String.Format(OutputMessages.CustomerAddedSuccessfully, customerName);

    }
    public string AddVehicle(string vehicleTypeName, string model, double price)
    {
        if (!validVehicleTypes.Contains(vehicleTypeName))
        {
            return String.Format(OutputMessages.InvalidType, vehicleTypeName);
        }

        if (dealership.Vehicles.Get(model) != null)
        {
            return String.Format(OutputMessages.VehicleAlreadyAdded, model);
        }


        IVehicle newVehicle = null;
        switch (vehicleTypeName)
        {
            case $"{nameof(SaloonCar)}":
                newVehicle = new SaloonCar(model, price);

                break;


            case $"{nameof(SUV)}":
                newVehicle = new SUV(model, price);

                break;



            case $"{nameof(Truck)}":
                newVehicle = new Truck(model, price);
                break;

        }


        dealership.Vehicles.Add(newVehicle);

        return String.Format(OutputMessages.VehicleAddedSuccessfully, vehicleTypeName, model,
            newVehicle.Price.ToString("F2"));

    }   //дава 37
    public string PurchaseVehicle(string vehicleTypeName, string customerName, double budget)
    {
        if (!dealership.Customers.Exists(customerName))
        {
            return String.Format(OutputMessages.CustomerNotFound, customerName);
        }


        bool isOffered = false;
        foreach (var vhcl in dealership.Vehicles.Models)
        {
            if (vhcl.GetType().Name == vehicleTypeName)
            {
                isOffered = true;
                break;
            }
        }

        if (!isOffered)
        {
            return String.Format(OutputMessages.VehicleTypeNotFound, vehicleTypeName);
        }


        var custumerToBuy = dealership.Customers.Get(customerName);
        bool isEligable = false;
        if (custumerToBuy.GetType().Name == nameof(IndividualClient))
        {
            if (vehicleTypeName == nameof(SaloonCar) || vehicleTypeName == nameof(SUV))
            {
                isEligable = true;
            }



        }
        if (custumerToBuy.GetType().Name == nameof(LegalEntityCustomer))
        {
            if (vehicleTypeName == nameof(SUV) || vehicleTypeName == nameof(Truck))
            {
                isEligable = true;
            }
        }

        if (!isEligable)
        {
            return String.Format(OutputMessages.CustomerNotEligibleToPurchaseVehicle, customerName, vehicleTypeName);
        }






        var vehiclesFromTheCorrectType = dealership.Vehicles.Models
            .Where(x => x.GetType().Name == vehicleTypeName)
            .Where(x=>x.Price<=budget)
            .ToList();
        if (vehiclesFromTheCorrectType.Count==0 )
        {
            return String.Format(OutputMessages.BudgetIsNotEnough, customerName, vehicleTypeName);
        }

        var sortedVehiclesByPrice=vehiclesFromTheCorrectType
            .OrderByDescending(x => x.Price).ToList();
        var vehicleToBeAdded = sortedVehiclesByPrice.First();

        custumerToBuy.BuyVehicle(vehicleToBeAdded.Model);
        vehicleToBeAdded.SellVehicle(customerName);

        return String.Format(OutputMessages.VehiclePurchasedSuccessfully, customerName, vehicleToBeAdded.Model);

    }  //super sme

    public string CustomerReport()
    {
        StringBuilder result = new StringBuilder();

        result.AppendLine("Customer Report:");

        var orderedCustomers = dealership.Customers.Models
            .OrderBy(c => c.Name)
            .ToList();

        foreach (var customer in orderedCustomers)
        {
            result.AppendLine($"{customer.ToString().Trim()}");

            var purchasedVehicles = customer.Purchases.OrderBy(v => v).ToList();

            if (purchasedVehicles.Count == 0)
            {
                result.AppendLine("-Models:");
                result.AppendLine("--none");
            }
            else
            {
                result.AppendLine("-Models:");
                foreach (var vehicleModel in purchasedVehicles)
                {
                    result.AppendLine($"--{vehicleModel}");
                }
            }
        }

        return result.ToString().TrimEnd(); 
    }


    public string SalesReport(string vehicleTypeName)
    {
        StringBuilder result = new StringBuilder();

        var vehiclesOfType = dealership.Vehicles.Models
            .Where(v => v.GetType().Name == vehicleTypeName)
            .OrderBy(v => v.Model) 
            .ToList();

        if (vehiclesOfType.Count == 0)
        {
            return $"No vehicles of type {vehicleTypeName} found in the dealership.";
        }

        result.AppendLine($"{vehicleTypeName} Sales Report:");

        foreach (var vehicle in vehiclesOfType)
        {
            result.AppendLine($"--{vehicle.ToString().Trim()}");
        }

        int totalCountSoldFromType = vehiclesOfType.Sum(v => v.SalesCount);

        result.AppendLine($"-Total Purchases: {totalCountSoldFromType}");

        return result.ToString().TrimEnd(); 
    }

}