using Lab1_Calculator.Services;
using Microsoft.Maui.ApplicationModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Windows.Networking.Sockets;

namespace Lab1_Calculator.ViewModel;

public class CurrencyConverterViewModel : BaseViewModel.BaseViewModel
{
    IRateService _rateService;
    NetworkAccess accessType;

    #region Commands
    public ICommand DatePickCommand { get; set; }
    public ICommand ConvertCommand { get; set; }

    #endregion

    #region Properties

    #region Converts Values

    #region CurrencyBYN
    private decimal _currencyBYN;

    public decimal CurrencyBYN
    {
        get => _currencyBYN;
        set => SetProperty(ref _currencyBYN, value);
    }

    #endregion

    #region CurrencyToConvertBYN
    private decimal _currencyToConvertBYN;

    public decimal CurrencyToConvertBYN
    {
        get => _currencyToConvertBYN;
        set => SetProperty(ref _currencyToConvertBYN, value);
    }

    #endregion

    #region CurrencyAny
    private decimal _currencyConvertAny;

    public decimal CurrencyAny
    {
        get => _currencyConvertAny;
        set => SetProperty(ref _currencyConvertAny, value);
    }

    #endregion

    #region CurrencyToConvertAny
    private decimal _currencyToConvertAny;

    public decimal CurrencyToConvertAny
    {
        get => _currencyToConvertAny;
        set => SetProperty(ref _currencyToConvertAny, value);
    }

    #endregion

    #endregion

    #region Rates
    private ObservableCollection<Rate> _rates = new();

    public ObservableCollection<Rate> Rates
    {
        get => _rates;
        set
        {
            SetProperty(ref _rates, value);
            OnPropertyChanged(nameof(Rates));
        }
    }
    #endregion

    #region MaxDate
    private DateTime _maxDate = DateTime.Today;

    public DateTime MaxDate
    {
        get => _maxDate;
        set => SetProperty(ref _maxDate, value);
    }
    #endregion

    #region MinDate
    private DateTime _minDate = new DateTime(2000, 1, 1);

    public DateTime MinDate
    {
        get => _minDate;
        set => SetProperty(ref _minDate, value);
    }
    #endregion

    #region SelectedDate

    private DateTime _selectedDate = DateTime.Today;

    public DateTime SelectedDate
    {
        get => _selectedDate;
        set => SetProperty(ref _selectedDate, value);
    }

    #endregion

    #region CurrentRate

    private Rate _currentRate;

    public Rate CurrentRate
    {
        get => _currentRate;
        set
        {
            SetProperty(ref _currentRate, value);
            OnPropertyChanged(nameof(CurrentRate));
        }
    }

    #endregion

    #endregion

    #region Constructor
    public CurrencyConverterViewModel(IRateService rateService)
    {
        _rateService = rateService;

        DatePickCommand = new Command(() => DatePick());
        ConvertCommand = new Command(() => ConvertCurrency());

        DatePick();
        CurrentRate = Rates.FirstOrDefault();
        OnPropertyChanged(nameof(Rates));
    }
    #endregion

    async Task<IEnumerable<Rate>> GetRates()
    {
        return await _rateService.GetRates(SelectedDate);
    }

    void DatePick()
    {
        accessType = Connectivity.Current.NetworkAccess;

        if (accessType == NetworkAccess.Internet)
        {
            Rates.Clear();

            Rates = new ObservableCollection<Rate>(GetRates().Result
                .Where(f => f.Cur_Abbreviation == "USD"
                || f.Cur_Abbreviation == "RUB"
                || f.Cur_Abbreviation == "EUR"
                || f.Cur_Abbreviation == "CHF"
                || f.Cur_Abbreviation == "CNY"
                || f.Cur_Abbreviation == "GBP")
                .ToArray());
            OnPropertyChanged(nameof(Rates));
        }
        else
        {
            Shell.Current.DisplayAlert("Warning", "There's no Internet connection.\nTry again.", "OK");
            Rates = new ObservableCollection<Rate>() 
            { new Rate() { 
                Cur_Name = "Internet == NULL",
                Cur_Abbreviation = "Oops", 
                Cur_OfficialRate = 404,
                Cur_Scale = 404
            }};
            OnPropertyChanged(nameof(Rates));
        }

    }

    void ConvertCurrency()
    {
        CurrencyToConvertBYN = CurrencyBYN / CurrentRate.Cur_OfficialRate * CurrentRate.Cur_Scale ?? 0;
        CurrencyToConvertAny = CurrencyAny * CurrentRate.Cur_OfficialRate / CurrentRate.Cur_Scale ?? 0;
    }

}
