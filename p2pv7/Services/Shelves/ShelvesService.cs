using AutoMapper;
using p2pv7.Data;

namespace p2pv7.Services.Shelves
{
    public class ShelvesService :IShelvesService
    {
        private readonly DataContext _context;

        private readonly IMapper _mapper;
        public ShelvesService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
    }
}
