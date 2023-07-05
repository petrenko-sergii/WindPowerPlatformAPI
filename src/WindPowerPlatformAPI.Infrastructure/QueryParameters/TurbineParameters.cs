namespace WindPowerPlatformAPI.Infrastructure.QueryParameters
{
    public class TurbineParameters : QueryStringParameters
    {
        public TurbineParameters() {
            OrderBy = "price";
        }
    }
}
