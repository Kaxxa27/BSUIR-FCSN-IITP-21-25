
using System;
using System.Windows.Input;

namespace Lab1_Calculator.ViewModel;

public class CalculatorViewModel : BaseViewModel.BaseViewModel
{
    #region Properties   
    // Вывод процесса операции.
    #region ProcessLabel
    private string processLabel = "";
    public string ProcessLabel
    {
        get => processLabel;
        set => SetProperty(ref processLabel, value);
    }
    #endregion

    // Результат оперции.
    #region ResultLabel
    private string resultLabel = "";

    public string ResultLabel
    {
        get => resultLabel;
        set => SetProperty(ref resultLabel, value);
    }

    #endregion

    // Если CE использовано на второе число.
    #region IsSecondNumberCE
    private bool isSecondNumberCE;

    public bool IsSecondNumberCE
    {
        get => isSecondNumberCE;
        set => SetProperty(ref isSecondNumberCE, value);
    }

    #endregion

    // Первое число.
    #region FirstNumber
    private double firstNumber = 0;

    public double FirstNumber
    {
        get => firstNumber;
        set => SetProperty(ref firstNumber, value);
    }

    #endregion

    // Второе число.
    #region SecondNumber
    private double secondNumber = 0;

    public double SecondNumber
    {
        get => secondNumber;
        set => SetProperty(ref secondNumber, value);
    }

    #endregion

    // Результат.
    #region Result
    private double result;

    public double Result
    {
        get => result;
        set => SetProperty(ref result, value);
    }

    #endregion

    // Математический оператор.
    #region MathOperator
    private string mathOperator = "";

    public string MathOperator
    {
        get => mathOperator;
        set => SetProperty(ref mathOperator, value);
    }

    #endregion

    //  Состояние, определяющее номер заполнения числа.
    #region NumberState
    /// <summary>
    ///  1 - первое число.
    ///  2 - второе число.
    /// </summary>
    private int numberState;

    public int NumberState
    {
        get => numberState;
        set => SetProperty(ref numberState, value);
    }

    #endregion

    //  Формат, для нецелых чисел.
    #region DecimalFormat
    /// <summary>
    ///  "NO" - 0 знаков после запятой.
    ///  "N1" - 1 знак после запятой.
    ///  "N2" - 2 знака после запятой.
    /// </summary>
    private string decimalFormat;

    public string DecimalFormat
    {
        get => decimalFormat;
        set => SetProperty(ref decimalFormat, value);
    }

    #endregion

    #endregion

    #region Commands
    public ICommand NumberButtonClickCommand { get; set; }
    public ICommand MathOperatorButtonClickCommand { get; set; }
    public ICommand CalculateClickCommand { get; set; }
    public ICommand OnClearCommand { get; set; }
    public ICommand OnClearNumberCommand { get; set; }
    public ICommand BackSpaceCommand { get; set; }
    public ICommand SineCommand { get; set; }
    public ICommand SqrtCommand { get; set; }
    public ICommand SqDegreeCommand { get; set; }
    public ICommand DivisionByXCommand { get; set; }
    public ICommand NegativeCommand { get; set; }
    #endregion

    #region CalculatorViewModel_Ctor
    public CalculatorViewModel()
    {
        OnClear();
        Title = "Обычный";

        #region ImplementingCommands

        NumberButtonClickCommand = new Command<string>((string text) => OnSelectNumber(text));
        MathOperatorButtonClickCommand = new Command<string>((string mathOperator) => OnSelectOperator(mathOperator));
        CalculateClickCommand = new Command(() => OnCalculate());
        OnClearCommand = new Command(() => OnClear());
        BackSpaceCommand = new Command(() => BackSpace());
        SineCommand = new Command(() => Sine());
        SqrtCommand = new Command(() => Sqrt());
        SqDegreeCommand = new Command(() => SqDegree());
        DivisionByXCommand = new Command(() => DivisionByX());
        NegativeCommand = new Command(() => Negative());
        OnClearNumberCommand = new Command(() =>
        {
            if (NumberState is 1)
            {
                ProcessLabel = "0";
                ResultLabel = "0";
            }
            else
            {
                IsSecondNumberCE = true;
                ResultLabel = "0";
                ProcessLabel = $"{FirstNumber} {MathOperator}";
            }
        });
        #endregion
    }
    #endregion

    #region OnSelectNumber
    void OnSelectNumber(string text)
    {
        if (ProcessLabel.EndsWith(',') is true && text is ","
            || ProcessLabel.Contains(",") is true && text is ",") { }
        else
        {
            if(ProcessLabel is "" && ResultLabel is not "") ProcessLabel = ResultLabel;

            if (NumberState is 1)
            {
                if (ProcessLabel is "0" && text is not ",")  
                    ProcessLabel = text;              
                else
                    ProcessLabel += text;
            }        
        }


        if ((ResultLabel is "0" && text is "0")
            || (ProcessLabel.Length <= 1 && text is not "0")
            || NumberState < 0)
        {
            ResultLabel = String.Empty;
            if (NumberState < 0)
                NumberState *= -1;
        }

        if (text is "," )
        {
            if (ResultLabel.Contains(','))
                return;

            if (ResultLabel is "" && NumberState is 2)          
                ResultLabel = "0";
            
            ResultLabel += ',';
            return;
        }

        if(ResultLabel is "0")       
            ResultLabel = text;  
        else 
            ResultLabel += text;
    }
    #endregion

