using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClassLibraryForLab8
{
    public class StreamService<T>
    {
        #region WriteToStreamAsync
        public async Task WriteToStreamAsync(Stream stream, IEnumerable<T> data)
        {
            Console.WriteLine($"Запись в поток {Thread.CurrentThread.ManagedThreadId} началась...");

            var optionsJSON = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            //await Task.Run(() => {
                lock (this)
                {
                    //stream.Seek(0, SeekOrigin.Begin);
                    //StreamWriter writer = new(stream);

                   await JsonSerializer.SerializeAsync(stream, data);

                    //writer.Write(JsonSerializer.Serialize(data, optionsJSON));
                    //writer.Flush();
                }
           // });

            Console.WriteLine($"Запись в поток {Thread.CurrentThread.ManagedThreadId} завершилась.");
        }
        #endregion

        #region CopyFromStreamAsync
        public async Task CopyFromStreamAsync(Stream stream, string fileName)
        {
            Console.WriteLine($"Чтение из потока {Thread.CurrentThread.ManagedThreadId} началось...");

            await Task.Run(() =>
            {
                lock (this)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    using StreamReader reader = new(stream);
                    using StreamWriter writer = new(fileName);
                    while (!reader.EndOfStream)
                    {
                        writer.WriteLine(reader.ReadLine());
                    }
                }
            });
            Console.WriteLine($"Чтение из потока {Thread.CurrentThread.ManagedThreadId} завершилось.");
        }
        #endregion

        #region GetStatisticsAsync
        public async Task<int> GetStatisticsAsync(string fileName, Func<Car, bool> filter)
        {
            int count = 0;

            await Task.Run(() => { });

            using FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);

            var items = JsonSerializer.Deserialize<List<Car>>(fs);

            foreach (var current in items)
            {
                if (filter(current))
                {
                    count++;
                }
            }
            return count;
        }
        #endregion
    }
}
