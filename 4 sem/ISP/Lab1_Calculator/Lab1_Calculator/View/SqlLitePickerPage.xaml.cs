using Lab1_Calculator.ViewModel;

namespace Lab1_Calculator.View;

public partial class SqlLitePickerPage : ContentPage
{
	public SqlLitePickerPage(SqlLitePickerViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}