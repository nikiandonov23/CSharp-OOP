using System;
using System.Linq;
using System.Text;
using EDriveRent.Core.Contracts;
using EDriveRent.Models;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories;
using EDriveRent.Utilities.Messages;

namespace EDriveRent.Core;

public class Controller:IController
{
    private UserRepository users;
    private VehicleRepository vehicles;
    private RouteRepository routes ;
    private string[] validVehicleTypes = new[] { nameof(PassengerCar), nameof(CargoVan) };


    public Controller()
    {
        users = new UserRepository();
        vehicles = new VehicleRepository();
        routes = new RouteRepository();
    }


    public string RegisterUser(string firstName, string lastName, string drivingLicenseNumber)   //dava 14
    {
        if (users.FindById(drivingLicenseNumber)!=null)
        {
            return String.Format(OutputMessages.UserWithSameLicenseAlreadyAdded, drivingLicenseNumber);
        }

        User user = new User(firstName, lastName, drivingLicenseNumber);
        users.AddModel(user);
        return String.Format(OutputMessages.UserSuccessfullyAdded, firstName, lastName,drivingLicenseNumber);
    }
    public string UploadVehicle(string vehicleType, string brand, string model, string licensePlateNumber)    //dava 21
    {
        if (!validVehicleTypes.Contains(vehicleType))
        {
            return String.Format(OutputMessages.VehicleTypeNotAccessible, vehicleType);
        }

        if (vehicles.FindById(licensePlateNumber)!=null)
        {
            return String.Format(OutputMessages.LicensePlateExists, licensePlateNumber);
        }

        IVehicle vehicleToBEAdded = null;
        switch (vehicleType)
        {
            case $"{nameof(CargoVan)}":
                vehicleToBEAdded = new CargoVan(brand, model, licensePlateNumber);
                break;

            case $"{nameof(PassengerCar)}":
                vehicleToBEAdded = new PassengerCar(brand, model, licensePlateNumber);
                break;
        }

        vehicles.AddModel(vehicleToBEAdded);
        return String.Format(OutputMessages.VehicleAddedSuccessfully, brand, model, licensePlateNumber);

    }





    public string AllowRoute(string startPoint, string endPoint, double length)   //dava 29
    {
        var existingRoute1 = routes.GetAll()
            .FirstOrDefault(x => x.StartPoint == startPoint && x.EndPoint == endPoint && x.Length == length);
        if (existingRoute1!=null)
        {
            return String.Format(OutputMessages.RouteExisting, startPoint, endPoint, length);
        }

        var existingRoute2= routes.GetAll()
            .FirstOrDefault(x => x.StartPoint == startPoint && x.EndPoint == endPoint && x.Length < length);
        if (existingRoute2 != null)
        {
            return String.Format(OutputMessages.RouteIsTooLong,startPoint,endPoint);
        }

        IRoute newRoute = new Route(startPoint, endPoint, length, routes.GetAll().Count + 1);
        routes.AddModel(newRoute);

        var longerRoute = routes.GetAll()
            .FirstOrDefault(x => x.StartPoint == startPoint && x.EndPoint == endPoint && x.Length > length);

        if (longerRoute!=null)
        {
            longerRoute.LockRoute();
            
        }

        return String.Format(OutputMessages.NewRouteAdded, startPoint, endPoint, length);
    }



    public string MakeTrip(string drivingLicenseNumber, string licensePlateNumber, string routeId, bool isAccidentHappened)
    {
        var user = users.FindById(drivingLicenseNumber);
        if (user.IsBlocked)
        {
            return String.Format(OutputMessages.UserBlocked, drivingLicenseNumber);
        }

        var vehicle = vehicles.FindById(licensePlateNumber);
        if (vehicle.IsDamaged)
        {
            return String.Format(OutputMessages.VehicleDamaged, licensePlateNumber);
        }

        var route = routes.FindById(routeId);
        if (route.IsLocked)
        {
            return String.Format(OutputMessages.RouteLocked, routeId);
        }

        vehicle.Drive(route.Length);

        if (isAccidentHappened)
        {
            vehicle.ChangeStatus();  
            user.DecreaseRating();
            
        }
        else
        {
            user.IncreaseRating();  
        }

        
        return vehicle.ToString();
    }



    public string RepairVehicles(int count)
    {
        int repairedCount = 0;

       
        foreach (var vehicle in vehicles.GetAll())
        {
            if (vehicle.IsDamaged && repairedCount < count)
            {
                vehicle.ChangeStatus();  
                repairedCount++;
            }
        }

        return String.Format(OutputMessages.RepairedVehicles, repairedCount);
    }


    public string UsersReport()
    {
        StringBuilder result = new StringBuilder();

        result.AppendLine("*** E-Drive-Rent ***");

        var sortedUsers = users.GetAll()
            .OrderByDescending(x => x.Rating)
            .ThenBy(x => x.LastName)
            .ThenBy(x => x.FirstName).ToList();
        foreach (var usr in sortedUsers)
        {
            result.AppendLine(usr.ToString().TrimEnd());
        }
        return result.ToString().Trim();
    }
}