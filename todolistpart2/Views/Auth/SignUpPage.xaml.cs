using System;
using Microsoft.Maui.Controls;
using todolistpart2.Services;

namespace todolistpart2.Views.Auth;

public partial class SignUpPage : ContentPage
{
    private readonly IAuthService _authService;

    public SignUpPage()
    {
        InitializeComponent();
        _authService = new AuthService();
    }

    private async void OnSignUpClicked(object sender, EventArgs e)
    {
        var firstName = FirstNameEntry.Text;
        var lastName = LastNameEntry.Text;
        var email = EmailEntry.Text;
        var password = PasswordEntry.Text;
        var confirmPassword = ConfirmPasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(firstName) ||
            string.IsNullOrWhiteSpace(lastName) ||
            string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password) ||
            string.IsNullOrWhiteSpace(confirmPassword))
        {
            await DisplayAlert("Error", "Please fill in all fields.", "OK");
            return;
        }

        if (password != confirmPassword)
        {
            await DisplayAlert("Error", "Passwords do not match.", "OK");
            return;
        }

        var (success, message) = await _authService.SignUp(firstName, lastName, email, password, confirmPassword);

        if (success)
        {
            await DisplayAlert("Success", message, "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", message, "OK");
        }
    }

    private async void OnSignInClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}