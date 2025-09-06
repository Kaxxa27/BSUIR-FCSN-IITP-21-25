using Lab1_Calculator.ViewModel;

namespace Lab1_Calculator.View;

public partial class СurrencyСonverterPage : ContentPage
{
	public СurrencyСonverterPage(CurrencyConverterViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}