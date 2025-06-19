using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using InsuranceQuoteCalculator.Models;

namespace InsuranceQuoteCalculator.Controllers
{
    public class InsuranceController : Controller
    {
        private InsuranceDbContext db = new InsuranceDbContext();

        // GET: Insurance
        public ActionResult Index()
        {
            return View(db.Insurees.ToList());
        }

        // GET: Insurance/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Insurance/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,SpeedingTickets,DUI,FullCoverage")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                insuree.Quote = CalculateQuote(insuree);
                db.Insurees.Add(insuree);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(insuree);
        }

        private decimal CalculateQuote(Insuree insuree)
        {
            decimal monthlyTotal = 50; // Base quote

            // Age-based calculations
            var age = DateTime.Now.Year - insuree.DateOfBirth.Year;
            if (DateTime.Now.DayOfYear < insuree.DateOfBirth.DayOfYear) age--;

            if (age <= 18)
                monthlyTotal += 100;
            else if (age <= 25)
                monthlyTotal += 50;
            else
                monthlyTotal += 25;

            // Car year calculations
            if (insuree.CarYear < 2000)
                monthlyTotal += 25;
            if (insuree.CarYear > 2015)
                monthlyTotal += 25;

            // Car make and model calculations
            if (insuree.CarMake.ToLower() == "porsche")
            {
                monthlyTotal += 25;
                if (insuree.CarModel.ToLower() == "911 carrera")
                    monthlyTotal += 25;
            }

            // Speeding tickets
            monthlyTotal += insuree.SpeedingTickets * 10;

            // DUI calculation
            if (insuree.DUI)
                monthlyTotal *= 1.25m;

            // Coverage type calculation
            if (insuree.FullCoverage)
                monthlyTotal *= 1.50m;

            return monthlyTotal;
        }

        // GET: Insurance/Admin
        public ActionResult Admin()
        {
            var quotes = db.Insurees.Select(i => new
            {
                i.FirstName,
                i.LastName,
                i.EmailAddress,
                i.Quote
            }).ToList();

            return View(quotes);
        }
    }
}