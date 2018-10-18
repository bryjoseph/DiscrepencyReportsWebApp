using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscrepancyReport.Models
{
    public class Aircraft
    {
        // ID == PK
        public int ID { get; set; }
        // the string property is already nullable if not a US aircraft
        public string FaaNumber { get; set; }
        // the string property is already nullable if not a EU aircraft
        public string EasaNumber { get; set; }
        public string TailNumber { get; set; }
        public int AircraftModelID { get; set; }

        // navigation properties
        public ICollection<AircraftLocationAssignment> AircraftLocationAssignments { get; set; }
        // public ICollection<AircraftModel> AircraftModels { get; set; }
        public AircraftModel AircraftModel { get; set; }
    }
}
