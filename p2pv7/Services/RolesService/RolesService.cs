using Microsoft.EntityFrameworkCore;
using p2pv7.Data;
using p2pv7.DTOs;
using p2pv7.Models;

namespace p2pv7.Services.RolesService
{
    public class RolesService: IRolesService
    {
        private readonly DataContext _context;
        public RolesService(DataContext context)
        {
            _context = context;
        }
        public List<Role> GetRoles()
        {
            return _context.Roles
                 .Include(w => w.Users)
                .ToList();
        }
        public bool AddRole(RoleDto request)
        {
            var role = new Role()
            {
                RoleName = request.RoleName,
                Description = request.Description,
            };
            if (role == null)
            {
                return false;
            }
            else if (RoleExists(role))
            {
                return false;
            }
            else
            {
                _context.Roles.Add(role);
                _context.SaveChanges();
                return true;
            }
        }
        public bool UpdateRole(Role request)
        {
            var role = _context.Roles.Find(request.RoleId);
            if (role == null)
            {
                return false;
            }
            else if (!RoleExists(role))
            {
                return false;
            }
            else
            {
                role.RoleName = request.RoleName;
                role.Description = request.Description;

                _context.SaveChanges();
                return true;
            }
        }
        public bool RoleExists(Role request)
        {
            bool alreadyExist = _context.Roles.Any(x => x.RoleId == request.RoleId || x.RoleName == request.RoleName || x.Description == request.Description);
            return alreadyExist;
        }
    }
}
