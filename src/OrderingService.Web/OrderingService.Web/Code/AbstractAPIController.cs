using Microsoft.AspNetCore.Mvc;

namespace OrderingService.Web.Code
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class AbstractApiController : ControllerBase
    { }
}
