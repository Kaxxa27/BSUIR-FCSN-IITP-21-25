using ClassLibraryForLab8;
using LoremNET;

namespace _153503_KakhnouskiEC_Lab8
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string FileName = @"C:\BSUIR\3 sem\ИСП(C#)\Lab8\153503_KakhnouskiEC_Lab8\153503_KakhnouskiEC_Lab8\Files\CarInfo.json";

            List<Car> Cars = new List<Car>();
            for (int i = 0; i < 1000; i++)
            {
                Cars.Add(new Car(i, Lorem.Words(1,true, true), Lorem.Chance(5,10))); 
            }

            StreamService<Car> st = new StreamService<Car>();

            MemoryStream mem = new MemoryStream();

            var Task1 = st.WriteToStreamAsync(mem, Cars);

            var Task2 = st.CopyFromStreamAsync(mem, FileName);

            Task.WaitAll(Task1, Task2);

            var count = await st.GetStatisticsAsync(FileName, x => x.EngineValueThanTwoLitrs is true);

            Console.WriteLine($"Кол-во машин, объем двигателя которых превашает 2 литра равен : {count}");
        }
    }
}