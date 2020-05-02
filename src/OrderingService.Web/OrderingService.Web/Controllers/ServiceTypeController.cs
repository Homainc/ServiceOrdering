using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderingService.Domain.Logic.Code.Interfaces;
using OrderingService.Web.Code;

namespace OrderingService.Web.Controllers
{
    public class ServiceTypeController : AbstractApiController
    {
        private readonly IServiceTypeService _serviceTypeService;

        public ServiceTypeController(IServiceTypeService serviceTypeService) =>
            _serviceTypeService = serviceTypeService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllOrderedByProfilesCount() =>
            Ok(await _serviceTypeService.GetAllOrderedByProfilesCount());
    }
}