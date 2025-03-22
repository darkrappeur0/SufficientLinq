using GoldSavings.App.Model;
using GoldSavings.App.Client;
using GoldSavings.App.Services;
using System.Security.AccessControl;
namespace GoldSavings.App;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, Gold Investor!");

        // Step 1: Get gold prices
        GoldDataService dataService = new GoldDataService();
        DateTime startDate = new DateTime(2024,09,18);
        DateTime endDate = DateTime.Now;
        List<GoldPrice> goldPrices = dataService.GetGoldPrices(startDate, endDate).GetAwaiter().GetResult();

        

        if (goldPrices.Count == 0)
        {
            Console.WriteLine("No data found. Exiting.");
            return;
        }

        Console.WriteLine($"Retrieved {goldPrices.Count} records. Ready for analysis.");
        //1)Select prices from gold OrderBy ASC LIMITS 3 Union Select prices from gold OrderBy DESC Limits 3;
        var higherPrices = goldPrices.OrderBy(p => p.Price).Take(3);
        var lowerPrices = goldPrices.OrderByDescending(p => p.Price).Take(3);
        
    Console.WriteLine($"Here are the 3 lowest prices: {string.Join(", ", higherPrices.Select(p => p.Price))}");
    Console.WriteLine($"Here are the 3 highest prices: {string.Join(", ", lowerPrices.Select(p => p.Price))}");
        // 2) Select date from gold where 


        //3)
        // Step 2: Perform analysis
        GoldAnalysisService analysisService = new GoldAnalysisService(goldPrices);
        var avgPrice = analysisService.GetAveragePrice();

        // Step 3: Print results
        GoldResultPrinter.PrintSingleValue(Math.Round(avgPrice, 2), "Average Gold Price Last Half Year");

        Console.WriteLine("\nGold Analyis Queries with LINQ Completed.");

    }
}
