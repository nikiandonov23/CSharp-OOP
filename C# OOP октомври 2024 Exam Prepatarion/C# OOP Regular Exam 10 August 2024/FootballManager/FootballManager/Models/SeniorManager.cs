namespace FootballManager.Models;

public class SeniorManager:Manager
{
    public SeniorManager(string name) : base(name, 30)
    {
    }

    public override void RankingUpdate(double updateValue)
    {
        if (Ranking + updateValue  > 100)
        {
            Ranking = 100;

        }
        else if (Ranking + updateValue  < 0)
        {
            Ranking = 0;
        }
        else
        {
            Ranking += updateValue ;
        }
    }
}