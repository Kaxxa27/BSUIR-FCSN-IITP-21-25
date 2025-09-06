namespace TaxiApplication.DAL;

public class TaxiApplicationDbContext : DbContext
{
	public DbSet<User> Users { get; set; }
	public DbSet<Client> Clients { get; set; }
	public DbSet<ClientProfile> clientProfiles { get; set; }
	public TaxiApplicationDbContext(DbContextOptions<TaxiApplicationDbContext> dbContextOptions) : base(dbContextOptions)
	{
		//Database.EnsureDeleted();
		Database.EnsureCreated();
	}



	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		//modelBuilder.Entity<User>()
		//.HasIndex(u => u.Login)
		//.IsUnique();

		User Admin = new User
		{
			Id = 1,	
			Login = "Kaxxa",
			Password = "12345",
			Role = Role.Admin
		};



		modelBuilder.Entity<User>().HasData(Admin);
		modelBuilder.Entity<TaxiOrder>()
		   .HasIndex(p => p.ClientId)
		   .IsUnique(false);
	}

	//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	//{
	//    optionsBuilder.UseSqlite("Data Source=TaxiApplicationDataBase.db");
	//}
}
