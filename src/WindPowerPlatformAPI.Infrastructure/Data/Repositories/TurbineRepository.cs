using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WindPowerPlatformAPI.Domain.Entities;
using WindPowerPlatformAPI.Infrastructure.Data.Repositories.Interfaces;
using System.Linq.Dynamic.Core;
using WindPowerPlatformAPI.Infrastructure.Helpers.Interfaces;

namespace WindPowerPlatformAPI.Infrastructure.Data.Repositories
{
    class TurbineRepository : ITurbineRepository
    {
        private readonly ApplicationDbContext _context;
        private ISortHelper<Turbine> _sortHelper;

        public TurbineRepository(ApplicationDbContext context, ISortHelper<Turbine> sortHelper)
        {
            _context = context;
            _sortHelper = sortHelper;
        }

        public void CreateTurbine(Turbine turbine)
        {
            if (turbine == null)
            {
                throw new ArgumentNullException(nameof(turbine));
            }

            _context.Turbines.Add(turbine);
        }

        public IEnumerable<Turbine> GetAllTurbines(string sortBy)
        {
            var turbines = _context.Turbines;

            return string.IsNullOrEmpty(sortBy)  ? turbines : _sortHelper.ApplySort(turbines, sortBy);
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

        public void DeleteTurbine(Turbine cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            var local = _context.Set<Turbine>().Local.FirstOrDefault(entry => entry.Id.Equals(cmd.Id));

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
