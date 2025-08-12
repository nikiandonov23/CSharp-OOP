namespace InfluencerManagerApp.Models;

public class BusinessInfluencer:Influencer
{
    public BusinessInfluencer(string username, int followers) : base(username, followers, 3.0)
    {
    }

    public override int CalculateCampaignPrice()
    {
        return (int)Math.Floor(this.Followers * this.EngagementRate * 0.15);
    }
}