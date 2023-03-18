using System;
using System.Collections.Generic;
using System.Linq;
using WindPowerPlatformAPI.Domain.Entities;
using WindPowerPlatformAPI.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WindPowerPlatformAPI.Infrastructure.Data.Repositories
{
    class CommandRepository : ICommandRepository
    {
        private readonly ApplicationDbContext _context;

        public CommandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            _context.Commands.Add(cmd);
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return _context.Commands.ToList();
        }

        public Command GetCommandById(int id)
        {
            return _context.Commands.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void UpdateCommand(Command cmd)
        {
            var local = _context.Set<Command>().Local.FirstOrDefault(entry => entry.Id.Equals(cmd.Id));

            // check if local is not null 
            if (local != null)
            {
                // detach
                _context.Entry(local).State = EntityState.Detached;
            }
            // set Modified flag in your entry
            _context.Entry(cmd).State = EntityState.Modified;
        }

        public void DeleteCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            var local = _context.Set<Command>().Local.FirstOrDefault(entry => entry.Id.Equals(cmd.Id));

            // check if local is not null 
            if (local != null)
            {
                // detach
                _context.Entry(local).State = EntityState.Detached;
            }
            // set Modified flag in your entry
            _context.Entry(cmd).State = EntityState.Deleted;
        }
    }
}
