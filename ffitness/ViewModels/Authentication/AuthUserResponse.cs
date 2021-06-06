using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ffitness.ViewModels
{
    public class AuthUserResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
    }
}
