using Lab1_Calculator.ViewModel;
using Lab1_Calculator.View;
using Lab1_Calculator.Services;

namespace Lab1_Calculator;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				//fonts.AddFont("CalculateSymbolStyle.ttf", "CalculateSymbolStyle");
				fonts.AddFont("Segoe-UI.ttf", "CalculateSymbolStyle");
			});


		// ViewModels/
		builder.Services.AddSingleton<CalculatorViewModel>();
		builder.Services.AddTransient<IntegralCalculateViewModel>();
		builder.Services.AddTransient<SqlLitePickerViewModel>();
		builder.Services.AddTransient<CurrencyConverterViewModel>();

        // View/
        builder.Services.AddSingleton<CalculatorPage>();
        builder.Services.AddTransient<IntegralCalculatePage>();
        builder.Services.AddTransient<SqlLitePickerPage>();
        builder.Services.AddTransient<СurrencyСonverterPage>();


		//services
		builder.Services.AddTransient<IDbService, SQLiteService>();
		builder.Services.AddHttpClient<IRateService, RateService>(opt => opt.BaseAddress = new Uri("https://www.nbrb.by/api/exrates/rates"));
		//builder.Services.AddSingleton<IRateService, RateService>();

        return builder.Build();
	}
}
