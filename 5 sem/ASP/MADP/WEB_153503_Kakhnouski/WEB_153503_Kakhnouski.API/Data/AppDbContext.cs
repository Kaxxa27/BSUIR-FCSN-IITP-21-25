using Microsoft.EntityFrameworkCore;
using WEB_153503_Kakhnouski.Domain.Entities;

namespace WEB_153503_Kakhnouski.API.Data;

public class AppDbContext : DbContext
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<Category> Categories { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
}
