using Microsoft.EntityFrameworkCore;
using WindPowerPlatformAPI.Domain.Entities;

namespace WindPowerPlatformAPI.Infrastructure.Data
{
    public class CommandContext : DbContext
    {
        public CommandContext(DbContextOptions<CommandContext> options)
        : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Command>().ToTable("Commands");
        //    modelBuilder.Entity<Command>().Property(i => i.Id).ValueGeneratedOnAdd();
        //}

        public DbSet<Command> Commands { get; set; }

        public DbSet<Turbine> Turbines { get; set; }
    }
}
