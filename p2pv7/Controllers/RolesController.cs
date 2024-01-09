using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using p2pv7.DTOs;
using p2pv7.Models;
using p2pv7.Services.RolesService;

namespace p2pv7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;
        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet]
        public ActionResult<List<Role>> Get()
            => Ok(_rolesService.GetRoles());

        [HttpPost]
        public ActionResult<List<Role>> AddRole(RoleDto request)
        {
            _rolesService.AddRole(request);
            return Ok(_rolesService.GetRoles());
        }
    }
}
