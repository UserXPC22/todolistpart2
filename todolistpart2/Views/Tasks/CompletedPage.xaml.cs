using System.Linq;

namespace todolistpart2.Views.Tasks;

public partial class CompletedPage : ContentPage
{
    public CompletedPage()
    {
        InitializeComponent();
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