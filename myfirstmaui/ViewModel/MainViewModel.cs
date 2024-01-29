using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace myfirstmaui.ViewModel;

public partial class MainViewModel : ObservableObject
{   
    private IConnectivity _connectivity;

    [ObservableProperty]
    string text;

    [ObservableProperty]
    ObservableCollection<string> items;

    public MainViewModel(IConnectivity connectivity)
    {
        Items = new ObservableCollection<string>();
        connectivity = _connectivity;
    }

    [RelayCommand]
    async Task Add()
    {
        if (string.IsNullOrWhiteSpace(Text))
            return;

        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("Uh oh!", "No internet", "Ok");
            return;
        }
        
        Items.Add(Text);

        Text = string.Empty;

    }

    [RelayCommand]
    async Task Tap(string s)
    {
        await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Id={s}");
    }

    [RelayCommand]
    void Delete(string text)
    {
        if (Items.Contains(text))
            Items.Remove(text);
    }
}