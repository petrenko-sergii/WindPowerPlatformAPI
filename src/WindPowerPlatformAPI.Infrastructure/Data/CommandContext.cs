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

        public DbSet<Command> CommandItems { get; set; }
    }
}