    #region OnClean
    /// <summary>
    /// Обнуление переменных.
    /// </summary>
    /// 
    void OnClear()
    {
        NumberState = 1;
        DecimalFormat = "N5";
        ResultLabel = "0";
        ProcessLabel = "0";
    }
    #endregion

    #region OnSelectOperator
    /// <summary>
    /// Обработка математического оператора.
    /// </summary>
    /// <param name="mathOperator"></param>
    /// 
    void OnSelectOperator(string mathOperator)
    {
        LockNumberValue(ResultLabel);

        if (NumberState is 2)
        {         
            OnCalculate();
            if (ProcessLabel is not "0")
            {
                NumberState = -2;
            }
            ProcessLabel = $"{FirstNumber} {MathOperator} ";
            ResultLabel = "0";  
            return;
        }
        NumberState = -2;

        MathOperator = mathOperator;

        ResultLabel = "0";
        ProcessLabel = $"{FirstNumber} {MathOperator} ";
    }
    #endregion

    #region LockNumberValue
    /// <summary>
    /// Выбор обработки какого числа.
    /// </summary>
    /// <param name="text"></param>
    private void LockNumberValue(string text)
    {
        double number;
        if (double.TryParse(text, out number))
        {
            if (NumberState is 1)
            {
                FirstNumber = number;
            }
            else
            {
                SecondNumber = number;
            }

            ProcessLabel = string.Empty;
        }
    }
    #endregion

    #region OnCalculate
    void OnCalculate()
    {
        if (NumberState is 2)
        {
            if (SecondNumber is 0)
                LockNumberValue(ResultLabel);

            Calculate();

            ResultLabel = Result.ToString();
            ResultLabel = ToTrimmedString(ResultLabel);
            FirstNumber = Result;
            SecondNumber = 0;
            NumberState = 1;
            ProcessLabel = string.Empty;
        }
    }

    #region ToTrimmedString
    public string ToTrimmedString<T>(T value/*, string decimalFormat*/)
    {
        string strValue = value.ToString(); //Get the stock string

        //If there is a decimal point present
        if (strValue.Contains(","))
        {
            //Remove all trailing zeros
            strValue = strValue.TrimEnd('0');
        
            if (strValue.EndsWith(",")) 
                strValue = strValue.TrimEnd(',');
        }

        return strValue;
    }
    #endregion

    #region Calculate
    void Calculate()
    {
        double value1 = FirstNumber;
        double value2 = SecondNumber;
        string mathOperator = MathOperator;


        switch (mathOperator)
        {
            case "÷":
                Result = value1 / value2;
                break;
            case "×":
                Result = value1 * value2;
                break;
            case "+":
                Result = value1 + value2;
                break;
            case "-":
                Result = value1 - value2;
                break;
        }

    }
    #endregion

    #endregion

    #region BackSpace
    void BackSpace()
    {
        if(ResultLabel is not "0")
        {
            ResultLabel = ResultLabel.Substring(0, ResultLabel.Length - 1);  // ctrl + k + d форматирование !!!

            if (ResultLabel is "")
                ResultLabel = "0";

            if (NumberState is 1)
            {
                ProcessLabel = ResultLabel;
            }                 
        }
    }
    #endregion

    #region Sine
    void Sine()
    {
        if (NumberState is 1)
        {
            LockNumberValue(ResultLabel);
            ResultLabel = Math.Sin(Math.PI * FirstNumber / 180.0).ToString();
        }
    }
    #endregion

    #region Sqrt
    void Sqrt()
    {
        if (NumberState is 1)
        {
            LockNumberValue(ResultLabel);
            ResultLabel = Math.Sqrt(FirstNumber).ToString();
        }
    }
    #endregion

    #region SqDegree
    void SqDegree()
    {
        if (NumberState is 1)
        {
            LockNumberValue(ResultLabel);
            ResultLabel = Math.Pow(FirstNumber,2).ToString();
        }
    }
    #endregion

    #region DivisionByX
    void DivisionByX()
    {
        if (NumberState is 1)
        {
            LockNumberValue(ResultLabel);
            ResultLabel = (1 / FirstNumber).ToString();
        }
    }
    #endregion

    #region Negative
    void Negative()
    {
        if (NumberState is 1)
        {
            if (ResultLabel is "0")
                return;

            LockNumberValue(ResultLabel);
            ResultLabel =  (FirstNumber * -1).ToString();
        }
    }
    #endregion

}

