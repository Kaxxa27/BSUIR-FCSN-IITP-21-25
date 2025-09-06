using System;

namespace Lab1;

class Program
{
    static void Main(string[] args)
    {
        List<int> list = new List<int>() { 1, 2, 3, 4, 5 };

        Stack<int> stack = new Stack<int>();
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);

        Queue<string> queue = new Queue<string>();
        queue.Enqueue("first");
        queue.Enqueue("second");
        queue.Enqueue("third");

        int[] array = new int[] { 10, 20, 30, 40, 50 };

        Dictionary<string, int> dictionary = new Dictionary<string, int>();
        dictionary.Add("one", 1);
        dictionary.Add("two", 2);
        dictionary.Add("three", 3);

        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine("for: " + i);
        }

        int j = 0;
        while (j < 5)
        {
            j++;
        }

        int k = 0;
        do
        {
            k++;
        } while (k < 5);

        RecursiveFunction(5);

        void LocalFunction()
        {
            Console.WriteLine("This is a local function.");
        }
        LocalFunction();

        bool logicalAnd = true && false;
        bool logicalOr = true || false;
        bool logicalNot = !true;

        bool equal = (10 == 10);
        bool notEqual = (20 != 30);
        bool greaterThan = (40 > 30);
        bool lessThan = (50 < 60);
        bool greaterThanOrEqual = (70 >= 70);
        bool lessThanOrEqual = (80 <= 90);

        int x = 5;
        int y = (x > 0) ? 1 : -1;

        int num = 10;
        num += 5; // Прибавление
        num -= 3; // Вычитание
        num *= 2; // Умножение
        num /= 4; // Деление
        num %= 3; // Остаток от деления

        int[] numbers = { 1, 2, 3, 4, 5 };
        Console.WriteLine("Элемент по индексу 2: " + numbers[2]);

        string str = "Hello";
        Console.WriteLine(str.Length);

    }

    static void RecursiveFunction(int n)
    {
        if (n > 0)
        {
            RecursiveFunction(n - 1);
            Console.WriteLine("Recursive: " + n);
        }
    }
}
