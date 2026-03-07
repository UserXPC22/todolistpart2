using System.Collections.ObjectModel;
using System.Linq;

namespace todolistpart2;

public partial class ToDoPage : ContentPage
{
    // The static list that all other pages reference
    public static ObservableCollection<ToDoClass> MyTasks { get; set; } = new ObservableCollection<ToDoClass>();

    public ToDoPage()
    {
        InitializeComponent();

        if (MyTasks.Count == 0)
        {
            MyTasks.Add(new ToDoClass { item_name = "Task 1", item_description = "Setup MAUI Navigation", status = "Pending" });
        }

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
            task.status = "Completed";
            await DisplayAlert("Success", "Task marked as completed!", "OK");
            RefreshListView();
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