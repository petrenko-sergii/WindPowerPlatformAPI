namespace WindPowerPlatformAPI.Infrastructure.Dtos
{
    public class TurbineInfoFileDownloadDto : TurbineInfoFileReadDto
    {
        public byte[] Bytes { get; set; }
    }
}
