using System;
using System.Linq;
using WindPowerPlatformAPI.Domain.Entities;
using WindPowerPlatformAPI.Infrastructure.Data.Repositories.Interfaces;

namespace WindPowerPlatformAPI.Infrastructure.Data.Repositories
{
    class TurbineInfoFileRepository : ITurbineInfoFileRepository
    {
        private readonly ApplicationDbContext _context;

        public TurbineInfoFileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateTurbineInfoFile(TurbineInfoFile infoFile)
        {
            if (infoFile == null) { throw new ArgumentNullException(nameof(infoFile)); }

            _context.TurbineInfoFiles.Add(infoFile);
        }

        public TurbineInfoFile GetInformationFile(int turbineId)
        {
            return _context.TurbineInfoFiles.FirstOrDefault(f => f.TurbineId == turbineId);
        }

        public TurbineInfoFile GetInformationFileByName(string name)
        {
            return _context.TurbineInfoFiles.FirstOrDefault(f => f.Description == name);
;        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
