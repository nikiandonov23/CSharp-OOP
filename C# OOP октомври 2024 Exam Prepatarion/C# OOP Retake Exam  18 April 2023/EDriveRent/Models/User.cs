using System;
using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;

namespace EDriveRent.Models;

public class User:IUser
{
    private string firstName;
    private string lastName;
    private double rating=0;  //инициал валю =0
    private string drivingLicenseNumber;
    private bool isBlocked=false;


    public User(string firstName, string lastName, string drivingLicenseNumber)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.DrivingLicenseNumber = drivingLicenseNumber;
    }


    public string FirstName
    {
        get
        {
            return firstName;
        }
        private set
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(ExceptionMessages.FirstNameNull);
            }

            firstName = value;
        }
    }
    public string LastName
    {
        get { return lastName; }
        private set
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(ExceptionMessages.LastNameNull);
            }

            lastName = value;
        }
    }
    public string DrivingLicenseNumber
    {
        get { return drivingLicenseNumber; }
        private set
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(ExceptionMessages.DrivingLicenseRequired);
            }

            drivingLicenseNumber = value;
        }
    }
    public double Rating
    {
        get
        {
            return rating;
        }
        private set
        {
            if (value>10.0)
            {
                rating = 10.0;
            }

            else if (value<0.0)
            {
                rating = 0.0;
            }
            else
            {
                rating = value;

            }
        }
    }   // moje da ima tuka 4ikiriki :D
    public bool IsBlocked
    {
        get
        {
            return isBlocked;
        }
        private set
        {
            isBlocked = value;
        }
    }



    public void IncreaseRating()
    {
        Rating += 0.5;
    }

    public void DecreaseRating()
    {
        Rating -=2.0;
        if (this.Rating<=0)
        {
            this.isBlocked = true;
        }
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName} Driving license: {drivingLicenseNumber} Rating: {rating}";
    }
}