using System;
using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;

namespace EDriveRent.Models;

public class Route : IRoute
{
    private string startPoint;
    private string endPoint;
    private double length;
    private int routeId;
    private bool isLocked=false;


    public Route(string startPoint, string endPoint, double length, int routeId)
    {
        this.StartPoint = startPoint;
        this.EndPoint = endPoint;
        this.Length = length;
        this.RouteId = routeId;
    }


    public string StartPoint
    {
        get
        {
            return startPoint;
        }
        private set
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(ExceptionMessages.StartPointNull);
            }

            startPoint = value;
        }
    }
    public string EndPoint
    {
        get { return endPoint; }
        private set
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(ExceptionMessages.EndPointNull);
            }

            endPoint = value;
        }
    }
    public double Length
    {
        get { return length; }
        private set
        {
            if (value < 1)
            {
                throw new ArgumentException(ExceptionMessages.RouteLengthLessThanOne);
            }

            length = value;  

        }
    }
    public int RouteId
    {
        get
        {
            return routeId;
        }
        private set
        {
            routeId = value;
        }
    }
    public bool IsLocked
    {
        get
        {
            return isLocked;
        }
        private set
        {
            isLocked = value;
        }
    }



    public void LockRoute()
    {
        isLocked = true;
    }
}