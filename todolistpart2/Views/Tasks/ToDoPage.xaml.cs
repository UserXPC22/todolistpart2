using System.Collections.ObjectModel;
using System.Linq;
using todolistpart2.Services;

namespace todolistpart2.Views.Tasks;

public partial class ToDoPage : ContentPage
{
    private readonly IAuthService _authService;

    // The static list that all other pages reference
    public static ObservableCollection<ToDoClass> MyTasks { get; set; } = new ObservableCollection<ToDoClass>();

    public ToDoPage()
    {
        InitializeComponent();
        _authService = new AuthService();

        RefreshListView();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        RefreshListView();
    }

    private void RefreshListView()
    {
        // Only show tasks that are NOT completed
        TodoListView.ItemsSource = MyTasks.Where(t => t.status != "Completed").ToList();
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddToDoPage());
    }

    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null) return;
        var selectedTask = e.SelectedItem as ToDoClass;
        await Navigation.PushAsync(new TaskDetailsPage(selectedTask));
        TodoListView.SelectedItem = null;
    }

    private async void OnCompleteButtonClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var task = button?.CommandParameter as ToDoClass;
        if (task != null)
        {
            var (success, message) = await _authService.UpdateItemStatus(task.item_id, "inactive");

            if (success)
            {
                task.status = "Completed";
                await DisplayAlert("Success", message ?? "Task marked as completed!", "OK");
                RefreshListView();
            }
            else
            {
                await DisplayAlert("Error", message ?? "Failed to complete task.", "OK");
            }
        }
    }

    private void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var task = button?.CommandParameter as ToDoClass;
        if (task != null)
        {
            MyTasks.Remove(task);
            RefreshListView();
        }
    }
}
