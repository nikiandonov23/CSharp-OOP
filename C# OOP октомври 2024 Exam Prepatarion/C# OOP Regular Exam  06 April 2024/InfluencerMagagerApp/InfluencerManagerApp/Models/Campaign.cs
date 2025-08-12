using InfluencerManagerApp.Models.Contracts;
using InfluencerManagerApp.Utilities.Messages;

namespace InfluencerManagerApp.Models;

public abstract  class Campaign:ICampaign
{
    private string brand;
    private double budget;
    private List<string> contributors;


    public Campaign(string brand, double budget)
    {
        this.Brand = brand;
        this.Budget = budget;
        contributors=new List<string>();
    }


    public string Brand
    {
        get { return brand; }
        private set
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(ExceptionMessages.BrandIsrequired);
            }

            brand = value;
        }
    }
    public double Budget
    {

        get
        {
            return budget;
        }
        private set
        {

            budget = value;
        }
    }
    public IReadOnlyCollection<string> Contributors
    {
        get { return contributors.AsReadOnly(); }
    }




    public void Gain(double amount)
    {
        this.Budget += amount;
    }

    public void Engage(IInfluencer influencer)
    {
        contributors.Add(influencer.Username);
        this.Budget -= influencer.CalculateCampaignPrice();
    }

    public override string ToString()
    {
        return $"{this.GetType().Name} - Brand: {Brand}, Budget: {Budget}, Contributors: {contributors.Count}";
    }
}