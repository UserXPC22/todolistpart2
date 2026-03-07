using System;
using Microsoft.Maui.Controls;

namespace todolistpart2;

public partial class SignInPage : ContentPage
{
    public SignInPage()
    {
        InitializeComponent();
    }

    // This handles the "Sign in" button click
    private async void OnSignInClicked(object sender, EventArgs e)
    {
        // Simple validation check
        if (string.IsNullOrEmpty(EmailEntry.Text) || string.IsNullOrEmpty(PasswordEntry.Text))
        {
            await DisplayAlert("Error", "Please enter both email and password.", "OK");
            return;
        }

        // Switching to AppShell loads the TabBar we just created
        // This is better than NavigationPage because it shows the bottom menu
        Application.Current.MainPage = new AppShell();
    }

    // This handles the "Sign up" button click
    private async void OnSignUpClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignUpPage());
    }
}