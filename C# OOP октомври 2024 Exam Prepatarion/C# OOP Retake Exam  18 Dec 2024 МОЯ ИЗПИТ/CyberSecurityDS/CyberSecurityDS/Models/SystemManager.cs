using CyberSecurityDS.Models.Contracts;
using CyberSecurityDS.Repositories;
using CyberSecurityDS.Repositories.Contracts;

namespace CyberSecurityDS.Models;

public class SystemManager:ISystemManager
{
    private IRepository<ICyberAttack> cyberAttacks;
    private IRepository<IDefensiveSoftware> defensiveSoftwares;


    public SystemManager()
    {
        this.cyberAttacks = new CyberAttackRepository();
        this.defensiveSoftwares = new DefensiveSoftwareRepository();


    }


    public IRepository<ICyberAttack> CyberAttacks
    {
        get { return this.cyberAttacks; }
    }

    public IRepository<IDefensiveSoftware> DefensiveSoftwares
    {
        get { return this.defensiveSoftwares; }
    }
}