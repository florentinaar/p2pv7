using p2pv7.DTOs;
using p2pv7.Models;

namespace p2pv7.Services
{
    public interface IBusinessIntegrationService
    {
        bool SaveBusiness(BusinessDto request);
        List<Business> getAllBusinesses();
        Business getBussinesByToken(string token);

    }
}
