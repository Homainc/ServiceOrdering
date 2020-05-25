using Microsoft.AspNetCore.Mvc;

namespace OrderingService.Web.Code.Abstractions
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class AbstractApiController : ControllerBase
    { }
}
