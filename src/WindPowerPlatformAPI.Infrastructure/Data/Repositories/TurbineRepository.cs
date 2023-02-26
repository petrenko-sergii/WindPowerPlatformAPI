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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void UpdateTurbine(Turbine turbine)
        {
            throw new NotImplementedException();
        }
    }
}
