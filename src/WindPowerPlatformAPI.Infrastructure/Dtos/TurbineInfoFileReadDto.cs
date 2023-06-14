namespace WindPowerPlatformAPI.Infrastructure.Dtos
{
    public class TurbineInfoFileReadDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string FileExtension { get; set; }
        public int TurbineId { get; set; }
    }
}
