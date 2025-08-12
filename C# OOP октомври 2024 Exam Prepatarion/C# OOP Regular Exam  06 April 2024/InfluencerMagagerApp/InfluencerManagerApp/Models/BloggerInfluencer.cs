namespace InfluencerManagerApp.Models;

public class BloggerInfluencer:Influencer
{
    public BloggerInfluencer(string username, int followers ) : base(username, followers, 2.0)
    {
    }

    public override int CalculateCampaignPrice()
    {
        return (int)Math.Floor(this.Followers * this.EngagementRate * 0.2);
    }
}