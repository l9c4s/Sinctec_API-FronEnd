using Entities.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InfrasStructure.Configuration
{
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {

        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {


        }
        public DbSet<Menssage> Message { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public string ObterStringConexao()
        {
            return "Server=DESKTOP-F96R3A2\\SQLEXPRESS;Database=Menssage;User Id=SistemaLogon;Password=123456";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ObterStringConexao());
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(T => T.Id);
            base.OnModelCreating(builder);

        }
    }
}
