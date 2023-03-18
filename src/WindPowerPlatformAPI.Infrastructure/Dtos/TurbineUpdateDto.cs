using System.ComponentModel.DataAnnotations;

namespace WindPowerPlatformAPI.Infrastructure.Dtos
{
    public class TurbineUpdateDto
    {
        [Required]
        [MaxLength(250)]
        public string SerialNumber { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
