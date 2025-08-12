using System.Collections.Generic;
using System.Linq;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;

namespace EDriveRent.Repositories;

public class RouteRepository:IRepository<IRoute>
{
    private List<IRoute> routes;


    public RouteRepository()
    {
        routes = new List<IRoute>();
    }


    public void AddModel(IRoute model)
    {
        routes.Add(model);
    }

    public bool RemoveById(string identifier)
    {
        var routeToBERemoved = routes.FirstOrDefault(x => x.RouteId == int.Parse(identifier));
        if (routeToBERemoved != null)
        {
            routes.Remove(routeToBERemoved);
            return true;
        }
        return false;
    }

    public IRoute FindById(string identifier)
    {
        var routeToBeFound = routes.FirstOrDefault(x => x.RouteId == int.Parse(identifier));
        if (routeToBeFound != null)
        {
            
            return routeToBeFound;
        }
        return null;
    }

    public IReadOnlyCollection<IRoute> GetAll()
    {
        return routes.AsReadOnly();
    }
}