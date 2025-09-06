using System.ComponentModel;

namespace Lab1_3;

class BinaryOperation
{
    #region Sum
    static public BinaryNumber Sum(BinaryNumber value1, BinaryNumber value2)
    {

        BinaryNumber result = new BinaryNumber();
        int over = 0;

        for (int i = 31; i >= 0; i--)
        {
            result.AdditionalValue[i] = value1.AdditionalValue[i] + value2.AdditionalValue[i] + over;

            over = result.AdditionalValue[i] / 2;

            result.AdditionalValue[i] %= 2;
        }

        result.BinaryValue = (new BinaryNumber((int)result.ToDecimalNumberSystem())).BinaryValue;

        if ((result.AdditionalValue[0] != value1.AdditionalValue[0]
            || result.AdditionalValue[0] != value2.AdditionalValue[0]) && value1.AdditionalValue[0] == value2.AdditionalValue[0])
            Console.WriteLine("Переполрнение суммы.");

        return result;
    }
    #endregion

    #region Negative
    static public BinaryNumber Negative(BinaryNumber value)
    {

        BinaryNumber result = new BinaryNumber((int[])value.AdditionalValue.Clone());

        for (int i = 0; i < value.AdditionalValue.Length; i++)
        {
            result.AdditionalValue[i] ^= 1;
        }

        if (BinaryNumber.ToDecimalNumberSystem(value.BinaryValue) > 0)
        {
            result.BinaryValue = (int[])value.BinaryValue.Clone();
            result.BinaryValue[0] = 1;
        }
        else
        {
            result.BinaryValue = (int[])value.BinaryValue.Clone();
            result.BinaryValue[0] = 0;
        }

            result.AdditionalValue = BinaryOperation.Sum(result, new BinaryNumber(1)).AdditionalValue;

        return result;
    }
    #endregion


    #region Subtraction
    static public BinaryNumber Subtraction(BinaryNumber value1, BinaryNumber value2)
    {
        value2 = Negative(value2);
        BinaryNumber result = new BinaryNumber();
        result.BinaryValue = Sum(value1, value2).BinaryValue;
        result.AdditionalValue = Sum(value1, value2).AdditionalValue;

        return result;
    }

    #endregion

    #region Multiplication
    static public BinaryNumber Multiplication(BinaryNumber value1, BinaryNumber value2)
    {
        if (value1.AdditionalValue.Length != value2.AdditionalValue.Length)
        {
            throw new NotImplementedException();
        }

        var M = new BinaryNumber((int)value1.ToDecimalNumberSystem()) ; // Множимое    
        var Q = new BinaryNumber((int)value2.ToDecimalNumberSystem()); // Множитель
        var A = new BinaryNumber();
        int Q_1 = 0;

        int Count = value1.AdditionalValue.Length / 2; // 16
 

        while (Count != 0)
        {
            if (Q.AdditionalValue.Last() == 1 && Q_1 == 0)
            {
                A = BinaryOperation.Subtraction(A, M);
            }
            else if (Q.AdditionalValue.Last() == 0 && Q_1 == 1)
            {
                A = BinaryOperation.Sum(A, M);
            }

            Q_1 = Q.AdditionalValue.Last();

            for (int i = 31; i > 16; i--)
            {
                Q.AdditionalValue[i] = Q.AdditionalValue[i - 1];
            }

            Q.AdditionalValue[16] = A.AdditionalValue.Last();

            for (int i = 31; i > 16 ; i--)
            {
                A.AdditionalValue[i] = A.AdditionalValue[i - 1];
            }

            Count--;
        }

        BinaryNumber result = new BinaryNumber();
        var truncated_Q = Q.AdditionalValue[16..].Reverse();
        var truncated_A = A.AdditionalValue[16..].Reverse();
        
        result.AdditionalValue = truncated_Q.Concat(truncated_A).Reverse().ToArray();
        result.BinaryValue = (new BinaryNumber((int)result.ToDecimalNumberSystem())).BinaryValue;

        return result;
    }
    #endregion

    #region Division

    static public (BinaryNumber, BinaryNumber) Div(BinaryNumber value1, BinaryNumber value2)
    {
        value1 = value1.ToExpandedBitGrid();

        if (value1.AdditionalValue.Length != 2 * value2.AdditionalValue.Length)
        {
            throw new InvalidDataException();
        }

        var A = new BinaryNumber((int)value1.ToDecimalNumberSystem());
        A.AdditionalValue = value1.AdditionalValue[value2.AdditionalValue.Length..];


        var Q = new BinaryNumber((int)value1.ToDecimalNumberSystem()); // Остаток
       
        Q.AdditionalValue = value1.AdditionalValue[..value2.AdditionalValue.Length];

        var M = new BinaryNumber((int)value2.ToDecimalNumberSystem()); /*b.ToAdditional(); // Делитель*/
        int Count = Q.AdditionalValue.Length;

        while (Count != 0)
        {
            // 2.
            for (int i = 0; i < A.AdditionalValue.Length - 1; i++)
            {
                A.AdditionalValue[i] = A.AdditionalValue[i + 1];
            }

            A.AdditionalValue[31] = Q.AdditionalValue.First();

            for (int i = 0; i < Q.AdditionalValue.Length - 1; i++)
            {
                Q.AdditionalValue[i] = Q.AdditionalValue[i + 1];
            }

            Q.AdditionalValue[31] = 0;

            var A_backup = new BinaryNumber((int)A.ToDecimalNumberSystem());

            if (M.AdditionalValue.First() == A.AdditionalValue.First())
            {
                A = BinaryOperation.Subtraction(A, M);
            }
            else
            {
                A = BinaryOperation.Sum(A, M);
            }

            if (A_backup.AdditionalValue.First() == A.AdditionalValue.First() ||
                (A.AdditionalValue.Sum() + Q.AdditionalValue.Sum() == 0))
            {
                Q.AdditionalValue[31] = 1;
            }
            else if (A_backup.AdditionalValue.First() != A.AdditionalValue.First() &&
                (A.AdditionalValue.Sum() + Q.AdditionalValue.Sum() != 0))
            {
                Q.AdditionalValue[31] = 0;
                A = A_backup;
            }

            Count--;
        }

        if (value1.AdditionalValue.First() != value2.AdditionalValue.First())
        {
            Q = BinaryOperation.Negative(Q);
        }

        return (Q, A);
    }
    #endregion
}

