
using Bislerium_cafe.Data.Models;
using Bislerium_cafe.Data.Utils;
using Newtonsoft.Json;

namespace Bislerium_cafe.Data.Services;

public class CoffeeService
{
    // Saves lists of hobby data Injected to a JSON file.
    public static void SaveCoffeeToJson(List<Coffee> coffee)
    {
        // Gets the file path where form data will be stored from ApplicationFilePath method
        // in FormUtils class in Utils Folder and stores it in the variable filePath.
        string filePath = FormUtils.CoffeeFilePath();

        // Serialize the list of hobbies to JSON format with formatting Indented and store it in Variable jsonData
        string jsonData = JsonConvert.SerializeObject(coffee, Formatting.Indented);

        // Write the JSON data to the file given from filePath variable and data from jsonData variable.
        File.WriteAllText(filePath, jsonData);
    }

    //This method Injects the data Into the Json File Manually by creating the multiple objects and Passing it to the list only if the data inside the file is empty.
    public static void InjectCoffeeData()
    {
        // Gets the file path where hobby data will be stored from HobbiesFilePath method
        // in FormUtils class in Utils Folder and stores it in the variable filePath.
        string filePath = FormUtils.CoffeeFilePath();

        // Read existing data from the file and store it in variable existingData
        var existingData = File.ReadAllText(filePath);

        // If the file is empty, injects a list of sample hobby data in a object of List<Hobby> called sampleHobbies first and saves it in a Json File by calling method SaveHobbiesToJson.
        if (string.IsNullOrEmpty(existingData))
        {
            List<Coffee> sampleCoffee = new List<Coffee>
            {
                new Coffee { Name = "Cortadoaa", Price="50" },
                new Coffee { Name = "Espressooo", Price="80" },
                new Coffee { Name = "Latteee" , Price = "90"},
                new Coffee { Name = "Mocha" , Price = "70"},
                new Coffee { Name = "Macchiato" , Price = "80"},
                new Coffee { Name = "Americano" , Price = "90"},
                new Coffee { Name = "Flat White" , Price = "110"},
                new Coffee { Name = "Affogato" , Price = "120"},
                new Coffee { Name = "Irish Coffee" , Price = "170"},
                new Coffee { Name = "Vienna Coffee" , Price = "730"},

            };
            SaveCoffeeToJson(sampleCoffee); // Save the sample hobby data to the JSON file by calling SaveHobbiesToJson Method and passing sampleHobbies as it Argument.
        }
    }

    // Retrieves hobby data from the JSON file.
    public static List<Coffee> RetrieveCoffeeData()
    {
        // Gets the file path where hobby data is stored from HobbiesFilePath method
        // in FormUtils class in Utils Folder and stores it in the variable filePath.
        string filePath = FormUtils.CoffeeFilePath();
        try
        {
            string existingJsonData = File.ReadAllText(filePath); // Read existing JSON data from the file.

            // If the existing JSON data is empty, return an empty list;
            // otherwise, deserialize the data into a list of Hobby objects.
            if (string.IsNullOrEmpty(existingJsonData))
            {
                return new List<Coffee>();
            }
            return JsonConvert.DeserializeObject<List<Coffee>>(existingJsonData);
        }
        catch (Exception ex)
        {
            // Handle exceptions by displaying an alert with the error message.
            Console.WriteLine($"Error reading JSON file: {ex.Message}");
            return new List<Coffee>();
        }
    }

    // Retrieves a specific hobby by its Id.
    public static Coffee GetcoffeeById(Guid id)
    {
        List<Coffee> coffee = RetrieveCoffeeData(); // Retrieves the list of hobbies and stores it in hobbies object

        // Return the first hobby with the specified Id.
        return coffee.FirstOrDefault(x => x.Id == id); //creating arrow function and checking whether the Id of Hobbies is equal to the id of parameter that recieves value later on.
    }


    // Retrieves a specific hobby by its Id.
    public static Coffee GetcoffeeByName(String name)
    {
        List<Coffee> coffee = RetrieveCoffeeData(); // Retrieves the list of hobbies and stores it in hobbies object

        // Return the first hobby with the specified Id.
        return coffee.FirstOrDefault(x => x.Name == name); //creating arrow function and checking whether the Id of Hobbies is equal to the id of parameter that recieves value later on.
    }

    // Edits the name of a specific hobby.
    public static List<Coffee> EditCoffee(Guid id, string newPrice)
    {
        // Retrieve the list of hobbies.
        List<Coffee> coffee = RetrieveCoffeeData();
        // Find the hobby with the specified Id.
        Coffee editCoffee = coffee.FirstOrDefault(x => x.Id == id);
        // If the hobby is not found, throw an exception.
        if (editCoffee == null)
        {
            throw new Exception("Coffee not found");
        }
        // Update the name of the hobby.
        editCoffee.Price = newPrice;
        SaveCoffeeToJson(coffee); // Save the updated list of hobbies to the JSON file by calling method SaveHobbiesToJson
        return coffee;  // Return the updated list of hobbies.
    }
}
