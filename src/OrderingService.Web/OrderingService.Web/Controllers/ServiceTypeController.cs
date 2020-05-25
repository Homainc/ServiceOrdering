using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderingService.Data.Code.Constants;
using OrderingService.Domain;
using OrderingService.Domain.Logic.Code.Interfaces;
using OrderingService.Web.Code.Abstractions;

namespace OrderingService.Web.Controllers
{
    [Authorize(Roles = RoleDefaults.Admin)]
    public class ServiceTypeController : AbstractApiController
    {
        private readonly IServiceTypeService _serviceTypeService;

        public ServiceTypeController(IServiceTypeService serviceTypeService) =>
            _serviceTypeService = serviceTypeService;

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ServiceTypeDto>))]
        public async Task<IActionResult> GetAllOrderedByProfilesCount() =>
            Ok(await _serviceTypeService.GetAllOrderedByProfilesCount());

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceTypeDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> CreateAsync(ServiceTypeCreateDto item)
            => Ok(await _serviceTypeService.CreateAsync(item));

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceTypeDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> UpdateAsync(ServiceTypeDto item)
            => Ok(await _serviceTypeService.UpdateAsync(item));

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceTypeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> DeleteAsync(int id)
            => Ok(await _serviceTypeService.DeleteAsync(id));

    }
}