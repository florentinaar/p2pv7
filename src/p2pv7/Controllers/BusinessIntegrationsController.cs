using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using p2pv7.DTOs;
using p2pv7.Models;
using p2pv7.Services;

namespace p2pv7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessIntegrationsController : ControllerBase
    {
        private readonly IBusinessIntegrationService _businessIntegration;

        public BusinessIntegrationsController(IBusinessIntegrationService businessIntegrationService)
        {
            _businessIntegration = businessIntegrationService;
        }

        [HttpPost("SaveBusiness")]
        public bool SaveBusiness(BusinessDto request)
           => _businessIntegration.SaveBusiness(request);

        [HttpGet("GetAllBusinesses")]
        public List<Business> getAllBusinesses()
            => _businessIntegration.GetAllBusinesses();

        [HttpGet("GetBussinesByToken")]
        public Business getBussinesByToken(string token)
            => _businessIntegration.GetBussinesByToken(token);
    }
}
