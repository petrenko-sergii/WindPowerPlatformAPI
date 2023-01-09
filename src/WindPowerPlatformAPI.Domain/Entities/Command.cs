using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WindPowerPlatformAPI.Domain.Entities
{
	public class Command
	{
		[Key]
		[Required]
		public int Id { get; set; }

		[Required]
		[MaxLength(250)]
		public string HowTo { get; set; }

		[Required]
		public string Platform { get; set; }

		[Required]
		public string CommandLine { get; set; }
	}
}
