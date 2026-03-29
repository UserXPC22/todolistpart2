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
}