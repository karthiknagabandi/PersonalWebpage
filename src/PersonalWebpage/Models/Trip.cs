﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebpage.Models
{
    public class Trip
    {
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public int Id { get; set; }
        public string UserName { get; set; }

        public ICollection<Stops> Stops { get; set; }

    }
}
