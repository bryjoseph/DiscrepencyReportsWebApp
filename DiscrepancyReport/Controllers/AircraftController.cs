using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DiscrepancyReport.Data;
using DiscrepancyReport.Models;
using DiscrepancyReport.Models.ViewModels;

namespace DiscrepancyReport.Controllers
{
    public class AircraftController : Controller
    {
        private readonly MaintenanceContext _context;

        public AircraftController(MaintenanceContext context)
        {
            _context = context;
        }

        // GET: Aircraft
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? page)
        {
            // Pagination (NOT USED)
            ViewData["CurrentSort"] = sortOrder;
            // sorting for FFA Number
            ViewData["FaaNumberSortParm"] = String.IsNullOrEmpty(sortOrder) ? "faa_number_desc" : "";
            // sorting for TailNumber Number
            ViewData["TailNumberSortParm"] = String.IsNullOrEmpty(sortOrder) ? "tail_number_desc" : "";

            // (NOT USED)
            if (searchString != null)
            {
                // without a searchString set the page to 1
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            // Filtering by searchString
            ViewData["CurrentSearchFilter"] = searchString;

            // var aircraftSort = from a in _context.Aircrafts select a;
            var aircrafts = _context.Aircrafts
                .Include(a => a.AircraftModel)
                .AsNoTracking();

            // searching on different variables
            if(!String.IsNullOrEmpty(searchString))
            {
                aircrafts = aircrafts.Where(a => a.FaaNumber.Contains(searchString)
                                            || a.TailNumber.Contains(searchString)
                                            || a.EasaNumber.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "faa_number_desc":
                    aircrafts = aircrafts.OrderByDescending(a => a.FaaNumber);
                    break;
                case "tail_number_desc":
                    aircrafts = aircrafts.OrderByDescending(a => a.TailNumber);
                    break;
                default:
                    aircrafts = aircrafts.OrderBy(a => a.FaaNumber);
                    break;
            }
            // set the number of results per page
            // int pageSize = 5;
            // return View(await PaginatedList<Aircraft>.CreateAsync(aircrafts.AsNoTracking(), page ?? 1, pageSize));
            return View(await aircrafts.ToListAsync());
        }

        // GET: Aircraft/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // this code is retrieving a single Aircraft Entry from the DB
            //var aircraft = await _context.Aircrafts
            //    .SingleOrDefaultAsync(m => m.ID == id);
            // this code is going to include the Aircraft_Location_Assignments collection
            var aircraft = await _context.Aircrafts
                // including the aircraft model table
                .Include(am => am.AircraftModel)
                // then including the Assignments, and locations
                .Include(ala => ala.AircraftLocationAssignments)
                    .ThenInclude(l => l.Location)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);

            if (aircraft == null)
            {
                return NotFound();
            }

            return View(aircraft);
        }

        // GET: Aircraft/Create
        public IActionResult Create()
        {
            PopulateAircraftModelsDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ID,FaaNumber,EasaNumber,TailNumber,AircraftModelID")] Aircraft aircraft)
        {
            if(ModelState.IsValid)
            {
                _context.Add(aircraft);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateAircraftModelsDropDownList(aircraft.AircraftModelID);
            return View(aircraft);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await _context.Aircrafts
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);

            if (aircraft == null)
            {
                return NotFound();
            }
            PopulateAircraftModelsDropDownList(aircraft.AircraftModelID);
            return View(aircraft);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraftToUpdate = await _context.Aircrafts.SingleOrDefaultAsync(a => a.ID == id);

            if (await TryUpdateModelAsync<Aircraft>(
                aircraftToUpdate,
                "",
                a => a.FaaNumber, a => a.EasaNumber, a => a.TailNumber, a => a.AircraftModelID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    // Log the error ( uncomment the ex to write a log)
                    ModelState.AddModelError("", "Unable to save changes " +
                                            "Try again, and if the problem persists " +
                                            "see your system administrator");
                }
                return RedirectToAction(nameof(Index));
            }
            PopulateAircraftModelsDropDownList(aircraftToUpdate.AircraftModelID);
            return View(aircraftToUpdate);
        }

        // GET: Aircraft/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChagesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await _context.Aircrafts
                .Include(a => a.AircraftModel)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);

            if (aircraft == null)
            {
                return NotFound();
            }

            if(saveChagesError.GetValueOrDefault())
            {
                // creating a custom error message the View for Aircraft can access
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator";
            }
            return View(aircraft);
        }

        // this is a read-first approach
        // POST: Aircraft/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aircraft = await _context.Aircrafts
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);

            // if the aircraft is null redirect back to the index page
            if(aircraft == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Aircrafts.Remove(aircraft);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /*ex*/)
            {
                // to log the error from deletetion uncomment the ex variable
                // set the saveChangesError to true because there is a new error
                // the true is passed to the above Delete method and now the custom error message shows up
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private void PopulateAircraftModelsDropDownList(object selectedModel = null)
        {
            var aircraftModelQuery = from m in _context.AircraftModels
                                     orderby m.ModelType
                                     select m;
            ViewBag.ID = new SelectList(aircraftModelQuery.AsNoTracking(), "ID", "ModelType", selectedModel);
        }

        private bool AircraftExists(int id)
        {
            return _context.Aircrafts.Any(e => e.ID == id);
        }
    }
}
