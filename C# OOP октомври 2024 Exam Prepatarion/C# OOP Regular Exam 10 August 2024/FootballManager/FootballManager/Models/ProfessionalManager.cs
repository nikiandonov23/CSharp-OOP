namespace FootballManager.Models;

public class ProfessionalManager:Manager
{
    public ProfessionalManager(string name) : base(name, 60)
    {
    }

    public override void RankingUpdate(double updateValue)
    {
        if (Ranking + updateValue * 1.5 > 100)
        {
            Ranking = 100;

        }
        else if (Ranking + updateValue * 1.5 < 0)
        {
            Ranking = 0;
        }
        else
        {
            Ranking += updateValue * 1.5;
        }
    }
}