namespace FootballManager.Models;

public class AmateurManager:Manager
{
    public AmateurManager(string name) : base(name, 15)
    {
    }

    public override void RankingUpdate(double updateValue)
    {
        if (Ranking+updateValue*0.75>100)
        {
            Ranking = 100;
           
        }
        else if (Ranking+updateValue*0.75<0)
        {
            Ranking = 0;
        }
        else
        {
            Ranking += updateValue*0.75;
        }
    }
}