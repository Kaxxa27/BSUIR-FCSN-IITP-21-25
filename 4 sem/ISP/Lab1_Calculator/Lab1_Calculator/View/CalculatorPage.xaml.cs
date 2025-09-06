
using Lab1_Calculator.ViewModel;

namespace Lab1_Calculator.View;

public partial class CalculatorPage : ContentPage
{
	public CalculatorPage(CalculatorViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}