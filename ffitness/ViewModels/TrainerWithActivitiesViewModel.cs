using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ffitness.ViewModels
{
    public class TrainerWithActivitiesViewModel
    {
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Description { get; set; }
        public List<int> Activities { get; set; }
    }
}
