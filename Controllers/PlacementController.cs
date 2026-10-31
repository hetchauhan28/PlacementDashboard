
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlacementDashboard.Models;

namespace PlacementDashboard.Controllers
{
    public class PlacementController : Controller
    {
        private readonly PlacementContext _context;

        public PlacementController(PlacementContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string search)
        {
            var students = await _context.Students.ToListAsync();

            ViewBag.Total = students.Count;
            ViewBag.Placed = students.Count(s => s.Status == "Placed");
            ViewBag.NotPlaced = students.Count(s => s.Status != "Placed");

            ViewBag.Percentage = students.Count == 0
                ? 0
                : students.Count(s => s.Status == "Placed") * 100.0
                    / students.Count;

            if (!string.IsNullOrWhiteSpace(search))
            {
                students = students.Where(s =>
                    s.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    s.Department.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    s.Company.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            ViewBag.Search = search;

            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                if (student.Status != "Placed")
                {
                    student.Company = "Not Assigned";
                    student.Package = 0;
                }

                _context.Students.Add(student);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(student);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            if (id != student.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                if (student.Status != "Placed")
                {
                    student.Company = "Not Assigned";
                    student.Package = 0;
                }

                _context.Students.Update(student);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
