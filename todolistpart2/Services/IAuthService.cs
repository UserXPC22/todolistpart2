namespace todolistpart2.Services;

public interface IAuthService
{
    Task<(bool success, string? message)> SignUp(string firstName, string lastName, string email, string password, string confirmPassword);
    Task<(bool success, string? message, int userId, string? fname)> SignIn(string email, string password);
    Task<(bool success, string? message, ToDoClass? item)> AddItem(string itemName, string itemDescription, int userId);
    Task<(bool success, string? message)> UpdateItem(int itemId, string itemName, string itemDescription);
    Task<(bool success, string? message)> UpdateItemStatus(int itemId, string status);
}
