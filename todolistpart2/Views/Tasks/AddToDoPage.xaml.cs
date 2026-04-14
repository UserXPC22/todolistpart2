using System;
using Microsoft.Maui.Controls;
using todolistpart2.Services;

namespace todolistpart2.Views.Tasks;

public partial class AddToDoPage : ContentPage
{
    private readonly IAuthService _authService;

    public AddToDoPage()
    {
        InitializeComponent();
        _authService = new AuthService();
    }


    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TaskNameEntry.Text))
        {
            await DisplayAlert("Error", "Please enter a task name.", "OK");
            return;
        }

        try
        {
            var userId = Preferences.Get("user_id", 0);

            var (success, message, newTask) = await _authService.AddItem(TaskNameEntry.Text, DescriptionEntry.Text, userId);

            if (success && newTask != null)
            {
                // We add directly to the list in ToDoPage! No messenger needed.
                ToDoPage.MyTasks.Add(newTask);

                await DisplayAlert("Success", message ?? "Task Saved!", "OK");
                await Navigation.PopAsync(); 
            }
            else
            {
                await DisplayAlert("Error", message ?? "Failed to add task.", "OK");
            }
        }
        catch (Exception)
        {
            await DisplayAlert("Error", "Something went wrong. Please try again.", "OK");
        }
    }
}
