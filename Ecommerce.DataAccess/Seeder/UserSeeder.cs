﻿using Ecommerce.Entities.Models.Auth.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.DataAccess.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<User> _userManager)
        {
            var usersCount = await _userManager.Users.CountAsync();
            if (usersCount <= 0)
            {
                var adminUser = new User()
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    PhoneNumber = "01224309198",
                    EmailConfirmed = true,
                };
                await _userManager.CreateAsync(adminUser, "P@ssw0rd123Pass");
                await _userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
