using System.ComponentModel.DataAnnotations;
using WindPowerPlatformAPI.Infrastructure.Attributes;

namespace WindPowerPlatformAPI.Infrastructure.Dtos
{
    public class TurbineCreateDto
    {
        [Required]
        [MaxLength(250)]
        public string SerialNumber { get; set; }

        [Required]
        [RequiredZeroOrGreater]
        public decimal Price { get; set; }
    }
}
