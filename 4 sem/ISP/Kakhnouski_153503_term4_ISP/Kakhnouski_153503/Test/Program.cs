using Kakhnouski_153503.Domain.Entities;
using Kakhnouski_153503.Persistence.Data;
using Kakhnouski_153503.Persistence.Repository;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (AppDbContext db = new AppDbContext()) 
            {
                FakeUnitOfWork fakeUnitOfWork = new FakeUnitOfWork(db);
                //  fakeUnitOfWork.CreateDatabaseAsync().Wait();
                fakeUnitOfWork.AuthorRepository.AddAsync(new Author() { Id = 1, Name = "Jeka", Surname = "Kaxxa", Age = 39 });
                fakeUnitOfWork.AuthorRepository.AddAsync(new Author() { Id = 2, Name = "Denis", Surname = "KDS", Age = 187 });

                fakeUnitOfWork.SaveAllAsync();
                var test = fakeUnitOfWork.AuthorRepository.ListAllAsync().Result;
                foreach (var author in test)
                {
                    Console.WriteLine($"Имя: {author.Name}\n" +
                                 $"Фамилия: {author.Surname}\n" +
                                 $"Id: {author.Id}");
                }
            }
           // FakeAuthorRepository fakeAuthorRepository = new FakeAuthorRepository();

           // fakeAuthorRepository.AddAsync(new Author() { Id = 5, Name = "Jeka", Surname = "Kaxxa", Age = 39 });
           // var test = fakeAuthorRepository.ListAsync((f) => f.Age > 40).Result;
           //// var test = fakeAuthorRepository.ListAllAsync().Result;

           // foreach (var author in test) 
           // {
           //     Console.WriteLine($"Имя: {author.Name}\n" +
           //                       $"Фамилия: {author.Surname}\n" + 
           //                       $"Id: {author.Id}");
           // }
        }
    }
}

