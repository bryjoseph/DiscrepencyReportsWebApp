using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscrepancyReport.Models;

namespace DiscrepancyReport.Data
{
    public class DbInitializer
    {
        public static void Initialize(MaintenanceContext context)
        {
            context.Database.EnsureCreated();

            //Look for any aircraft values to see if the DB successfully seeded
            if (context.Aircrafts.Any())
            {
                return; // returning means DB successfully seeded
            }

            var aircraftModels = new AircraftModel[]
            {
                //new AircraftModel{ModelType="A-29", Manufacturer="Embraer", AircraftID=1},
                //new AircraftModel{ModelType="C-146", Manufacturer="Fairchild Dornier", AircraftID=2},
                //new AircraftModel{ModelType="C-146", Manufacturer="Fairchild Dornier", AircraftID=3},
                //new AircraftModel{ModelType="A-29", Manufacturer="Embraer", AircraftID=4},
                //new AircraftModel{ModelType="A-29", Manufacturer="Embraer", AircraftID=5}
                new AircraftModel{ModelType="A-29", Manufacturer="Embraer"},
                new AircraftModel{ModelType="C-146", Manufacturer="Fairchild Dornier"}
            };
            foreach (AircraftModel am in aircraftModels)
            {
                context.AircraftModels.Add(am);
            }
            context.SaveChanges();

            var aircrafts = new Aircraft[]
            {
                 new Aircraft{FaaNumber="0011001",EasaNumber=null,TailNumber="1234567", AircraftModelID=1},
                 new Aircraft{FaaNumber="0011002",EasaNumber=null,TailNumber="1234568", AircraftModelID=1},
                 new Aircraft{FaaNumber="0011003",EasaNumber=null,TailNumber="1234569", AircraftModelID=1},
                 new Aircraft{FaaNumber="0011004",EasaNumber=null,TailNumber="1234560", AircraftModelID=2},
                 new Aircraft{FaaNumber="0011005",EasaNumber=null,TailNumber="1234561", AircraftModelID=2}
                //new Aircraft{FaaNumber="0011001",EasaNumber=null,TailNumber="1234567"},
                //new Aircraft{FaaNumber="0011002",EasaNumber=null,TailNumber="1234568"},
                //new Aircraft{FaaNumber="0011003",EasaNumber=null,TailNumber="1234569"},
                //new Aircraft{FaaNumber="0011004",EasaNumber=null,TailNumber="1234560"},
                //new Aircraft{FaaNumber="0011005",EasaNumber=null,TailNumber="1234561"}
            };
            foreach (Aircraft a in aircrafts)
            {
                context.Aircrafts.Add(a);
            }
            context.SaveChanges();

            var locations = new Location[]
            {
                new Location{RegionCode="DENV", RegionName="West", LocationCode="WWRM"},
                new Location{RegionCode="CALI", RegionName="Pacific", LocationCode="WWPO"},
                new Location{RegionCode="BOST", RegionName="East", LocationCode="ECCA"},
                new Location{RegionCode="FLAA", RegionName="South East", LocationCode="SESC"},
                new Location{RegionCode="TENN", RegionName="Central", LocationCode="CCAM"}
            };
            foreach (Location l in locations)
            {
                context.Locations.Add(l);
            }
            context.SaveChanges();

            var locationAssignments = new AircraftLocationAssignment[]
            {
                new AircraftLocationAssignment{LocationID=1, AircraftID=1, Planned=true, Unplanned=false},
                new AircraftLocationAssignment{LocationID=1, AircraftID=2, Planned=true, Unplanned=false},
                new AircraftLocationAssignment{LocationID=2, AircraftID=3, Planned=true, Unplanned=false},
                new AircraftLocationAssignment{LocationID=3, AircraftID=4, Planned=true, Unplanned=false},
                new AircraftLocationAssignment{LocationID=4, AircraftID=5, Planned=false, Unplanned=true}
            };
            foreach (AircraftLocationAssignment ala in locationAssignments)
            {
                context.AircraftLocationAssignments.Add(ala);
            }
            context.SaveChanges();

            var titles = new Title[]
            {
                //new Title{EmployeeID=1, TitleName="Technician"},
                //new Title{EmployeeID=2, TitleName="Quality Assure"},
                //new Title{EmployeeID=3, TitleName="Technician"},
                //new Title{EmployeeID=4, TitleName="Government Official"},
                //new Title{EmployeeID=5, TitleName="Supervisor"}
                new Title{TitleName="Technician"},
                new Title{TitleName="Quality Assure"},
                new Title{TitleName="Technician"},
                new Title{TitleName="Government Official"},
                new Title{TitleName="Supervisor"}
            };

            var employees = new Employee[]
            {
                new Employee{FirstName="Bob", LastName="Haskins", HireDate=DateTime.Parse("2018-10-01"), TitleID=1},
                new Employee{FirstName="Claire", LastName="Little", HireDate=DateTime.Parse("2010-06-05"), TitleID=2},
                new Employee{FirstName="Tom", LastName="Law", HireDate=DateTime.Parse("2015-12-11"), TitleID=1},
                new Employee{FirstName="Jason", LastName="Day", HireDate=DateTime.Parse("2011-03-25"), TitleID=4},
                new Employee{FirstName="Kelly", LastName="Wynn", HireDate=DateTime.Parse("2009-05-16"), TitleID=5}
            };
            foreach (Employee e in employees)
            {
                context.Employees.Add(e);
            }
            context.SaveChanges();
        }
    }
}
