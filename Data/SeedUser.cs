using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApiBakery.Data
{    public static class SeedUser
    {
        public async static Task Seed(UserManager<AppUser> userMgr, RoleManager<IdentityRole> roleMgr)
        {
            var employee = await userMgr.FindByNameAsync("employee1");
            // add employee
            if (employee == null)
            {
                if (!(await roleMgr.RoleExistsAsync("employee")))
                {
                    var role = new IdentityRole("employee");
                    await roleMgr.CreateAsync(role);
                    //role.Claims.Add(new IdentityRoleClaim<string>() { ClaimType = "IsAdmin", ClaimValue = "True" });
                    //await _roleMgr.CreateAsync(role);
                }

                employee = new AppUser()
                {
                    FirstName = "firstname",
                    LastName = "lastname",
                    UserName = "employee1"     
                };

                var userResult = await userMgr.CreateAsync(employee, "pw1234");
                var roleResult = await userMgr.AddToRoleAsync(employee, "employee");
                //var claimResult = await _userMgr.AddClaimAsync(user, new Claim("SuperUser", "True"));

                if (!userResult.Succeeded || !roleResult.Succeeded /*|| !claimResult.Succeeded*/)
                {
                    throw new InvalidOperationException("Failed to build user and roles");
                }
            }

            var user1 = await userMgr.FindByNameAsync("user1");
            var user2 = await userMgr.FindByNameAsync("user2");
            //add user
            if (user1 == null & user2 == null)
            {
                //if (!(await _roleMgr.RoleExistsAsync("User")))
                //{
                //    var role = new IdentityRole("User");
                //    await _roleMgr.CreateAsync(role);
                //    //role.Claims.Add(new IdentityRoleClaim<string>() { ClaimType = "IsAdmin", ClaimValue = "True" });
                //    //await _roleMgr.CreateAsync(role);
                //}

                user1 = new AppUser()
                {
                    FirstName = "firstname",
                    LastName = "lastname",
                    UserName = "user1",
                    Nip = "1234567891",
                    PhoneNumber = "111222333",
                    CompanyName = "ONEONE",
                    Address = "Kraków, Rakowicka 27",
                    PostalCode = "31-510 Kraków"


                };

                user2 = new AppUser()
                {
                    FirstName = "firstname",
                    LastName = "lastname",
                    UserName = "user2",
                    Nip = "1111111111",
                    PhoneNumber = "777888999",
                    CompanyName = "TWOTWO",
                    Address = "Kraków, Rakowicka 27B",
                    PostalCode = "31-510 Kraków"
                };

                var userResult1 = await userMgr.CreateAsync(user1, "pw1234");
                var userResult2 = await userMgr.CreateAsync(user2, "pw1234");
                //var roleResult = await _userMgr.AddToRoleAsync(user, "User");
                //var claimResult = await _userMgr.AddClaimAsync(user, new Claim("SuperUser", "True"));

                if (!userResult1.Succeeded || !userResult2.Succeeded /*|| !roleResult.Succeeded *//*|| !claimResult.Succeeded*/)
                {
                    throw new InvalidOperationException("Failed to build user and roles");
                }
            }
        }
    }
}
