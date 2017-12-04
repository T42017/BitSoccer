using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BitSoccerWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Identity.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BitSoccerWeb.Extensions
{
    public static class UserManagerExtensions
    {
        //public static async Task<IdentityResult> SetDisplayNameAsync(ApplicationUser user, string email)
        //{
        //    return await UpdateUserAsync(user);
        //}

        public static string GetDisplayName(UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            return userManager?.Users?.First(u => u.DisplayName == user.DisplayName).DisplayName;
        }
    }
}
