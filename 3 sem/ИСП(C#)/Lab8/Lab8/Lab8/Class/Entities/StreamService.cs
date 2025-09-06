using Class.Interface;
using System.Text;
using System.Text.Json;

namespace Class.Entities {
    public class StreamService : IStreamService<Passanger> {
        private readonly object locker = new();

        public async Task CopyFromStreamAsync(Stream stream, string fileName, IProgress<string> progress) {
            
            progress.Report($"Копирование началось в потоке {Environment.CurrentManagedThreadId}");
            await Task.Run(() => {
                lock (locker) {
                    stream.Seek(0, SeekOrigin.Begin);
                    using StreamReader reader = new(stream);
                    using StreamWriter writer = new(fileName);
                    while (!reader.EndOfStream) {
                        writer.WriteLine(reader.ReadLine());
                    }
                }
            });
            progress.Report("Копирование окончено");
        }

        public async Task<int> GetStatisticsAsync(string fileName, Func<Passanger, bool> filter) {
            using var reader = new StreamReader(fileName);
            var items = JsonSerializer.Deserialize<IEnumerable<Passanger>>(await reader.ReadToEndAsync())!;
            int count = 0;
            foreach (var current in items) {
                if (filter(current)) {
                    count++;
                }
            }
            return count;
        }

        public async Task WriteToStreamAsync(Stream stream, IEnumerable<Passanger> data, IProgress<string> progress) {
            progress.Report($"Начало записи в потоке {Environment.CurrentManagedThreadId}");
            await Task.Run(() => {
                lock (locker) {
                    stream.Seek(0, SeekOrigin.Begin);
                    StreamWriter writer = new(stream);
                    writer.Write(JsonSerializer.Serialize(data));
                    writer.Flush();
                }
            });
            progress.Report("Запись окончена");
        }
    }
}
