
using Bislerium_cafe.Data.Models;
using Bislerium_cafe.Data.Utils;
using Newtonsoft.Json;

namespace Bislerium_cafe.Data.Services;

public class AddinService
{
    // Saves lists of hobby data Injected to a JSON file.
    public static void SaveAddinsToJson(List<Addins> addins)
    {
        // Gets the file path where form data will be stored from ApplicationFilePath method
        // in FormUtils class in Utils Folder and stores it in the variable filePath.
        string filePath = FormUtils.AddinsFilePath();

        // Serialize the list of hobbies to JSON format with formatting Indented and store it in Variable jsonData
        string jsonData = JsonConvert.SerializeObject(addins, Formatting.Indented);

        // Write the JSON data to the file given from filePath variable and data from jsonData variable.
        File.WriteAllText(filePath, jsonData);
    }

    //This method Injects the data Into the Json File Manually by creating the multiple objects and Passing it to the list only if the data inside the file is empty.
    public static void InjectSampleAddinsData()
    {
        // Gets the file path where hobby data will be stored from HobbiesFilePath method
        // in FormUtils class in Utils Folder and stores it in the variable filePath.
        string filePath = FormUtils.AddinsFilePath();

        // Read existing data from the file and store it in variable existingData
        var existingData = File.ReadAllText(filePath);

        // If the file is empty, injects a list of sample hobby data in a object of List<Hobby> called sampleHobbies first and saves it in a Json File by calling method SaveHobbiesToJson.
        if (string.IsNullOrEmpty(existingData))
        {
            List<Addins> sampleAddins = new List<Addins>
            {
                new Addins { Name = "Honey", Price="50" },
                new Addins { Name = "Chocolate Syrup", Price="80" },
                new Addins { Name = "Creme" , Price = "90"},
                new Addins { Name = "Caramel Syrup" , Price = "70"},
            };
            SaveAddinsToJson(sampleAddins); // Save the sample hobby data to the JSON file by calling SaveHobbiesToJson Method and passing sampleHobbies as it Argument.
        }
    }

    // Retrieves hobby data from the JSON file.
    public static List<Addins> RetrieveAddinsData()
    {
        // Gets the file path where hobby data is stored from HobbiesFilePath method
        // in FormUtils class in Utils Folder and stores it in the variable filePath.
        string filePath = FormUtils.AddinsFilePath();
        try
        {
            string existingJsonData = File.ReadAllText(filePath); // Read existing JSON data from the file.

            // If the existing JSON data is empty, return an empty list;
            // otherwise, deserialize the data into a list of Hobby objects.
            if (string.IsNullOrEmpty(existingJsonData))
            {
                return new List<Addins>();
            }
            return JsonConvert.DeserializeObject<List<Addins>>(existingJsonData);
        }
        catch (Exception ex)
        {
            // Handle exceptions by displaying an alert with the error message.
            Console.WriteLine($"Error reading JSON file: {ex.Message}");
            return new List<Addins>();
        }
    }

    // Retrieves a specific hobby by its Id.
    public static Addins GetAddinsById(Guid id)
    {
        List<Addins> addins = RetrieveAddinsData(); // Retrieves the list of hobbies and stores it in hobbies object

        // Return the first hobby with the specified Id.
        return addins.FirstOrDefault(x => x.Id == id); //creating arrow function and checking whether the Id of Hobbies is equal to the id of parameter that recieves value later on.
    }

    public static Addins GetAddinsByName(string name)
    {
        List<Addins> addins = RetrieveAddinsData(); // Retrieves the list of hobbies and stores it in hobbies object

        // Return the first hobby with the specified Id.
        return addins.FirstOrDefault(x => x.Name == name); //creating arrow function and checking whether the Id of Hobbies is equal to the id of parameter that recieves value later on.
    } //creating arrow function and checking whether the Id of Hobbies is equal to the id of parameter that recieves value later on.
    

    // Edits the name of a specific hobby.
    public static List<Addins> EditAddins(Guid id, string newPrice)
    {
        // Retrieve the list of hobbies.
        List<Addins> addins = RetrieveAddinsData();
        // Find the hobby with the specified Id.
        Addins editAddins = addins.FirstOrDefault(x => x.Id == id);
        // If the hobby is not found, throw an exception.
        if (editAddins == null)
        {
            throw new Exception("Addins not found");
        }
        // Update the name of the hobby.
        editAddins.Price = newPrice;
        SaveAddinsToJson(addins); // Save the updated list of hobbies to the JSON file by calling method SaveHobbiesToJson
        return addins;  // Return the updated list of hobbies.
    }
}
