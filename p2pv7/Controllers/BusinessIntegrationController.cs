using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using p2pv7.DTOs;
using p2pv7.Models;
using p2pv7.Services.BusinessIntegration;

namespace p2pv7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessIntegrationController : ControllerBase
    {
        private readonly IBusinessIntegrationService _businessIntegration;

        public BusinessIntegrationController(IBusinessIntegrationService businessIntegrationService)
        {
            _businessIntegration = businessIntegrationService;
        }

        [HttpPost("SaveBusiness")]
        public async Task<ActionResult<string>> SaveBusiness(BusinessDto request)
        {
            _businessIntegration.SaveBusiness(request);
            return Ok();
        }
        [HttpGet("GetAllBusinesses")]
        public List<Business> getAllBusinesses()
        {
            //_businessIntegration.getAllBusinesses();
            return _businessIntegration.getAllBusinesses();
        }
        [HttpGet("GetBussinesByToken")]
        public Business getBussinesByToken(string token)
        {
            return _businessIntegration.getBussinesByToken( token);
        }
    }
}
