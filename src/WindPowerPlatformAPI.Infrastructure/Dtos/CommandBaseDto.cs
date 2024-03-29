﻿using System.ComponentModel.DataAnnotations;

namespace WindPowerPlatformAPI.Infrastructure.Dtos
{
    public class CommandBaseDto
    {
        [Required]
        [MaxLength(250)]
        public string HowTo { get; set; }

        [Required]
        public string Platform { get; set; }

        [Required]
        public string CommandLine { get; set; }
    }
}
