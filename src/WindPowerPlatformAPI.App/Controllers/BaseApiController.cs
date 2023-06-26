using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WindPowerPlatformAPI.App.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected IMapper _mapper;
        protected ILogger _logger;

        protected BaseApiController(IMapper mapper, ILogger logger)
        {
            _mapper = mapper;
            _logger = logger;
        }
    }
}
