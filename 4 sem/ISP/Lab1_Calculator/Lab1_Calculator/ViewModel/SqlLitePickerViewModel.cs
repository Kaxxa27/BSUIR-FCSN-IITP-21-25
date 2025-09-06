
using Lab1_Calculator.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Lab1_Calculator.ViewModel;

public class SqlLitePickerViewModel : BaseViewModel.BaseViewModel
{
	IDbService _db;
	public ICommand RefreshCommand { get; set; }

    #region IsRefreshing
    private bool _isRefreshing;
    public bool IsRefreshing
    {
        get => _isRefreshing;
        set => SetProperty(ref _isRefreshing, value);
    }
    #endregion

    #region Brigade
    private Brigade _brigade;
    public Brigade Brigade
    {
        get { return _brigade; }
        set
        {
            if (SetProperty(ref _brigade, value))
                updateJobs();
        }
    }
    #endregion

    #region ObservableCollection
    public ObservableCollection<Brigade> Brigades { get; set; } = new();
    public ObservableCollection<Job> Jobs { get; set; } = new();
    #endregion

    public SqlLitePickerViewModel(IDbService db)
	{
		_db = db;
		updateBrigades();

		RefreshCommand = new Command(async ()=> 
		{
			if (IsBusy) return;

			Jobs.Clear();
			await updateBrigades();
			Brigade = Brigades.FirstOrDefault();
		});

    }

	async Task updateBrigades()
	{
        if (IsBusy) return;

        IsBusy= true;
        IsRefreshing= true;

		Brigades = new ObservableCollection<Brigade>(await _db.GetAllBrigades());
		OnPropertyChanged(nameof(Brigades));


        IsBusy = false;
        IsRefreshing = false;
    }


    async Task updateJobs()
    {
        if (IsBusy) return;


        IsBusy = true;
        IsRefreshing = true;

      //  await Task.Delay(500);

        Jobs = new ObservableCollection<Job>(await _db.GetBrigadeJob(Brigade.Id));
        OnPropertyChanged(nameof(Jobs));


        IsBusy = false;
        IsRefreshing = false;
    }

	

}