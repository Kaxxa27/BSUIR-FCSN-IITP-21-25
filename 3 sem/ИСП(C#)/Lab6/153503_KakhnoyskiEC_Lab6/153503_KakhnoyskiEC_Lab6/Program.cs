using System.Collections.Generic;
using System.Reflection;

namespace _153503_KakhnoyskiEC_Lab6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string JSON = @"C:\BSUIR\3 sem\ИСП(C#)\Lab6\153503_KakhnoyskiEC_Lab6\153503_KakhnoyskiEC_Lab6\Files\FILE_JSON.json";

            List<Employee> employees = new List<Employee>(); ;

            employees.Add(new Employee("Jeka", 19, true));
            employees.Add(new Employee("Den", 100));
            employees.Add(new Employee("Arthur", 34, true));
            employees.Add(new Employee());


            Assembly asm = Assembly.LoadFrom(@"C:\BSUIR\3 sem\ИСП(C#)\Lab6\153503_KakhnoyskiEC_Lab6\FileServiceLibrary\obj\Debug\net6.0\FileServiceLibrary.dll");

            // Type? FileSer = asm.GetTypes()[3];

            Type? FileSer = asm?
                .GetType("FileServiceLibrary.FileServiceFromLib`1")?
                .MakeGenericType(typeof(Employee)) ?? null;

            if (FileSer is not null)
            {
                
                MethodInfo? SaveData = FileSer.GetMethod("SaveData");
                MethodInfo? ReadFile = FileSer.GetMethod("ReadFile");

                object? FileService = Activator.CreateInstance(FileSer);

                SaveData?.Invoke(FileService, new object[] { employees, JSON });             

                List<Employee> emp = ReadFile?.Invoke(FileService, new object[] { JSON }) as List<Employee> ?? new List<Employee>();

                foreach (var item in emp)
                {
                    Console.WriteLine($" Name: {item.Name}\n Age: {item.Age}\n isWorking: {item.isWorking}\n");
                }
            }
        } 
    }
}
