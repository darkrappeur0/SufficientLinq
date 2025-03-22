using GoldSavings.App.Model;
using GoldSavings.App.Client;
using GoldSavings.App.Services;
using System.Security.AccessControl;
namespace GoldSavings.App;

class Program
{
    static void Main(string[] args)
    {

        /*
        
            Start of initialization

        */
        #region Initialization

        Console.WriteLine("Hello, Gold Investor!");

        GoldDataService dataService = new GoldDataService();

        DateTime startDate2019 = new DateTime(2019,01,01);
        DateTime endDate2019 =new DateTime(2019,12,31);
        List<GoldPrice> goldPrices2019 = dataService.GetGoldPrices(startDate2019, endDate2019).GetAwaiter().GetResult();

        DateTime startDate2020 = new DateTime(2020,01,01);
        DateTime endDate2020 =new DateTime(2020,12,31);
        List<GoldPrice> goldPrices2020 = dataService.GetGoldPrices(startDate2020, endDate2020).GetAwaiter().GetResult();

        DateTime startDate2021 = new DateTime(2021,01,01);
        DateTime endDate2021 =new DateTime(2021,12,31);
        List<GoldPrice> goldPrices2021 = dataService.GetGoldPrices(startDate2021, endDate2021).GetAwaiter().GetResult();

        DateTime startDate2022 = new DateTime(2022,01,01);
        DateTime endDate2022 =new DateTime(2022,12,31);
        List<GoldPrice> goldPrices2022 = dataService.GetGoldPrices(startDate2022, endDate2022).GetAwaiter().GetResult();

        DateTime startDate2023 = new DateTime(2023,01,01);
        DateTime endDate2023 =new DateTime(2023,12,31);
        List<GoldPrice> goldPrices2023 = dataService.GetGoldPrices(startDate2023, endDate2023).GetAwaiter().GetResult();

        DateTime startDate2024 = new DateTime(2024,01,01);
        DateTime endDate2024 =new DateTime(2024,12,31);
        List<GoldPrice> goldPrices2024 = dataService.GetGoldPrices(startDate2024, endDate2024).GetAwaiter().GetResult();
        
        var combinedGoldPrices1 = goldPrices2019.Concat(goldPrices2020).ToList();
        var combinedGoldPrices2 = goldPrices2021.Concat(goldPrices2022).ToList();
        var combinedGoldPrices3 = goldPrices2023.Concat(goldPrices2024).ToList();

        var combinedGoldPricesSemi1 = combinedGoldPrices1.Concat(combinedGoldPrices2).ToList();
        var combinedGoldPricesAll = combinedGoldPricesSemi1.Concat(combinedGoldPrices3).ToList();


        var combinedGoldPricesSemi2 = combinedGoldPrices2.Concat(combinedGoldPrices3).ToList();
        var combinedGoldPricesAllminus2019 = goldPrices2020.Concat(combinedGoldPricesSemi2).ToList();

        #endregion 


        /*
        
                end of initilization
        
        */
        if (combinedGoldPricesAll.Count == 0)
        {
            Console.WriteLine("No data found. Exiting.");
            return;
        }

        Console.WriteLine($"Retrieved {combinedGoldPricesAll.Count} records. Ready for analysis.");

        /*
                Question 2.a
        */
        #region Question 2.a :

        var lowerPrices = goldPrices2024.OrderBy(p => p.Price).Take(3);
        var higherPrices = goldPrices2024.OrderByDescending(p => p.Price).Take(3);
        
        Console.WriteLine($"Here are the 3 highest prices: {string.Join(", ", higherPrices.Select(p => p.Price))}");
        Console.WriteLine($"Here are the 3 lowest prices: {string.Join(", ", lowerPrices.Select(p => p.Price))}");

        #endregion

        /*
            Question 2.b
        */ 
        #region Question 2.b

        var Janu2020Prices = goldPrices2020.Where(p=>p.Date.Year==2020 & p.Date.Month == 1).FirstOrDefault();
        var dayupper5per = goldPrices2020.Where(p=> p.Price > Janu2020Prices.Price * 1.05).Select(p=> new {p.Date}).ToList();
        Console.WriteLine($"Here are the differents days that the sales will win 5% more from they buy in January 2020 : {string.Join(",",dayupper5per)}");
        #endregion
        
        /*
            Question 2.c
        */ 
        #region Question 2.c

        var secondTenDates = combinedGoldPricesSemi1.Where(p =>p.Date.Year >= 2019 && p.Date.Year <= 2022)
        .OrderByDescending(p => p.Price).Skip(10).Take(3).Select(p=> p.Date).ToList();
        Console.WriteLine("");
        Console.WriteLine($"Here are the 11, 12 and 13 days that have the highest prices between 2019 - 2022 : ");
        foreach (var date in secondTenDates)
        {
            Console.WriteLine(date.ToShortDateString());
        }
        #endregion

        /*
            Question 2.d
        */ 

        #region Question 2.d

        GoldAnalysisService analysisService2020 = new GoldAnalysisService(goldPrices2020);
        var avgPrice2020 = analysisService2020.GetAveragePrice();

        GoldAnalysisService analysisService2023 = new GoldAnalysisService(goldPrices2023);
        var avgPrice2023 = analysisService2023.GetAveragePrice();

        GoldAnalysisService analysisService2024 = new GoldAnalysisService(goldPrices2024);
        var avgPrice2024 = analysisService2024.GetAveragePrice();

    

        GoldResultPrinter.PrintSingleValue(Math.Round(avgPrice2020, 2), $"Average Gold Price in {startDate2020.Year} Year");
        GoldResultPrinter.PrintSingleValue(Math.Round(avgPrice2023, 2), $"Average Gold Price in {startDate2023.Year} Year");
        GoldResultPrinter.PrintSingleValue(Math.Round(avgPrice2024, 2), $"Average Gold Price in {startDate2024.Year} Year");
        Console.WriteLine("");
        #endregion

        /*
            Question 2.e
        */ 
        #region Question 2.e

        var cheapestprice = combinedGoldPricesAllminus2019.OrderBy(p => p.Price).First();

        var highestprice = combinedGoldPricesAllminus2019.Where(p => p.Date > cheapestprice.Date).OrderByDescending(p => p.Price).First();

        double returnofinterest = ( (highestprice.Price-cheapestprice.Price)/cheapestprice.Price  )*100;
        
        Console.WriteLine($"Cheapest price at: {cheapestprice.Price} when : {cheapestprice.Date.ToShortDateString()} ");
        Console.WriteLine($"Highest price to sell: {highestprice.Price} when : {highestprice.Date.ToShortDateString()}");
        Console.WriteLine($"Return on Investment : {returnofinterest:F2}%");

        #endregion
        Console.WriteLine("\nGold Analyis Queries with LINQ Completed.");

    }
}
