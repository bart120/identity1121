using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    public class ManageController : Controller
    {
        private readonly ConfigurationDbContext _conf;
        public ManageController(ConfigurationDbContext conf)
        {
            _conf = conf;
        }
        public async Task<IActionResult> AddResource()
        {
            ApiResource res = new ApiResource();
            res.Name = "api_meteo";
            res.Description = "une api en formation identity server";
            res.DisplayName = "API Météo";
            res.Enabled = true;
            _conf.ApiResources.Add(res.ToEntity());
            await _conf.SaveChangesAsync();

            return Ok();
        }

        public async Task<IActionResult> AddClient()
        {
            Client client = new Client
            {
                ClientId = "client_console",
                ClientSecrets =
                {
                    new Secret("secret_client_console".Sha256())
                },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                RedirectUris = { "https://oauth.pstmn.io/v1/callback" },
                RequirePkce = false,
                AllowedScopes =
                {
//                    IdentityServerConstants.StandardScopes.

                }
            };

            _conf.Clients.Add(client.ToEntity());
            await _conf.SaveChangesAsync();
            return Ok();
        }
    }
}
