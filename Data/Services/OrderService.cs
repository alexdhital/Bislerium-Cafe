
using Newtonsoft.Json;  //Giving the reference of the package so that we can use it's method
using Bislerium_cafe.Data.Models;  //Giving the path of the files that are in Model folder i.e. AddForm.cs and Hobby.cs, Allowing us to use it
using Bislerium_cafe.Data.Utils;  //Giving the path of the files that is in Utils folder i.e. FormUtils.cs, allowing us to use it's methods

namespace Bislerium_cafe.Data.Services;

// Service class responsible for handling operations like Saving, Retrieving overall Manipulating related to form data.
public class OrderServices
{
    // Saves user Input in Form to a JSON file.
    public static void SaveFormDataInJson(OrderModel form)
    {
        // Gets the file path where form data will be stored from ApplicationFilePath method
        // in FormUtils class in Utils Folder and stores it in the variable filePath.
        string filePath = FormUtils.ApplicationFilePath();
        try // Deserialize existing JSON data from the file into a list of AddForm objects called formList.
        {
            List<OrderModel> formList; // object of List of AddForm i.e. formList
            string existingJsonData = File.ReadAllText(filePath); //ReadAllText reads the datas inside the file from filePath Variable and Stores in variable called existingJsonData

            // If the existingJSONData variable is empty, initialize a new list; otherwise, deserialize the data.
            if (string.IsNullOrEmpty(existingJsonData))
            {
                formList = new List<OrderModel>();
            }
            else
            {
                formList = JsonConvert.DeserializeObject<List<OrderModel>>(existingJsonData);
            }
            foreach(var item in formList)
            {
                if(form.Id == item.Id)
                {
                    formList.Remove(item);
                }
            }
            // Add the current form to the list.
            formList.Add(form);

            // Serialize the updated list of form data to JSON format with formatting Indented and store it in a variable formJsonData
            string formJsonData = JsonConvert.SerializeObject(formList, Formatting.Indented);

            // Write the JSON data back to the file.
            File.WriteAllText(filePath, formJsonData);
        }
        catch (Exception ex)
        {
            // Handle exceptions by displaying an alert with the error message.
            Console.WriteLine($"Error reading JSON file: {ex.Message}");
            App.Current.MainPage.DisplayAlert("Error", $"Error Saving Data", "OK");
        }
    }


    public static void SaveAfterEditJson(OrderModel form)
    {
        string filePath = FormUtils.ApplicationFilePath();
        try // Deserialize existing JSON data from the file into a list of AddForm objects called formList.
        {
            List<OrderModel> formList; // object of List of AddForm i.e. formList
            string existingJsonData = File.ReadAllText(filePath); //ReadAllText reads the datas inside the file from filePath Variable and Stores in variable called existingJsonData

            // If the existingJSONData variable is empty, initialize a new list; otherwise, deserialize the data.
            if (string.IsNullOrEmpty(existingJsonData))
            {
                formList = new List<OrderModel>();
            }
            else
            {
                formList = JsonConvert.DeserializeObject<List<OrderModel>>(existingJsonData);
            }
            // Add the current form to the list.
            formList.Add(form);

            // Serialize the updated list of form data to JSON format with formatting Indented and store it in a variable formJsonData
            string formJsonData = JsonConvert.SerializeObject(formList, Formatting.Indented);

            // Write the JSON data back to the file.
            File.WriteAllText(filePath, formJsonData);
        }
        catch (Exception ex)
        {
            // Handle exceptions by displaying an alert with the error message.
            Console.WriteLine($"Error reading JSON file: {ex.Message}");
            App.Current.MainPage.DisplayAlert("Error", $"Error Saving Data", "OK");
        }
    }
    // Retrieves form data from the JSON file.
    public static List<OrderModel> RetrieveOrderData()
    {
        // Gets the file path where form data is stored from ApplicationFilePath method
        // in FormUtils class in Utils Folder and stores it in the variable filePath.
        string filePath = FormUtils.ApplicationFilePath();
        try
        {
            string existingJsonData = File.ReadAllText(filePath); //ReadAllText reads the datas inside the file from filePath Variable and Stores in variable called existingJsonData

            // If the existing JSON data is empty, return an empty list;
            // otherwise, deserialize the data into a list of AddForm objects.
            if (string.IsNullOrEmpty(existingJsonData))
            {
                return new List<OrderModel>();
            }
            return JsonConvert.DeserializeObject<List<OrderModel>>(existingJsonData);
        }
        catch (Exception ex)
        {
            // Handle exceptions by displaying an alert with the error message.
            Console.WriteLine($"Error reading JSON file: {ex.Message}");
            App.Current.MainPage.DisplayAlert("Error", "Error Retrieving Data from Json", "OK");
            return new List<OrderModel>();
        }
    }

