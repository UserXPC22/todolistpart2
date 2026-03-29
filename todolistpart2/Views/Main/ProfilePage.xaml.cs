using System;

namespace todolistpart2.Views.Main;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();

        // Read from Preferences saved during Sign In
        var name = Preferences.Get("user_name", "Unknown");
        var email = Preferences.Get("user_email", "Unknown");

        NameLabel.Text = $"Name: {name}";
        EmailLabel.Text = $"Email: {email}";
    }

    private void OnSignOutClicked(object sender, EventArgs e)
    {
        Preferences.Clear();
        Application.Current.MainPage = new NavigationPage(new Views.Auth.SignInPage());
    }
}