using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WindPowerPlatformAPI.Domain.Entities
{
	public class TurbineInfoFile
    {
		[Key]
		[Required]
		public int Id { get; set; }

        [Required]
        public byte[] Bytes { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string FileExtension { get; set; }

        [Required]
        public decimal Size { get; set; }

        [Required]
        public DateTime CreatedDt { get; set; }

        [Required]
        public int TurbineId { get; set; }

        [ForeignKey("TurbineId")]

        public Turbine Turbine { get; set; }
    }
}
