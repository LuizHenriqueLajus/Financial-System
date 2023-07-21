using Finance.Entities.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infra.Configuration;

public class ContextBase : IdentityDbContext<ApplicationUser>
{
    public ContextBase(DbContextOptions options) : base(options)
    {      
    }

    public DbSet<FinancialSystem> FinancialSystem { get; set; }
    public DbSet<UserFinancialSystem> UserFinancialSystem { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Expense> Expense { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(ObtainConnectionString());
            base.OnConfiguring(optionsBuilder);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);
        
        base.OnModelCreating(builder);  
    }

    public string ObtainConnectionString() 
    {
        return "Data Source=localhost,1433;Initial Catalog=FINANCE_2023;Integrated Security=False;User ID=sa;Password=1q2w3e4r@#$;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
    } 
}
