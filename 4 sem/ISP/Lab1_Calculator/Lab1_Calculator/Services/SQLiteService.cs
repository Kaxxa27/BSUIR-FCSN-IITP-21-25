
using SQLite;

namespace Lab1_Calculator.Services;

public class SQLiteService : IDbService
{
    SQLiteAsyncConnection _dataBase;
    public SQLiteService()
    {

    }

    async public Task<IEnumerable<Brigade>> GetAllBrigades() 
    {
        await Init();
        return await _dataBase.Table<Brigade>().ToListAsync();
    }
      

    async public Task<IEnumerable<Job>> GetBrigadeJob(int id)
    {
        await Init();
        return await _dataBase.Table<Job>().Where(t => t.BrigadeId == id).ToListAsync();
    }
   
 

    async public Task Init()
    {
        if (_dataBase is not null)
            return;

        _dataBase = new SQLiteAsyncConnection(Constants.DataBaseConstants.DatabasePath);

        await _dataBase.DropTableAsync<Brigade>();
        await _dataBase.DropTableAsync<Job>();

        await _dataBase.CreateTableAsync<Brigade>();
        await _dataBase.CreateTableAsync<Job>();

        await _dataBase.InsertAllAsync(new List<Brigade>() 
        { 
          new Brigade() { Specialization = "Electrics", Staff = 2, Title = "Jeka&Denis"},
          new Brigade() { Specialization = "Installers", Staff = 10, Title = "We are Invalids"},
          new Brigade() { Specialization = "Physicists", Staff = 7, Title = "Dorochevich"},
        });

        await _dataBase.InsertAllAsync(new List<Job>()
        {
          new Job() { Title = "Подключение устройств", Price = 2000, ExecutionTimeDay = 3, BrigadeId = 1},
          new Job() { Title = "Починка проводки", Price = 6666, ExecutionTimeDay = 7,  BrigadeId = 1},
          new Job() { Title = "Отладка системы энергоснабжения", Price = 10000, ExecutionTimeDay = 20, BrigadeId = 1},
          new Job() { Title = "Полная проврка устройсв", Price = 500, ExecutionTimeDay = 1, BrigadeId = 1},

          new Job() { Title = "Подключение устройств", Price = 2000, ExecutionTimeDay = 3, BrigadeId = 1},
          new Job() { Title = "Починка проводки", Price = 6666, ExecutionTimeDay = 7,  BrigadeId = 1},
          new Job() { Title = "Отладка системы энергоснабжения", Price = 10000, ExecutionTimeDay = 20, BrigadeId = 1},
          new Job() { Title = "Полная проврка устройсв", Price = 500, ExecutionTimeDay = 1, BrigadeId = 1},

          new Job() { Title = "Подключение устройств", Price = 2000, ExecutionTimeDay = 3, BrigadeId = 1},
          new Job() { Title = "Починка проводки", Price = 6666, ExecutionTimeDay = 7,  BrigadeId = 1},
          new Job() { Title = "Отладка системы энергоснабжения", Price = 10000, ExecutionTimeDay = 20, BrigadeId = 1},
          new Job() { Title = "Полная проврка устройсв", Price = 500, ExecutionTimeDay = 1, BrigadeId = 1},

          new Job() { Title = "Подключение устройств", Price = 2000, ExecutionTimeDay = 3, BrigadeId = 1},
          new Job() { Title = "Починка проводки", Price = 6666, ExecutionTimeDay = 7,  BrigadeId = 1},
          new Job() { Title = "Отладка системы энергоснабжения", Price = 10000, ExecutionTimeDay = 20, BrigadeId = 1},
          new Job() { Title = "Полная проврка устройсв", Price = 500, ExecutionTimeDay = 1, BrigadeId = 1},
        });

        await _dataBase.InsertAllAsync(new List<Job>()
        {
          new Job() { Title = "Сборка узловых конструкцй", Price = 5000, ExecutionTimeDay = 5, BrigadeId = 2},
          new Job() { Title = "Консультация по строительству", Price = 777, ExecutionTimeDay = 1,  BrigadeId = 2},
          new Job() { Title = "Полный объем работ", Price = 30000, ExecutionTimeDay = 31, BrigadeId = 2},
        }); 
        
        await _dataBase.InsertAllAsync(new List<Job>()
        {
          new Job() { Title = "Теоретическая консультация", Price = 1250, ExecutionTimeDay = 1, BrigadeId = 3},
          new Job() { Title = "Практическая консультация", Price = 2500, ExecutionTimeDay = 3,  BrigadeId = 3},
          new Job() { Title = "Выполнение вычислений и подсчетов", Price = 3000, ExecutionTimeDay = 7, BrigadeId = 3},
        });

    }
}
