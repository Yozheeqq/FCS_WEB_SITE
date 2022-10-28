using Microsoft.AspNetCore.Identity;
using FCS_WebSite_v2.Data.Models;
using System;

namespace FCS_WebSite_v2.Data
{
    public static class ContextSeed
    { 
        public static async Task SeedRolesAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Teacher.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Pupil.ToString()));
        }
    }
}
