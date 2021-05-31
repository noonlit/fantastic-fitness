﻿using System;
using System.Collections.Generic;

namespace Ffitness.ViewModels
{
    public class UserViewModel
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public DateTime? BirthDate { get; set; }

        public IList<string> Roles { get; set; }
    }
}
