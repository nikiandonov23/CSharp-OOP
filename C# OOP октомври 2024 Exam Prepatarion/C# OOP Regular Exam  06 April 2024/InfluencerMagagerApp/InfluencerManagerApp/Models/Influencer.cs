using InfluencerManagerApp.Models.Contracts;
using InfluencerManagerApp.Utilities.Messages;

namespace InfluencerManagerApp.Models;

public abstract class Influencer:IInfluencer
{
    private string username;
    private int followers;
    private double engagementRate;
    private double income;
    private List<string> participations;


    public Influencer(string username, int followers, double engagementRate)
    {
        this.Username = username;
        this.Followers = followers;
        this.EngagementRate = engagementRate;
        this.Income = 0;
        this.participations = new List<string>();
    }


    public string Username
    {
        get { return username; }
        private set
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(ExceptionMessages.UsernameIsRequired);
            }

            username = value;
        }
    }
    public int Followers
    {
        get { return followers; }
        private set
        {
            if (value<0)
            {
                throw new ArgumentException(ExceptionMessages.FollowersCountNegative);
            }

            followers = value;
        }
    }
    public double EngagementRate
    {
        get { return engagementRate; }
        private set
        {
            
            engagementRate = value;
        }
    }
    public double Income    //tuka moje bi moje bez setter....
    {
        get
        {
            return income;
        }
        private set
        {
            income = value;
        }
    }  //SETVAME V CTOR-a da e =0;
    public IReadOnlyCollection<string> Participations
    {
        get { return participations.AsReadOnly(); }
    }


    public override string ToString()
    {
        return $"{Username} - Followers: {Followers}, Total Income: {Income}";
    }


    public void EarnFee(double amount)
    {
        Income += amount;
    }

    public void EnrollCampaign(string brand)
    {
        participations.Add(brand);
    }

    public void EndParticipation(string brand)
    {
        participations.Remove(brand);
    }

    public abstract int CalculateCampaignPrice();

}