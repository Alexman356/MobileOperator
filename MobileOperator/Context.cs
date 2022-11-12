using Microsoft.EntityFrameworkCore;
using MobileOperator;

namespace MobileOperator;

public class Context: DbContext
{
    private static Context context;
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=ALEX-PC\SQLSERVER;Database=MobileOperator2022;Trusted_Connection=True;Encrypt=no");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Abonent>().HasKey(u => u.abonent_ID);
        modelBuilder.Entity<Contract>().HasKey(u => u.contract_ID);
        modelBuilder.Entity<Employee>().HasKey(u => u.employee_ID);
        modelBuilder.Entity<Number>().HasKey(u => u.Number_telephone);
        modelBuilder.Entity<Person>().HasKey(u => u.Id);
        modelBuilder.Entity<Rate>().HasKey(u => u.rate_ID);
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<Abonent>().HasOne(u => u.Person);
        modelBuilder.Entity<Abonent>().HasOne(u => u.User);
    }

    public static Context Get()
    {
        if (context == null)
        {
            context = new Context();
        }
              
        return context;
    }

    public static void Set(Context newContext)
    {
        context = newContext;
    }
    
    public virtual DbSet<Abonent> Abonents { get; set; }
    
    public virtual DbSet<Contract> Contracts { get; set; }
    
    public virtual DbSet<Employee> Employees { get; set; }
    
    public virtual DbSet<Number> Numbers { get; set; }
    
    public virtual DbSet<Person> Persons { get; set; }
    
    public virtual DbSet<Rate> Rates { get; set; }
    
    public virtual DbSet<User> Users { get; set; }
}