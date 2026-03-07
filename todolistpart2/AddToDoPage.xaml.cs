using System;

namespace todolistpart2;

public partial class AddToDoPage : ContentPage
{
    public AddToDoPage()
    {
        InitializeComponent();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TaskNameEntry.Text))
        {
            await DisplayAlert("Error", "Please enter a task name.", "OK");
            return;
        }

        var newTask = new ToDoClass 
        { 
            item_name = TaskNameEntry.Text, 
            item_description = DescriptionEntry.Text,
            status = "Pending"
        };

        // We add directly to the list in ToDoPage! No messenger needed.
        ToDoPage.MyTasks.Add(newTask);

        await DisplayAlert("Success", "Task Saved!", "OK");
        await Navigation.PopAsync(); 
    }
}