using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Lab1_3;

internal class Program
{
    static void Main(string[] args)
    {
        // On new Branch -> NewTestBranch for test merge to Lab1
        // Now on Laptop on NewTestBranch
        BinaryNumber FirstNumber;
        BinaryNumber SecondNumber;
        try
        {
            Console.Write("Enter first number: ");
            FirstNumber = new BinaryNumber(Int32.Parse(Console.ReadLine()));
            Console.Write("Enter second number: ");
            SecondNumber = new BinaryNumber(Int32.Parse(Console.ReadLine()));
            Console.WriteLine();
        }
        catch (Exception)
        {
            FirstNumber = new(0);
            SecondNumber = new(0);
            Console.WriteLine("Неккоректный ввод.");
        }

        BinaryNumber Sum = BinaryOperation.Sum(FirstNumber, SecondNumber);
        BinaryNumber Sub = BinaryOperation.Subtraction(FirstNumber, SecondNumber);
        BinaryNumber Mul = BinaryOperation.Multiplication(FirstNumber, SecondNumber);
        (BinaryNumber,BinaryNumber) Div = BinaryOperation.Div(FirstNumber, SecondNumber);

        Console.WriteLine($"First Number: {FirstNumber.ToDecimalNumberSystem()}");
        Console.WriteLine($"First BinaryNumber: {FirstNumber.ToString()}");
        Console.WriteLine($"First negative Number: {BinaryOperation.Negative(FirstNumber).ToDecimalNumberSystem()}");
        Console.WriteLine($"First negative BinaryNumber: {BinaryOperation.Negative(FirstNumber).ToString()}\n");
        

        Console.WriteLine($"Second Number: {SecondNumber.ToDecimalNumberSystem()}");
        Console.WriteLine($"Second BinaryNumber: {SecondNumber.ToString()}");
        Console.WriteLine($"Second negative Number: {BinaryOperation.Negative(SecondNumber).ToDecimalNumberSystem()}");
        Console.WriteLine($"Second negative BinaryNumber: {BinaryOperation.Negative(SecondNumber).ToString()}\n");

        Console.WriteLine($"Sum Number: {Sum.ToDecimalNumberSystem()}");
        Console.WriteLine($"Sum BinaryNumber: {Sum.ToString()}");
        Console.WriteLine($"Sum negative Number: {BinaryOperation.Negative(Sum).ToDecimalNumberSystem()}");
        Console.WriteLine($"Sum negative BinaryNumber: {BinaryOperation.Negative(Sum).ToString()}\n");
       
        Console.WriteLine($"Sub Number: {Sub.ToDecimalNumberSystem()}");
        Console.WriteLine($"Sub BinaryNumber: {Sub.ToString()}");
        Console.WriteLine($"Sub negative Number: {BinaryOperation.Negative(Sub).ToDecimalNumberSystem()}");
        Console.WriteLine($"Sub negative BinaryNumber: {BinaryOperation.Negative(Sub).ToString()}\n");

        Console.WriteLine($"Mul Number: {Mul.ToDecimalNumberSystem()}");
        Console.WriteLine($"Mul BinaryNumber: {Mul.ToString()}");
        Console.WriteLine($"Mul negative Number: {BinaryOperation.Negative(Mul).ToDecimalNumberSystem()}");
        Console.WriteLine($"Mul negative BinaryNumber: {BinaryOperation.Negative(Mul).ToString()}\n");


        Console.WriteLine($"Div Number(частное): {Div.Item1.ToDecimalNumberSystem()}");
        Console.WriteLine($"Div Number(остаток): {Div.Item2.ToDecimalNumberSystem()}");
        Console.WriteLine($"Div BinaryNumber(частное): {Div.Item1.ToString(true)}");
        Console.WriteLine($"Div BinaryNumber(остаток): {Div.Item2.ToString(true)}");
      //  Console.WriteLine($"Div negative Number: {BinaryOperation.Negative(Mul).ToDecimalNumberSystem()}");
       // Console.WriteLine($"Div negative BinaryNumber: {BinaryOperation.Negative(Mul).ToString()}\n");
    }
}

