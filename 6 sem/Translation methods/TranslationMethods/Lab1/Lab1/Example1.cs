namespace Lab1
{
    using System;

    class Example1
    {
        static void Main(string[] args)
        {
            int sumInt = 5 + 3;
            float sumFloat = 3.5f + 2.5f;

            int differenceInt = 10 - 4;
            float differenceFloat = 7.5f - 2.3f;

            int divisionInt = 20 / 4;
            float divisionFloat = 15.0f / 3.0f;

            int multiplicationInt = 6 * 4;
            float multiplicationFloat = 3.0f * 2.5f;

            int modulusInt = 10 % 3;

            int num1 = 5;
            num1++;
            num1--;

            bool isTrue = true;
            bool isFalse = false;

            string message = "Hello, ";
            string name = "John";
            string greeting = message + name;

            if (sumInt > 0)
            {
                Console.WriteLine("sumInt больше нуля.");
            }

            int switchValue = 2;
            switch (switchValue)
            {
                case 1:
                    Console.WriteLine("switchValue равно 1.");
                    break;
                case 2:
                    Console.WriteLine("switchValue равно 2.");
                    break;
                default:
                    Console.WriteLine("switchValue не равно ни 1, ни 2.");
                    break;
            }

            object obj1 = sumInt;
        }
    }

}
