using myfirstmaui.ViewModel;

namespace myfirstmaui;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel mv)
	{
		InitializeComponent();
		BindingContext = mv;
	}
}

