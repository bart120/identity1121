
using IdentityModel;
using IdentityServer.Data;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Services
{
    public class ProfilService : IProfileService
    {
        private readonly UserManager<User> _userManager;
        public ProfilService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var tokenProfileClaims = new List<Claim>();
            User user = await _userManager.GetUserAsync(context.Subject);
            if (user == null)
                throw new ArgumentException("Invalid subject identifier");

            var userClaims = await _userManager.GetClaimsAsync(user);
            foreach (var item in userClaims)
            {
                tokenProfileClaims.Add(new Claim(item.Type, item.Value));
            }

            context.IssuedClaims.AddRange(tokenProfileClaims);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.CompletedTask;
        }
    }
}
