using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HKT2tr5.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public List<Xe> Xes { get; set; }
    }
}
