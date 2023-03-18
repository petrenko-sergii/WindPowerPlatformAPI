using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WindPowerPlatformAPI.Domain.Entities;
using WindPowerPlatformAPI.Infrastructure.Data.Repositories.Interfaces;

namespace WindPowerPlatformAPI.Infrastructure.Data.Repositories
{
    class TurbineRepository : ITurbineRepository
    {
        private readonly ApplicationDbContext _context;

        public TurbineRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateTurbine(Turbine turbine)
        {
            if (turbine == null)
            {
                throw new ArgumentNullException(nameof(turbine));
            }

            _context.Turbines.Add(turbine);
        }

        public void DeleteTurbine(Turbine turbine)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Turbine> GetAllTurbines()
        {
            return _context.Turbines.ToList();
        }

        public Turbine GetTurbineById(int id)
        {
            return _context.Turbines.FirstOrDefault(t => t.Id == id);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void UpdateTurbine(Turbine turbine)
        {
            var local = _context.Set<Turbine>().Local.FirstOrDefault(entry => entry.Id.Equals(turbine.Id));

            // check if local is not null 
            if (local != null)
            {
                // detach
                _context.Entry(local).State = EntityState.Detached;
            }
            // set Modified flag in your entry
            _context.Entry(turbine).State = EntityState.Modified;
        }
    }
}
