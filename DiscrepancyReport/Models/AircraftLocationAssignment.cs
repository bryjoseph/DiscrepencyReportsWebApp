using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscrepancyReport.Models
{
    //// was the aircraft visit planned or unplanned
    //public enum VisitType
    //{
    //    Planned, Unplanned
    //}


    public class AircraftLocationAssignment
    {
        // ID == PK
        public int ID { get; set; }
        public int LocationID { get; set; }
        public int AircraftID { get; set; }
        public bool? Planned { get; set; }
        public bool? Unplanned { get; set; }

        // Navigation properties
        public Aircraft Aircraft { get; set; }
        public Location Location { get; set; }
    }
}
