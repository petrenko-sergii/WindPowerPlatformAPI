using System.ComponentModel.DataAnnotations;

namespace WindPowerPlatformAPI.Domain.Entities
{
	public class Turbine
	{
		[Key]
		[Required]
		public int Id { get; set; }

		[Required]
		[MaxLength(250)]
		public string SerialNumber { get; set; }

		[Required]
		public decimal Price { get; set; }
	}
}
