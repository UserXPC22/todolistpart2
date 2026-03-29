using System;
using Microsoft.Maui.Controls;
using todolistpart2.Services;

namespace todolistpart2.Views.Auth;

public partial class SignInPage : ContentPage
{
    private readonly IAuthService _authService;

    public SignInPage()
    {
        InitializeComponent();
        _authService = new AuthService();
    }

    private async void OnSignInClicked(object sender, EventArgs e)
    {
        var email = EmailEntry.Text;
        var password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Please enter both email and password.", "OK");
            return;
        }

        var (success, message, userId, fname) = await _authService.SignIn(email, password);

        if (success)
        {
            Preferences.Set("user_id", userId);
            Preferences.Set("user_name", fname);
            Preferences.Set("user_email", email);
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            await DisplayAlert("Error", message, "OK");
        }
    }

    private async void OnSignUpClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignUpPage());
    }
}