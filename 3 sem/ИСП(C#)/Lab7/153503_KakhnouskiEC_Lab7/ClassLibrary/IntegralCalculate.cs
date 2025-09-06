using System.Diagnostics;

namespace ClassLibrary
{
    public class IntegralCalculate
    {
        public delegate void AccountHandler(string message);
        public event AccountHandler? Notify;


        static Semaphore sem = new Semaphore(2, 2);


        public double Result { get; set; } = 0;
        public double h { get; set; } = 0; // h = b - a / n

        public double a { get; set; }
        public double b { get; set; }
        public double countOfRectangle { get; set; }

        public IntegralCalculate(double a, double b, double countOfRectangle)
        {
            Notify += (message) => Console.WriteLine(message);
            this.a = a;
            this.b = b;
            this.countOfRectangle = countOfRectangle;
        }

        public void CalcIntegral()
        {
            sem.WaitOne();
            Stopwatch sw = Stopwatch.StartNew();

            h = (b - a) / countOfRectangle;

            for (double i = 0, k = countOfRectangle; i < countOfRectangle; i++)
            {

                Result += Math.Sin(a + h / 2 + i * h);
                Notify?.Invoke($"Поток {Thread.CurrentThread.Name} Выполнено {i / (countOfRectangle / 100)}%");


            }
            Result *= h;

            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine($"Поток {Thread.CurrentThread.Name} RunTime " + elapsedTime);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Поток {Thread.CurrentThread.Name} Result SIN(x) = {Result}");
            Console.ResetColor();
            sem.Release();
        }
    }
}