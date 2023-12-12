using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiSample8;

public class CustomHeader
{
    public string? Title { get; set; }
}

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
    bool canSetIsDirty;
    public bool IsDirty { get; set; }
    int initValue = 0;


    public MainPage()
	{
		InitializeComponent();
        BindingContext = this;        

        PopulateList();
        CustomHeader = new CustomHeader
        {
            Title = "This is a CollectionViewHeader"
        };
	}

    void Button_Clicked(System.Object sender, System.EventArgs e)
    {
        initValue++;
        CustomHeader = new CustomHeader
        {
            Title = $"This is a CollectionViewHeader #{initValue}"
        };
    }

    void PopulateList()
    {
        ItemList = new List<string>()
        {
            "This is an item",
            "This is an item",
            "This is an item",
            "This is an item",
            "This is an item",
            "This is an item"
        };
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }


    IList<string> itemList;
    public IList<string> ItemList
    {
        get => itemList;
        set => SetProperty(ref itemList, value);
    }

    CustomHeader customHeader;
    public CustomHeader CustomHeader
    {
        get => customHeader;
        set => SetProperty(ref customHeader, value);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action? onChanged = null, Func<T, T, bool>? validateValue = null)
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
        {
            return false;
        }

        if (validateValue != null && !validateValue!(backingStore, value))
        {
            return false;
        }

        backingStore = value;
        onChanged?.Invoke();
        OnPropertyChanged(propertyName);

        if (canSetIsDirty)
        {
            IsDirty = true;
            OnPropertyChanged(nameof(IsDirty));
        }

        return true;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}


