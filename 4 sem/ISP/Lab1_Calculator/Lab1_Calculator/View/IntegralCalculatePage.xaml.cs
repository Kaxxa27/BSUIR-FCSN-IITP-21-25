using Lab1_Calculator.ViewModel;

namespace Lab1_Calculator.View;

public partial class IntegralCalculatePage : ContentPage
{
	public IntegralCalculatePage(IntegralCalculateViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}