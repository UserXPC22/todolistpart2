using System;

namespace todolistpart2;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();
    }

    private void OnSignOutClicked(object sender, EventArgs e)
    {
        // This resets the app's root to the Sign In page.
        // We use NavigationPage so the user can navigate to "Sign Up" again if they want.
        Application.Current.MainPage = new NavigationPage(new SignInPage());
    }
}