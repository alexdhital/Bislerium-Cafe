using Bislerium_cafe.Data.Models;


namespace Bislerium_cafe.Data.Services
{
    class ReportGenrateTopSales
    {
        


        public static List<SalesModel> GenerateReport(bool dailyReport, DateTime selectedDate)
        {
            var ordersAll = OrderServices.RetrieveOrderData();
            List < OrderModel>  orders = new List< OrderModel>();

            foreach(var item in ordersAll)
            {
                if (item.IsPaid)
                {
                    orders.Add(item);

                }
            }
            var filteredOrders = dailyReport
                ? orders.Where(order => order.OrderDate.Date == selectedDate.Date && order.IsPaid==true)
                : orders.Where(order => order.OrderDate.Month == selectedDate.Month && order.OrderDate.Year == selectedDate.Year && order.IsPaid==true);

            // Calculate the frequency of each coffee type
            Dictionary<string, int> coffeeFrequency = new Dictionary<string, int>();
            Dictionary<string, int> addinFrequency = new Dictionary<string, int>();

            foreach (var order in filteredOrders)
            {
                // Coffee type frequency
                string coffeeName = order.Coffee.Name;
                if (!coffeeFrequency.ContainsKey(coffeeName))
                    coffeeFrequency[coffeeName] = 0;
                coffeeFrequency[coffeeName] += int.Parse( order.Quantity);

                // Add-in frequency
                foreach (var addin in order.Addins)
                {
                  
                        string addinName = addin.Name;
                        if (!addinFrequency.ContainsKey(addinName))
                            addinFrequency[addinName] = 0;
                        addinFrequency[addinName] += int.Parse(order.Quantity);
                  
               
                }
            }

            // Get the top 5 most frequently purchased coffee types
            var topCoffeeTypes = coffeeFrequency.OrderByDescending(kv => kv.Value).Take(5);

            // Get the top 5 most frequently purchased add-ins
            var topAddins = addinFrequency.OrderByDescending(kv => kv.Value).Take(5);

            // Create sales models for top coffee types
            List<SalesModel> topCoffeeSales = topCoffeeTypes.Select(coffeeType =>
                new SalesModel
                {
                  
                    Name = coffeeType.Key,
                    QuantitySold = coffeeType.Value,
                    ItemType = "Coffee"
                }
            ).ToList();

            // Create sales models for top add-ins
            List<SalesModel> topAddinSales = topAddins.Select(addin =>
                new SalesModel
                {
                    Name = addin.Key,
                    QuantitySold = addin.Value,
                    ItemType = "Addins"
                }
            ).ToList();

            // Combine and return the results
            List<SalesModel> topSales = topCoffeeSales.Concat(topAddinSales).ToList();

            return topSales;
        }
    }
}
