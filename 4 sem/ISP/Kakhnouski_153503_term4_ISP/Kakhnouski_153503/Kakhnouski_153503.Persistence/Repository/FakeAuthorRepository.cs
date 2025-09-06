using SQLitePCL;

namespace Kakhnouski_153503.Persistence.Repository;

public class FakeAuthorRepository : IRepository<Author>
{
    List<Author> _authors;

    public FakeAuthorRepository()
    {
        _authors = new List<Author>()
        {
            new Author()
            {
               Id = 1,
               Name = "Михаил",
               Surname = "Булгаков",
               Age = 60,
               Email = string.Empty,
               GenresOfBooks = "Фантастика",
               Books = new List<Book>()
               {
                   new Book()
                   {
                       AuthorID = 1,
                       Title = "Мастер и Маргарита",
                       Price = 1000,
                       Id = 1,
                       Rating = 5,
                       DateOfWriting = new DateTime(2000,12,12),
                       Description = string.Empty
                   }
               }
            },
            new Author()
            {
                Id = 2,
                Name = "Стивен",
                Surname = "Кинг",
                Age = 60,
                Email = string.Empty,
                GenresOfBooks = "Ужасы",
                Books = new List<Book>()
                {
                   new Book()
                   {
                       AuthorID = 2,
                       Title = "Зеленая миля",
                       Price = 5000,
                       Id = 1,
                       Rating = 4.34,
                       DateOfWriting = new DateTime(1995,12,12),
                       Description = string.Empty
                   }
                }
            },
            new Author()
            {
                Id = 3,
                Name = "Лев",
                Surname = "Толстой",
                Age = 100,
                Email = string.Empty,
                GenresOfBooks = "Военная тематика",
                Books = new List<Book>()
                {
                   new Book()
                   {
                       AuthorID = 3,
                       Title = "Война и мир",
                       Price = 9999,
                       Id = 1,
                       Rating = 2.2,
                       DateOfWriting = new DateTime(1995,12,12),
                       Description = string.Empty
                   }
                }
            },
            new Author()
            {
                Id = 4,
                Name = "Джоан",
                Surname = "Роулинг",
                Age = 32,
                Email = string.Empty,
                GenresOfBooks = "Фантастика",
                Books = new List<Book>()
                {
                   new Book()
                   {
                       AuthorID = 4,
                       Title = "Гарри потер и философский камень",
                       Price = 2750,
                       Id = 1,
                       Rating = 9.99,
                       DateOfWriting = new DateTime(1995,12,12),
                       Description = string.Empty
                   }
                }
            },


        };
    }

    public async Task AddAsync(Author entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(() => _authors.Add(entity));
    }

    public async Task DeleteAsync(Author entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(() => _authors.Remove(entity));
    }

    public async Task<Author> FirstOrDefaultAsync(Func<Author, bool>? filter = null, CancellationToken cancellationToken = default)
    {

        if (filter is null)
            return await Task.Run(() => _authors.FirstOrDefault());
        return await Task.Run(() => _authors.Where(filter).FirstOrDefault());
    }

    public async Task<Author> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => _authors.ToList().Find(x => x.Id == id));
    }

    public async Task<IReadOnlyList<Author>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => _authors);
    }

    public async Task<IReadOnlyList<Author>> ListAsync(Func<Author, bool> filter, CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => _authors.Where(filter).ToList());
    }

    public Task UpdateAsync(Author entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
