using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscrepancyReport.Models
{
    public class Location
    {
        // ID == PK
        public int ID { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string LocationCode { get; set; }

        //navigation property to AircraftLocationAssignment
        public ICollection<AircraftLocationAssignment> AircraftLocationAssignments { get; set; }
    }
}
