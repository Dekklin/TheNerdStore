using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheNerdStore.Models.Handlers
{
    public class IsMemberReq : IAuthorizationRequirement
    {
        public string Email { get; set; }
        public IsMemberReq(string email)
        {
            Email = email;
        }
    }
}
