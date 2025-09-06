using Class.Entities;

class Program {
    static async Task Main(string[] args) {
        List<Passanger> passangers = new();
        Random random = new();
        var progress = new Progress<string>();

        progress.ProgressChanged += (obj, str) => Console.WriteLine(str);

        Console.WriteLine($"Поток {Environment.CurrentManagedThreadId} стартовал");
        for (int i = 0; i < 1000; i++) {
            int id = random.Next(1000000);
            string name = "";
            for (int j = 0; j < random.Next(1, 16); j++) {
                name += (char)random.Next(65, 90);
            }
            bool baggage = random.Next(2) == 1;
            passangers.Add(new(id, name, baggage));
        }

        StreamService streamService = new();
        MemoryStream memoryStream = new();

        var m1 = streamService.WriteToStreamAsync(memoryStream, passangers, progress);
        Thread.Sleep(200);
        var m2 = streamService.CopyFromStreamAsync(memoryStream, "passangers.json", progress);

        Task.WaitAll(m1, m2);

        var count = await streamService.GetStatisticsAsync("passangers.json", x => x.WithBaggage);
        Console.WriteLine($"Количество пассажиров с багажом: {count}");
    }
}
