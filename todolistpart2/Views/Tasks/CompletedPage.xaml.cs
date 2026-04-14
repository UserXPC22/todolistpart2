using System.Linq;
using todolistpart2.Services;

namespace todolistpart2.Views.Tasks;

public partial class CompletedPage : ContentPage
{
    private readonly IAuthService _authService;

    public CompletedPage()
    {
        InitializeComponent();
        _authService = new AuthService();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        RefreshList();
    }

    private void RefreshList()
    {
        // Explicitly reference ToDoPage.MyTasks
        CompletedListView.ItemsSource = ToDoPage.MyTasks.Where(t => t.status == "Completed").ToList();
    }

    private async void OnRestoreButtonClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var task = button?.CommandParameter as ToDoClass;
        if (task != null)
        {
            var (success, message) = await _authService.UpdateItemStatus(task.item_id, "active");
            if (success)
            {
                task.status = "Pending";
                await DisplayAlert("Restored", message ?? "Task moved back to To-Do list.", "OK");
                RefreshList();
            }
            else
            {
                await DisplayAlert("Error", message ?? "Failed to restore task.", "OK");
            }
        }
    }

    private void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var task = button?.CommandParameter as ToDoClass;
        if (task != null)
        {
            ToDoPage.MyTasks.Remove(task);
            RefreshList();
        }
    }

    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null) return;
        var selectedTask = e.SelectedItem as ToDoClass;
        await Navigation.PushAsync(new TaskDetailsPage(selectedTask));
        CompletedListView.SelectedItem = null;
    }
}
