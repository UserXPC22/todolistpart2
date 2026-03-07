using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace todolistpart2; // Removed curly braces to match your other files

public class ToDoClass : INotifyPropertyChanged
{
    private int _item_id;
    private string _item_name;
    private string _item_description;
    private string _status;
    private int _user_id;

    public int item_id 
    { 
        get => _item_id; 
        set { _item_id = value; OnPropertyChanged(); } 
    }

    public string item_name 
    { 
        get => _item_name; 
        set { _item_name = value; OnPropertyChanged(); } 
    }

    public string item_description 
    { 
        get => _item_description; 
        set { _item_description = value; OnPropertyChanged(); } 
    }

    public string status 
    { 
        get => _status; 
        set { _status = value; OnPropertyChanged(); } 
    }

    public int user_id 
    { 
        get => _user_id; 
        set { _user_id = value; OnPropertyChanged(); } 
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}