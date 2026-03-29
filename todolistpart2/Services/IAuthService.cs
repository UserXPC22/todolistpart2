namespace todolistpart2.Services;

public interface IAuthService
{
    Task<(bool success, string? message)> SignUp(string firstName, string lastName, string email, string password, string confirmPassword);
    Task<(bool success, string? message, int userId, string? fname)> SignIn(string email, string password);
}