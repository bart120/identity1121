using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Data
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string Lastname { get; set; }
        [Required]
        [StringLength(50)]
        public string Firstname { get; set; }
        [StringLength(50)]
        public string Service { get; set; }
    }
}
