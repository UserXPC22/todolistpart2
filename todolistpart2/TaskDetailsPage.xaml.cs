namespace todolistpart2;

public partial class TaskDetailsPage : ContentPage
{
    private ToDoClass _selectedTask;

    public TaskDetailsPage(ToDoClass task)
    {
        InitializeComponent();
        _selectedTask = task;

        EditNameEntry.Text = _selectedTask.item_name;
        EditDescriptionEntry.Text = _selectedTask.item_description;

        // Logic to swap button text based on status
        if (_selectedTask.status == "Completed")
        {
            StatusActionButton.Text = "Mark as Incomplete";
            StatusActionButton.BackgroundColor = Colors.Orange; // Visual cue it's changing back
        }
        else
        {
            StatusActionButton.Text = "Complete Task";
            StatusActionButton.BackgroundColor = Colors.Green;
        }
    }

    private async void OnStatusActionClicked(object sender, EventArgs e)
    {
        if (_selectedTask.status == "Completed")
        {
            // Move back to main list
            _selectedTask.status = "Pending";
            await DisplayAlert("Restored", "Task moved back to To-Do list.", "OK");
        }
        else
        {
            // Move to completed list
            _selectedTask.status = "Completed";
            await DisplayAlert("Success", "Task marked as completed!", "OK");
        }
        
        await Navigation.PopAsync();
    }

    private async void OnUpdateClicked(object sender, EventArgs e)
    {
        _selectedTask.item_name = EditNameEntry.Text;
        _selectedTask.item_description = EditDescriptionEntry.Text;
        await DisplayAlert("Updated", "Task has been updated!", "OK");
        await Navigation.PopAsync();
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Delete", "Are you sure?", "Yes", "No");
        if (answer)
        {
            ToDoPage.MyTasks.Remove(_selectedTask);
            await Navigation.PopAsync();
        }
    }
}