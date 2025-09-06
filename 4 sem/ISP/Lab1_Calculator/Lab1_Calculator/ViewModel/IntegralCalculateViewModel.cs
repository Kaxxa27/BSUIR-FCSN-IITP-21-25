using Lab1_Calculator.View;
using Lab1_Calculator.ViewModel.BaseViewModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.TizenSpecific;
using Microsoft.Maui.Platform;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Input;


namespace Lab1_Calculator.ViewModel;

public partial class IntegralCalculateViewModel : BaseViewModel.BaseViewModel
{
    private CancellationTokenSource cancellationTokenSource = new();

    int countOfRectangle = 10000;
    double LeftBorder = 0;
    double RightBorder = 1;
    double Procent = 0;
    double Step;

    #region Progress
    private double progress = 0;
    public double Progress
    {
        get => progress;
        set => SetProperty(ref progress, value);
    }
    #endregion

    #region Result
    private double result;
    public double Result
    {
        get => result;
        set => SetProperty(ref result, value);
    }
    #endregion

    #region ResultString
    private string resultString = "Welcome to IntegralCalculatePage!";
    public string ResultString
    {
        get => resultString;
        set => SetProperty(ref resultString, value);
    }
    #endregion

    #region IsBusy
    private bool isBusy = true;
    public bool IsBusy
    {
        get => isBusy;
        set => SetProperty(ref isBusy, value);
    }
    #endregion

    #region ExecutionResultString
    private string executionResultString = "0%";
    public string ExecutionResultString
    {
        get => executionResultString;
        set => SetProperty(ref executionResultString, value);
    }
    #endregion

    #region Commands
    public ICommand StartCommand { get; set; }
    public ICommand CancelCommand { get; set; }
    #endregion

    public IntegralCalculateViewModel()
    {
      
        Step = (RightBorder - LeftBorder) / countOfRectangle;
        CancelCommand = new Command(() => Cancel());
        StartCommand = new Command(execute: async () =>
        {
            IsBusy = false;
            await CalcIntegral(cancellationTokenSource.Token);
            //MainThread.InvokeOnMainThreadAsync(() => { IsBusy = true; }).Wait();
        }, canExecute: () => IsBusy);
    }


    void Cancel()
    {
        cancellationTokenSource.Cancel();
       // cancellationTokenSource.Dispose();

        //Progress = 0;
        //IsBusy = true;
        //Result = 0;
        //ExecutionResultString = "0%";
        //ResultString = "Выполнение отменено.";
    }
    async Task CalcIntegral(CancellationToken cancellationToken)
    {
        // IsBusy = false;
        //((Command)StartCommand).ChangeCanExecute();

        ResultString = "Вычисление...";
        var progress = new Progress<double>(percent =>
        {
            Progress = percent;
        });


        await Task.Run(() => Run(progress, cancellationToken));

     


        //await Task.Run(() =>
        //{
        //    sem.WaitOne();

        //    //DispatcherExtensions.DispatchAsync(IntegralProgressBar, (Task t) =>
        //    //{
        //    //    Progress = Procent;
        //    //});
        //    //for (double i = 0; i < countOfRectangle; i++)
        //    //{
        //    //    Task.Delay(1);
        //    //    Result += Math.Sin(LeftBorder + Step / 2 + i * Step);
        //    //    ExecutionResultString = $"{(int)(i / (countOfRectangle / 100))}%";
        //    //    Procent = (i / (countOfRectangle / 100) / 100);
        //    //    Progress = Procent;
        //    //}
        //    sem.Release();
        //});



        // IsBusy = true;
        //((Command)StartCommand).ChangeCanExecute();

    }

    void Run(IProgress<double> progress, CancellationToken cancellationToken)
    {
        for (double i = 0; i < countOfRectangle; i++)
        {
            if (!cancellationToken.IsCancellationRequested)
            {

                Task.Delay(100);
                for (int j = 0; j < 100000; j++)
                {
                    int g = j * j * j * j * j * j * j * j * j;
                }
                Result += Math.Sin(LeftBorder + Step / 2 + i * Step);
                ExecutionResultString = $"{(int)(i / (countOfRectangle / 100))}%";
                Procent = (i / (countOfRectangle / 100) / 100);
                progress.Report(Procent);
             //   MainThread.InvokeOnMainThreadAsync(()=> progress.Report(Procent)).Wait();
            }
            else
            {
                progress.Report(0);
               // IsBusy = true;
                Result = 0;
                ExecutionResultString = "0%";
                ResultString = "Выполнение отменено.";
                return;
            }

        }

        ExecutionResultString = "100%";
        Progress = 1;
        Result *= Step;
        ResultString = $"Ответ: {Result.ToString()}";

    }
}
