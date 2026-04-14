using System.Text;
using System.Text.Json;

namespace todolistpart2.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient = new HttpClient();
    private const string BaseUrl = "https://todo-list.dcism.org";

    public async Task<(bool success, string message)> SignUp(string firstName, string lastName, string email, string password, string confirmPassword)
    {
        try
        {
            var data = new
            {
                first_name = firstName,
                last_name = lastName,
                email = email,
                password = password,
                confirm_password = confirmPassword
            };

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BaseUrl}/signup_action.php", content);
            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<JsonElement>(responseString);
            var status = result.GetProperty("status").GetInt32();
            var message = result.GetProperty("message").GetString();

            return (status == 200, message);
        }
        catch (Exception)
        {
            return (false, "Something went wrong. Please try again.");
        }
    }

    public async Task<(bool success, string message, int userId, string fname)> SignIn(string email, string password)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/signin_action.php?email={email}&password={password}");
            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<JsonElement>(responseString);
            var status = result.GetProperty("status").GetInt32();
            var message = result.GetProperty("message").GetString();

            if (status == 200)
            {
                var data = result.GetProperty("data");
                var userId = data.GetProperty("id").GetInt32();
                var fname = data.GetProperty("fname").GetString();
                return (true, message, userId, fname);
            }

            return (false, message, 0, "");
        }
        catch (Exception)
        {
            return (false, "Something went wrong. Please try again.", 0, "");
        }
    }

    //AddToDoItem

    public async Task<(bool success, string? message, ToDoClass? item)> AddItem(string itemName, string itemDescription, int userId)
    {
        try
        {
            var data = new
            {
                item_name = itemName,
                item_description = itemDescription,
                user_id = userId
            };

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BaseUrl}/addItem_action.php", content);
            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<JsonElement>(responseString);
            var status = result.GetProperty("status").GetInt32();
            var message = result.GetProperty("message").GetString();

            if (status == 200)
            {
                var responseData = result.GetProperty("data");

                var newTask = new ToDoClass 
                { 
                    item_id = responseData.GetProperty("item_id").GetInt32(),
                    item_name = responseData.GetProperty("item_name").GetString(), 
                    item_description = responseData.GetProperty("item_description").GetString(),
                    status = responseData.GetProperty("status").GetString(),
                    user_id = responseData.GetProperty("user_id").GetInt32()
                };
                return (true, message, newTask);
            }
            return (false, message, null);
        }
        catch (Exception ex)
        {
            return (false, "Something went wrong. Please try again.", null);
        }
    }

    //UpdateToDoItem

    public async Task<(bool success, string? message)> UpdateItem(int itemId, string itemName, string itemDescription)
    {
        try
        {
            var data = new
            {
                item_id = itemId,
                item_name = itemName,
                item_description = itemDescription
            };

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{BaseUrl}/editItem_action.php", content);
            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<JsonElement>(responseString);
            var status = result.GetProperty("status").GetInt32();
            var message = result.GetProperty("message").GetString();

            return (status == 200, message);
        }
        catch (Exception)
        {
            return (false, "Something went wrong. Please try again.");
        }
    }

    //ItemStatusUpdate
    public async Task<(bool success, string? message)> UpdateItemStatus(int itemId, string status)
    {
        try
        {
            var data = new
            {
                item_id = itemId,
                status = status
            };

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{BaseUrl}/statusItem_action.php", content);
            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<JsonElement>(responseString);
            var responseStatus = result.GetProperty("status").GetInt32();
            var message = result.GetProperty("message").GetString();

            return (responseStatus == 200, message);
        }
        catch (Exception)
        {
            return (false, "Something went wrong. Please try again.");
        }
    }
}
