using System.Diagnostics.CodeAnalysis;
using System.Text;
using InfluencerManagerApp.Core.Contracts;
using InfluencerManagerApp.Models;
using InfluencerManagerApp.Models.Contracts;
using InfluencerManagerApp.Repositories;
using InfluencerManagerApp.Repositories.Contracts;
using InfluencerManagerApp.Utilities.Messages;

namespace InfluencerManagerApp.Core;

public class Controller : IController
{
    private IRepository<IInfluencer> influencers = new InfluencerRepository();
    private IRepository<ICampaign> campaigns = new CampaignRepository();

    private string[] validInfluencerTypes = new[] { nameof(BusinessInfluencer), nameof(FashionInfluencer), nameof(BloggerInfluencer) };
    private string[] validCampaignTypes = new[] { nameof(ProductCampaign), nameof(ServiceCampaign) };

    public string RegisterInfluencer(string typeName, string username, int followers)
    {
        if (!validInfluencerTypes.Contains(typeName))
        {
            return String.Format(OutputMessages.InfluencerInvalidType, typeName);
        }

        if (influencers.FindByName(username) != null)
        {
            return String.Format(OutputMessages.UsernameIsRegistered, username, influencers.GetType().Name);
        }

        IInfluencer toBeAdded = null;
        if (typeName == nameof(BusinessInfluencer))
        {
            toBeAdded = new BusinessInfluencer(username, followers);
        }
        if (typeName == nameof(BloggerInfluencer))
        {
            toBeAdded = new BloggerInfluencer(username, followers);
        }
        if (typeName == nameof(FashionInfluencer))
        {
            toBeAdded = new FashionInfluencer(username, followers);
        }
        influencers.AddModel(toBeAdded);
        return String.Format(OutputMessages.InfluencerRegisteredSuccessfully, username);
    }   //18
    public string BeginCampaign(string typeName, string brand)
    {
        if (!validCampaignTypes.Contains(typeName))
        {
            return String.Format(OutputMessages.CampaignTypeIsNotValid, typeName);
        }

        if (campaigns.FindByName(brand)!=null)
        {
            return String.Format(OutputMessages.CampaignDuplicated, brand);
        }

        ICampaign toBeAdded = null;
        if (typeName==nameof(ProductCampaign))
        {
            toBeAdded = new ProductCampaign(brand);
        }

        if (typeName==nameof(ServiceCampaign))
        {
            toBeAdded = new ServiceCampaign(brand);
        }

        campaigns.AddModel(toBeAdded);
        return String.Format(OutputMessages.CampaignStartedSuccessfully, brand, typeName);
    }  // 24
    public string AttractInfluencer(string brand, string username)  //36
    {
        if (influencers.FindByName(username)==null)
        {
            return String.Format(OutputMessages.InfluencerNotFound, influencers.GetType().Name, username);
        }

        if (campaigns.FindByName(brand)==null)
        {
            return String.Format(OutputMessages.CampaignNotFound, brand);
        }

        var influencerToBeChecked = influencers.FindByName(username);  //da go iztrivam ne mi trqaa
        var campaignToBeChecked=campaigns.FindByName(brand);
        if (campaignToBeChecked.Contributors.Contains(username))
        {
            return String.Format(OutputMessages.InfluencerAlreadyEngaged, username, brand);
        }

        string currentInfluencerType = influencerToBeChecked.GetType().Name;
        string currentCampaignType=campaignToBeChecked.GetType().Name;



        bool isEligible = true;
        if (currentCampaignType == nameof(ProductCampaign))
        {
            
            if (currentInfluencerType != nameof(BusinessInfluencer) && currentInfluencerType != nameof(FashionInfluencer))
            {
                isEligible = false;
            }
        }
        else if (currentCampaignType == nameof(ServiceCampaign))
        {
            
            if (currentInfluencerType != nameof(BusinessInfluencer) && currentInfluencerType != nameof(BloggerInfluencer))
            {
                isEligible = false;
            }
        }

        if (!isEligible)
        {
            return String.Format(OutputMessages.InfluencerNotEligibleForCampaign, username, brand);
        }

        if (campaignToBeChecked.Budget<influencerToBeChecked.CalculateCampaignPrice())  //check the logic carefullyyy
        {
            return String.Format(OutputMessages.UnsufficientBudget, brand, username);
            
        }
        
        influencerToBeChecked.EarnFee(influencerToBeChecked.CalculateCampaignPrice());  //joinva
        influencerToBeChecked.EnrollCampaign(brand);  // joinva i updateva Participations
        campaignToBeChecked.Engage(influencerToBeChecked);  //updateva contributions i namalqva budget-a na campaniqta

        return String.Format(OutputMessages.InfluencerAttractedSuccessfully, username, brand);
    }
    public string FundCampaign(string brand, double amount)
    {
        if (campaigns.FindByName(brand)==null)
        {
            return String.Format(OutputMessages.InvalidCampaignToFund);
        }

        if (amount<=0)
        {
            return String.Format(OutputMessages.NotPositiveFundingAmount);
        }
        campaigns.FindByName(brand).Gain(amount);

        return String.Format(OutputMessages.CampaignFundedSuccessfully, brand, amount);
    }
    public string CloseCampaign(string brand)
    {
        if (campaigns.FindByName(brand)==null)
        {
            return String.Format(OutputMessages.InvalidCampaignToClose);
        }

        if (campaigns.FindByName(brand).Budget<=10000)
        {
            return String.Format(OutputMessages.CampaignCannotBeClosed, brand);
        }

        foreach (var influ in campaigns.FindByName(brand).Contributors)
        {
            influencers.FindByName(influ).EarnFee(2000);
            influencers.FindByName(influ).EndParticipation(brand);
        }

        var toBeRemoved = campaigns.FindByName(brand);
        campaigns.RemoveModel(toBeRemoved);
        return String.Format(OutputMessages.CampaignClosedSuccessfully, brand);

    }





    public string ConcludeAppContract(string username)
    {
        if (influencers.FindByName(username)==null)
        {
            return String.Format(OutputMessages.InfluencerNotSigned, username);
        }

        if (influencers.FindByName(username).Participations.Count>0)
        {
            return String.Format(OutputMessages.InfluencerHasActiveParticipations, username);
        }

        var toBeRemoved= influencers.FindByName(username);
        influencers.RemoveModel(toBeRemoved);
        return String.Format(OutputMessages.ContractConcludedSuccessfully, username);


    }

    public string ApplicationReport()
    {
        StringBuilder result = new StringBuilder();

        var orderedInfluencers = influencers.Models
            .OrderByDescending(x => x.Income) // Първо сортиране по доходи в низходящ ред
            .ThenByDescending(x => x.Followers) // След това по брой последователи в низходящ ред
            .ToList();




        foreach (var inlf in orderedInfluencers)
        {
            result.AppendLine(inlf.ToString().Trim()); 

            if (inlf.Participations.Count > 0)
            {
                result.AppendLine("Active Campaigns:"); 



                var sortedCampaigns = inlf.Participations
                    .Select(campaignName => campaigns.FindByName(campaignName)) 
                    .Where(campaign => campaign != null) 
                    .OrderBy(campaign => campaign.Brand) 
                    .ToList();



                foreach (var campaign in sortedCampaigns)
                {
                    result.AppendLine($"--{campaign.ToString().Trim()}");
                }
            }
        }

        return result.ToString().Trim();
    }
}