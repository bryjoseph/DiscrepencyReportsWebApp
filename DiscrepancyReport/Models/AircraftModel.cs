using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscrepancyReport.Models
{
    public class AircraftModel
    {
        public int ID { get; set; }
        public string ModelType { get; set; }
        public string Manufacturer { get; set; }
        // public int AircraftID { get; set; }

        // Navigation properties
        // public Aircraft Aircraft { get; set; }
        public ICollection<Aircraft> Aircrafts { get; set; }
    }
}
