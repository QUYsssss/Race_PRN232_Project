using System;
using System.Collections.Generic;

namespace Race_PRN232_Project.Models
{
    public partial class Location
    {
        public Location()
        {
            Races = new HashSet<Race>();
        }

        public int LocationId { get; set; }
        public string LocationName { get; set; } = null!;
        public string? Address { get; set; }

        public virtual ICollection<Race> Races { get; set; }
    }
}
