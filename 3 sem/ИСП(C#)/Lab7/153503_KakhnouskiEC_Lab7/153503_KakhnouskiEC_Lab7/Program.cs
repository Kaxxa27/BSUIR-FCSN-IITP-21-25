using System.Diagnostics;
using System.Threading;
using System.Xml.Linq;
using ClassLibrary;


namespace _153503_KakhnouskiEC_Lab7
{
    internal class Program
    {

        static void Main(string[] args)
        {
            IntegralCalculate CalInt1 = new IntegralCalculate(0,1,10000);
            IntegralCalculate CalInt2 = new IntegralCalculate(0,1,10000);
            IntegralCalculate CalInt3 = new IntegralCalculate(0,1,10000);
            IntegralCalculate CalInt4 = new IntegralCalculate(0,1,10000);
         

            Thread FirstThread = new Thread(CalInt1.CalcIntegral) { Name = "First", Priority = ThreadPriority.Highest };
            FirstThread.Start();

            Thread SecondThread = new Thread(CalInt2.CalcIntegral) { Name = "Second", Priority = ThreadPriority.Lowest };
            SecondThread.Start();

            Thread ThirdThread = new Thread(CalInt3.CalcIntegral) { Name = "Third" };
            ThirdThread.Start();


            Thread FourThread = new Thread(CalInt4.CalcIntegral) { Name = "Four" };
            FourThread.Start();
        }
    }
}