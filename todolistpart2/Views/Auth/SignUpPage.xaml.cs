using System;
using Microsoft.Maui.Controls;

namespace todolistpart2.Views.Auth;

public partial class SignUpPage : ContentPage
{
    public SignUpPage()
    {
        InitializeComponent();
    }

    // You likely have a button for "Sign Up" in your XAML. 
    // Make sure its Clicked event is set to "OnSignUpClicked"
    private async void OnSignUpClicked(object sender, EventArgs e)
    {
        // Add your validation logic here (check if entries are empty)
        
        await DisplayAlert("Success", "Account created successfully!", "OK");

        // This is the fix: Redirect directly to the Tabbed interface
        Application.Current.MainPage = new AppShell();
    }

    private async void OnSignInClicked(object sender, EventArgs e)
    {
        // Returns to the Sign In page
        await Navigation.PopAsync();
    }
}