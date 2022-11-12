using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace MobileOperator
{
    public class Context : DbContext
    {
        private static Context context;

        public Context()
            : base("name=MobileOperator2022Entities")
        {
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


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Abonent> Abonents { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Number> Numbers { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
