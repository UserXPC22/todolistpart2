using todolistpart2.Services;

namespace todolistpart2.Views.Tasks;

public partial class TaskDetailsPage : ContentPage
{
    private ToDoClass _selectedTask;
    private readonly IAuthService _authService;

    public TaskDetailsPage(ToDoClass task)
    {
        InitializeComponent();
        _selectedTask = task;
        _authService = new AuthService();

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
        string currentStatus = _selectedTask.status;
        string targetStatus = currentStatus == "Completed" ? "Pending" : "Completed";
        string targetApiStatus = currentStatus == "Completed" ? "active" : "inactive";

        var (success, message) = await _authService.UpdateItemStatus(_selectedTask.item_id, targetApiStatus);

        if (success)
        {
            _selectedTask.status = targetStatus;

            if (targetStatus == "Completed")
            {
                await DisplayAlert("Success", message ?? "Task marked as completed!", "OK");
            }
            else
            {
                await DisplayAlert("Restored", message ?? "Task moved back to To-Do list.", "OK");
            }

            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", message ?? "Failed to update status.", "OK");
        }
    }

    private async void OnUpdateClicked(object sender, EventArgs e)
    {
        var newName = EditNameEntry.Text;
        var newDesc = EditDescriptionEntry.Text;

        if (string.IsNullOrWhiteSpace(newName))
        {
            await DisplayAlert("Error", "Please enter a task name.", "OK");
            return;
        }

        var (success, message) = await _authService.UpdateItem(_selectedTask.item_id, newName, newDesc);

        if (success)
        {
            _selectedTask.item_name = newName;
            _selectedTask.item_description = newDesc;
            await DisplayAlert("Success", message ?? "Task has been updated!", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", message ?? "Failed to update task.", "OK");
        }
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
