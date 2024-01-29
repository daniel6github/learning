using myfirstmaui.ViewModel;

namespace myfirstmaui;

public partial class DetailPage : ContentPage
{
	public DetailPage(DetailViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
	}
}