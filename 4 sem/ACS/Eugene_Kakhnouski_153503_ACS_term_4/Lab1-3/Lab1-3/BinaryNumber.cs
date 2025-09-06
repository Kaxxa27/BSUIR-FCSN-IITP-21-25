namespace Lab1_3;

public class BinaryNumber
{
    private int[] _binaryNumber = new int[32];
    public int[] BinaryValue
    {
        get => _binaryNumber;
        set => _binaryNumber = value;
    }

    private int[] _additionalNumber = new int[32];
    public int[] AdditionalValue
    {
        get => _additionalNumber;
        set => _additionalNumber = value;
    }

    #region Constructors
    public BinaryNumber()
    {
        _binaryNumber = new int[32];
        _additionalNumber = new int[32];
    }




    /// <summary>
    ///  int[] arr = {0,1,0...1}
    ///  Only 1 or 0 elements.
    /// </summary>
    /// <param name="number"></param> 
    public BinaryNumber(int[] number)
    {
        //if (number.Length > 32 || number.Length < 0)
        //    throw new Exception("Array length isn't correct. ( 0 < lenght < 32)");

        ToBinary(ToDecimalNumberSystem(number));
    }
    public BinaryNumber(int value)
    {
        ToBinary(value);
    }

    #endregion

    #region ToBinary
    void ToBinary(int value)
    {
        int[] result = new int[32];
        int j = 31;

        int count = Math.Abs(value);
        while (count > 0)
        {
            result[j--] = count % 2;
            count >>= 1;
        }

        // Если число отрицательное, то перевести в дополнительный код явно.
        // Т.к. если положительное, то прямой код == доп. коду.  

        if (value < 0)
        {
            _additionalNumber = ToAdditionalCode((int[])result.Clone());
            result[0] = 1;
            _binaryNumber = result;
        }       
        else
        {
            _additionalNumber = result;
            _binaryNumber = result;
        }
    }
    #endregion

    #region ToAdditionalCode
    int[] ToAdditionalCode(int[] arr)
    {

        for (int i = 0; i < 32; ++i)
            arr[i] = (arr[i] + 1) % 2;

        int j = 31;

        arr[j]++;

        while (arr[j] == 2)
        {
            arr[j--] %= 2;
            arr[j]++;
        }

        return arr;
    }
    #endregion

    #region ToString

    public override string ToString()
    {
        string strNumber = string.Empty;

        foreach (var item in _binaryNumber)
        {
            strNumber += item.ToString();
        }

        return strNumber;
    }

    public string ToString(bool AdditionalNumber)
    {
        string strNumber = string.Empty;
        if (AdditionalNumber is true)
        {
            foreach (var item in _additionalNumber)
            {
                strNumber += item.ToString();
            }
        }
        else strNumber = ToString();

        return strNumber;
    }
    #endregion

    #region ToDecimalNumberSystem
    public long ToDecimalNumberSystem()
    {
        long Decimal = (long)-(_additionalNumber[0] * Math.Pow(2, 31));

        for (int i = 1; i < 32; ++i)
            Decimal += (long)(_additionalNumber[i] * Math.Pow(2, 31 - i));

        return Decimal;
    }

    static public int ToDecimalNumberSystem(int[] arr)
    {
        int Decimal = (int)-(arr[0] * Math.Pow(2, 31));

        for (int i = 1; i < 32; ++i)
            Decimal += (int)(arr[i] * Math.Pow(2, 31 - i));

        return Decimal;
    }
    #endregion


    public BinaryNumber ToExpandedBitGrid()
    {
        var result = new BinaryNumber((int)this.ToDecimalNumberSystem());

        int[] additionalArray = new int[result.AdditionalValue.Length];

        if (result.AdditionalValue.First() == 1)
        {
           
                result.BinaryValue[0 /*result.BinaryValue.Length - 1*/] = 0;
                additionalArray[0 /*additionalArray.Length - 1*/] = 1;
           
            
                for (int i = additionalArray.Length; i > 0; i--)
                {
                    additionalArray[i] = 1;
                }
        }

        result.AdditionalValue = result.AdditionalValue.Concat(additionalArray).ToArray();

        return result;
    }
}

