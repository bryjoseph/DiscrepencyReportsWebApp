using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscrepancyReport.Models.ViewModels
{
    public class AircraftIndexData
    {
        public IEnumerable<Aircraft> Aircrafts { get; set; }
        public IEnumerable<AircraftModel> AircraftModels { get; set; }
    }
}
