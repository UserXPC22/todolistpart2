namespace todolistpart2;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // This line enables the ability to move between pages
        MainPage = new NavigationPage(new Views.Auth.SignInPage());
    }
}