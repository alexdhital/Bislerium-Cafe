using Bislerium_cafe.Data.Models;
using Bislerium_cafe.Data.Utils;
using Newtonsoft.Json;

namespace Bislerium_cafe.Data.Services
{
    public class CustomerServices
    {

        public static void EditCustomer(string phone, int count)
        {
            List<Customer> customerdata = RetrieveCustomerData(); // Retrieves the list of hobbies and stores it in hobbies object
            List<Customer> updatedOrders = new List<Customer>();

            foreach (var existingOrder in customerdata)
            {
                if (existingOrder.Phonenumber == phone)
                {
                    // Update the fields of the existing order.
                    existingOrder.Count += 1;

                }

                updatedOrders.Add(existingOrder);
            }


            SaveCustomerToJson(updatedOrders);

        }
        public static List<Customer> RetrieveCustomerData()
        {
            // Gets the file path where customer data is stored from ApplicationFilePath method
            // in customerUtils class in Utils Folder and stores it in the variable filePath.
            string filePath = FormUtils.CustomerFilePath();
            try
            {
                string existingJsonData = File.ReadAllText(filePath); //ReadAllText reads the datas inside the file from filePath Variable and Stores in variable called existingJsonData

                // If the existing JSON data is empty, return an empty list;
                // otherwise, deserialize the data into a list of Addcustomer objects.
                if (string.IsNullOrEmpty(existingJsonData))
                {
                    return new List<Customer>();
                }
                return JsonConvert.DeserializeObject<List<Customer>>(existingJsonData);
            }
            catch (Exception ex)
            {
                // Handle exceptions by displaying an alert with the error message.
                Console.WriteLine($"Error reading JSON file: {ex.Message}");
                App.Current.MainPage.DisplayAlert("Error", "Error Retrieving Data from Json", "OK");
                return new List<Customer>();
            }
        }


        public static bool SaveCustomerDataInJson(Customer customer)
        {
            // Gets the file path where customer data will be stored from ApplicationFilePath method
            // in customerUtils class in Utils Folder and stores it in the variable filePath.
            string filePath = FormUtils.CustomerFilePath();
            try // Deserialize existing JSON data from the file into a list of Addcustomer objects called customerList.
            {
                List<Customer> customerList; // object of List of Addcustomer i.e. customerList
                string existingJsonData = File.ReadAllText(filePath); //ReadAllText reads the datas inside the file from filePath Variable and Stores in variable called existingJsonData

                // If the existingJSONData variable is empty, initialize a new list; otherwise, deserialize the data.
                if (string.IsNullOrEmpty(existingJsonData))
                {
                    customerList = new List<Customer>();
                }
                else
                {
                    customerList = JsonConvert.DeserializeObject<List<Customer>>(existingJsonData);
                }

                // Add the current customer to the list.
                customerList.Add(customer);

                // Serialize the updated list of customer data to JSON customerat with customeratting Indented and store it in a variable customerJsonData
                string customerJsonData = JsonConvert.SerializeObject(customerList, Formatting.Indented);

                // Write the JSON data back to the file.
                File.WriteAllText(filePath, customerJsonData);

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



        public static void EditCustomerCountZero(string phone, int count)
        {
            List<Customer> customerdata = RetrieveCustomerData(); // Retrieves the list of hobbies and stores it in hobbies object
            List<Customer> updatedOrders = new List<Customer>();

            foreach (var existingOrder in customerdata)
            {
                if (existingOrder.Phonenumber == phone)
                {
                    // Update the fields of the existing order.
                    existingOrder.Count = 0;

                }

                updatedOrders.Add(existingOrder);
            }


            SaveCustomerToJson(updatedOrders);

        }

        public static void SaveCustomerToJson(List<Customer> CustomerModel)
        {
            string filePath = FormUtils.CustomerFilePath();
            try // Deserialize existing JSON data from the file into a list of AddForm objects called formList.
            {
                // List<CustomerModel> formList; // object of List of AddForm i.e. formList

                string formJsonData = JsonConvert.SerializeObject(CustomerModel, Formatting.Indented);

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
    }
}