
using Newtonsoft.Json;  //Giving the reference of the package so that we can use it's method
using Bislerium_cafe.Data.Models;  //Giving the path of the files that are in Model folder i.e. AddForm.cs and Hobby.cs, Allowing us to use it
using Bislerium_cafe.Data.Utils;  //Giving the path of the files that is in Utils folder i.e. FormUtils.cs, allowing us to use it's methods
namespace Bislerium_cafe.Data.Services;

// Service class responsible for handling operations like Saving, Retrieving overall Manipulating related to form data.
public class MemberServices
{
    // Saves user Input in Form to a JSON file.
    public static bool SaveFormDataInJson(MemberModel form)
{
    // Gets the file path where form data will be stored from ApplicationFilePath method
    // in FormUtils class in Utils Folder and stores it in the variable filePath.
    string filePath = FormUtils.MemberFilePath();
    try // Deserialize existing JSON data from the file into a list of AddForm objects called formList.
    {
        List<MemberModel> formList; // object of List of AddForm i.e. formList
        string existingJsonData = File.ReadAllText(filePath); //ReadAllText reads the datas inside the file from filePath Variable and Stores in variable called existingJsonData

        // If the existingJSONData variable is empty, initialize a new list; otherwise, deserialize the data.
        if (string.IsNullOrEmpty(existingJsonData))
        {
            formList = new List<MemberModel>();
        }
        else
        {
            formList = JsonConvert.DeserializeObject<List<MemberModel>>(existingJsonData);
        }
        if(formList != null)
            {
                foreach (MemberModel model in formList)
                {
                    if(model.Phonenumber == form.Phonenumber)
                    {
                        App.Current.MainPage.DisplayAlert("Failed to Registrations", $"Please enter a new phonenumber it is already Registered", "Close");
                        return false;

                    }
                }
            }
        // Add the current form to the list.
        formList.Add(form);

        // Serialize the updated list of form data to JSON format with formatting Indented and store it in a variable formJsonData
        string formJsonData = JsonConvert.SerializeObject(formList, Formatting.Indented);

        // Write the JSON data back to the file.
        File.WriteAllText(filePath, formJsonData);
            App.Current.MainPage.DisplayAlert("Successfully", $"Successfully Registered a Phonenumber", "OK");

            return true;
    }
    catch (Exception ex)
    {
        // Handle exceptions by displaying an alert with the error message.
        Console.WriteLine($"Error reading JSON file: {ex.Message}");
        App.Current.MainPage.DisplayAlert("Error", $"Error Saving Data", "OK");
            return false;
    }
}


// Retrieves form data from the JSON file.
public static List<MemberModel> RetrieveMemberData()
{
    // Gets the file path where form data is stored from ApplicationFilePath method
    // in FormUtils class in Utils Folder and stores it in the variable filePath.
    string filePath = FormUtils.MemberFilePath();
    try
    {
        string existingJsonData = File.ReadAllText(filePath); //ReadAllText reads the datas inside the file from filePath Variable and Stores in variable called existingJsonData

        // If the existing JSON data is empty, return an empty list;
        // otherwise, deserialize the data into a list of AddForm objects.
        if (string.IsNullOrEmpty(existingJsonData))
        {
            return new List<MemberModel>();
        }
        return JsonConvert.DeserializeObject<List<MemberModel>>(existingJsonData);
    }
    catch (Exception ex)
    {
        // Handle exceptions by displaying an alert with the error message.
        Console.WriteLine($"Error reading JSON file: {ex.Message}");
        App.Current.MainPage.DisplayAlert("Error", "Error Retrieving Data from Json", "OK");
        return new List<MemberModel>();
    }
}

//eves a specific hobby by its Id.
public static MemberModel GetOrderDataById(Guid id)
{
    List<MemberModel> orderdata = RetrieveMemberData(); // Retrieves the list of hobbies and stores it in hobbies object

    // Return the first hobby with the specified Id.
    return orderdata.FirstOrDefault(x => x.Id == id); //creating arrow function and checking whether the Id of Hobbies is equal to the id of parameter that recieves value later on.
}

    
public static void SaveAllPaymentToJson(List<MemberModel> MemberModel)
{
    string filePath = FormUtils.ApplicationFilePath();
    try // Deserialize existing JSON data from the file into a list of AddForm objects called formList.
    {
        // List<MemberModel> formList; // object of List of AddForm i.e. formList

        string formJsonData = JsonConvert.SerializeObject(MemberModel, Formatting.Indented);

        // Write the JSON data back to the file.
        File.WriteAllText(filePath, formJsonData);
        App.Current.MainPage.DisplayAlert("Success", $"Successfully added", "OK");

    }
    catch (Exception ex)
    {
        // Handle exceptions by displaying an alert with the error message.
        Console.WriteLine($"Error reading JSON file: {ex.Message}");
        App.Current.MainPage.DisplayAlert("Error", $"Error Saving Data", "OK");
    }
}



public static bool DeleteMemberModel(Guid? id)
{
    List<MemberModel> order = RetrieveMemberData();
    List<MemberModel> updatedOrders = new List<MemberModel>();

    foreach (var editOrder in order)
    {
        if (editOrder.Id == id)
        {
        }
        else
        {
            updatedOrders.Add(editOrder);
        }

    }

    SaveAfterDelete(updatedOrders);

    return true;
}

public static bool SaveAfterDelete(List<MemberModel> MemberModel)
{
    string filePath = FormUtils.ApplicationFilePath();
    try // Deserialize existing JSON data from the file into a list of AddForm objects called formList.
    {
        // List<MemberModel> formList; // object of List of AddForm i.e. formList

        string formJsonData = JsonConvert.SerializeObject(MemberModel, Formatting.Indented);

        // Write the JSON data back to the file.
        File.WriteAllText(filePath, formJsonData);
        App.Current.MainPage.DisplayAlert("Success", $"Successfully Removed the order", "OK");
        return true;

    }
    catch (Exception ex)
    {
        // Handle exceptions by displaying an alert with the error message.
        Console.WriteLine($"Error reading JSON file: {ex.Message}");
        App.Current.MainPage.DisplayAlert("Error", $"Error Saving Data", "OK");
        return false;
    }
}
}

