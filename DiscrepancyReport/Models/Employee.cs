using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscrepancyReport.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime HireDate { get; set; }
        public int TitleID { get; set; }

        // navigation property
        // public ICollection<Title> Titles { get; set; }
        public Title Title { get; set; }
    }
}
