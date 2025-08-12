using System.Runtime.CompilerServices;
using System.Text;
using BlackFriday.Core.Contracts;
using BlackFriday.Models;
using BlackFriday.Models.Contracts;
using BlackFriday.Utilities.Messages;

namespace BlackFriday.Core;

public class Controller:IController
{
    private IApplication application = new Application();
    private int allowedAdminRoles = 0;
    private string[] validProductTypes = new[] { nameof(Item), nameof(Service) };




    

    


    public string RegisterUser(string userName, string email, bool hasDataAccess)
    {
        if (application.Users.Exists(userName))
        {
            return String.Format(OutputMessages.UserAlreadyRegistered, userName);
        }

        if (application.Users.Models.FirstOrDefault(x=>x.Email==email)!=null)
        {
            return String.Format(OutputMessages.SameEmailIsRegistered, email);
        }

        
        if (hasDataAccess==true)
        {
            if (allowedAdminRoles>=2)
            {
                return String.Format(OutputMessages.AdminCountLimited);
            }

            IUser newUser = new Admin(userName, email);
            allowedAdminRoles++;
            application.Users.AddNew(newUser);
            return String.Format(OutputMessages.AdminRegistered, userName);
        }

        IUser newClientUser = new Client(userName, email);
        application.Users.AddNew(newClientUser);
        return String.Format(OutputMessages.ClientRegistered, userName);

    }
    public string AddProduct(string productType, string productName, string userName, double basePrice)
    {
        if (!validProductTypes.Contains(productType))
        {
            return String.Format(OutputMessages.ProductIsNotPresented, productType);
        }

        if (application.Products.Exists(productName))
        {
            return String.Format(OutputMessages.ProductNameDuplicated, productName);
        }

        if (!application.Users.Exists(userName) || application.Users.GetByName(userName).GetType().Name==nameof(Client) )
        {
            return String.Format(OutputMessages.UserIsNotAdmin, userName);
        }

        IProduct newProduct = null;
        if (productType==nameof(Service))
        {
            newProduct = new Service(productName, basePrice);
        }
        else if (productType==nameof(Item))
        {
            newProduct = new Item(productName, basePrice);
        }
        application.Products.AddNew(newProduct);
        return String.Format(OutputMessages.ProductAdded, productType, productName, basePrice.ToString("F2"));
    }
    public string UpdateProductPrice(string productName, string userName, double newPriceValue)
    {
        if (!application.Products.Exists(productName))
        {
            return String.Format(OutputMessages.ProductDoesNotExist, productName);
        }

        if (!application.Users.Exists(userName) || application.Users.GetByName(userName).GetType().Name==nameof(Client) )
        {
            return String.Format(OutputMessages.UserIsNotAdmin, userName);
        }

        IProduct productToBeUpdated = application.Products.GetByName(productName);
        var oldPrice = productToBeUpdated.BasePrice;
        productToBeUpdated.UpdatePrice(newPriceValue);
        return String.Format(OutputMessages.ProductPriceUpdated, productName, oldPrice.ToString("F2"), newPriceValue.ToString("F2"));
    }
    public string PurchaseProduct(string userName, string productName, bool blackFridayFlag)
    {
        if (!application.Users.Exists(userName)  ||  application.Users.GetByName(userName).GetType().Name==nameof(Admin))
        {
            return String.Format(OutputMessages.UserIsNotClient, userName);
        }

        if (!application.Products.Exists(productName))
        {
            return String.Format(OutputMessages.ProductDoesNotExist, productName);
        }

        if (application.Products.Exists(productName) && application.Products.GetByName(productName).IsSold)
        {
            return String.Format(OutputMessages.ProductOutOfStock, productName);
        }

        IProduct productToBePurchased = application.Products.GetByName(productName);
        Client client = application.Users.GetByName(userName) as Client;

        client.PurchaseProduct(productName,blackFridayFlag);
        productToBePurchased.ToggleStatus();


        double priceOfThePurchase = productToBePurchased.BasePrice;
        if (blackFridayFlag)
        {
            priceOfThePurchase = productToBePurchased.BlackFridayPrice;
        }

        return String.Format(OutputMessages.ProductPurchased, userName, productName, priceOfThePurchase.ToString("F2"));



    }






    public string RefreshSalesList(string userName)
    {
        if (!application.Users.Exists(userName)   || application.Users.GetByName(userName).GetType().Name==nameof(Client))
        {
            return String.Format(OutputMessages.UserIsNotAdmin, userName);
        }

        int count = 0;
        foreach (var prodct in application.Products.Models.Where(x=>x.IsSold==true))
        {
            prodct.ToggleStatus();
            count++;
        }

        return String.Format(OutputMessages.SalesListRefreshed, count);
    }



    public string ApplicationReport()
    {
        StringBuilder result = new StringBuilder();

        // Извеждаме администраторите, подредени по име
        result.AppendLine("Application administration:");
        foreach (var userAdmin in application.Users.Models.Where(x => x.HasDataAccess == true).OrderBy(x => x.UserName))
        {
            result.AppendLine(userAdmin.ToString());
        }

        // Извеждаме клиентите, подредени по име
        result.AppendLine("Clients:");
        foreach (var client in application.Users.Models.Where(x => !x.HasDataAccess).OrderBy(x => x.UserName))
        {
            var clientPurchases = (client as Client).Purchases;

            // Проверяваме дали клиентът има покупки с Black Friday отстъпка
            var blackFridayPurchases = clientPurchases.Where(p => p.Value == true).ToList();

            // Ако има Black Friday покупки, добавяме тази информация
            if (blackFridayPurchases.Any())
            {
                result.AppendLine($"{client}");
                result.AppendLine($"-Black Friday Purchases: {blackFridayPurchases.Count}");
                foreach (var purchase in blackFridayPurchases)
                {
                    result.AppendLine($"--{purchase.Key}");
                }
            }
        }

        // Връщаме резултата като текст
        return result.ToString().Trim(); // премахваме последната нова линия
    }

}