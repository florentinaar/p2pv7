﻿using p2pv7.DTOs;
using p2pv7.Models;

namespace p2pv7.Services.RolesService
{
    public interface IRolesService
    {
        List<Role> GetRoles();
        bool AddRole(RoleDto request);
    }
}
