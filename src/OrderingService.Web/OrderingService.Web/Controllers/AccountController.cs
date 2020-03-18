using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderingService.Domain;
using OrderingService.Domain.Logic.Interfaces;

namespace OrderingService.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private IUserService UserService { get; }
        public AccountController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpPost]
        public async Task<IOperationResult> Create(UserDTO userDto) => 
            await UserService.CreateAsync(userDto);
    }
}