    //eves a specific hobby by its Id.
    public static OrderModel GetOrderDataById(Guid id)
    {
        List<OrderModel> orderdata = RetrieveOrderData(); // Retrieves the list of hobbies and stores it in hobbies object

        // Return the first hobby with the specified Id.
        return orderdata.FirstOrDefault(x => x.Id == id); //creating arrow function and checking whether the Id of Hobbies is equal to the id of parameter that recieves value later on.
    }

    public static List<OrderModel> EditOrder(Guid id, string username, string phoneNumber, int finalPrice,int discount)
    {
        List<OrderModel> order = RetrieveOrderData();
        List<OrderModel> updatedOrders = new List<OrderModel>();

        foreach (var existingOrder in order)
        {
            if (existingOrder.Id == id)
            {
                // Update the fields of the existing order.
                existingOrder.Username = username;
                existingOrder.IsPaid = true;
                existingOrder.Phonenumber = phoneNumber;
                existingOrder.Discount = discount.ToString();
                existingOrder.FinalPrice = finalPrice.ToString();
                existingOrder.PaidDate= DateTime.Now;
            }

            updatedOrders.Add(existingOrder);
        }

        SaveAllPaymentToJson(updatedOrders);

        return updatedOrders;
    }

     public static void SaveAllPaymentToJson(List<OrderModel> orderModel)
    {
        string filePath = FormUtils.ApplicationFilePath();
        try // Deserialize existing JSON data from the file into a list of AddForm objects called formList.
        {
           // List<OrderModel> formList; // object of List of AddForm i.e. formList
       
            string formJsonData = JsonConvert.SerializeObject(orderModel, Formatting.Indented);

            // Write the JSON data back to the file.
            File.WriteAllText(filePath, formJsonData);
            App.Current.MainPage.DisplayAlert("Success", $"Successfully added", "OK");
            return;

        }
        catch (Exception ex)
        {
            // Handle exceptions by displaying an alert with the error message.
            Console.WriteLine($"Error reading JSON file: {ex.Message}");
            App.Current.MainPage.DisplayAlert("Error", $"Error Saving Data", "OK");
        }
    }

    public static List<OrderModel> MakeDiscount(Guid id, string discount)
    {
        // Retrieve the list of hobbies.
        List<OrderModel> order = RetrieveOrderData();
        // Find the hobby with the specified Id.
        OrderModel editOrder = order.FirstOrDefault(x => x.Id == id);
        // If the hobby is not found, throw an exception.
        if (editOrder == null)
        {
            throw new Exception("Addins not found");
        }
        // Update the name of the hobby.
        editOrder.Discount = discount;
        SaveFormDataInJson(editOrder); // Save the updated list of hobbies to the JSON file by calling method SaveHobbiesToJson
        return order;  // Return the updated list of hobbies.
    }

    public static List<OrderModel> EditQuantity(Guid id, string quantity)
    {
        List<OrderModel> order = RetrieveOrderData();
        List<OrderModel> updatedOrders = new List<OrderModel>();

        foreach (var editOrder in order)
        {
            if (editOrder.Id == id)
            {
                // Update the fields of the existing order.
                editOrder.Quantity = quantity;
                editOrder.FinalPrice = (int.Parse(quantity) * int.Parse(editOrder.Coffee.Price)).ToString();
            }

            updatedOrders.Add(editOrder);
        }

        SaveAllPaymentToJson(updatedOrders);

        return updatedOrders;
    }

    public static bool DeleteOrderModel(Guid? id)
    {
        List<OrderModel> order = RetrieveOrderData();
        List<OrderModel> updatedOrders = new List<OrderModel>();

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

    public static bool SaveAfterDelete(List<OrderModel> orderModel)
    {
        string filePath = FormUtils.ApplicationFilePath();
        try // Deserialize existing JSON data from the file into a list of AddForm objects called formList.
        {
            // List<OrderModel> formList; // object of List of AddForm i.e. formList

            string formJsonData = JsonConvert.SerializeObject(orderModel, Formatting.Indented);

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

