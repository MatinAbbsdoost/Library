using Library.DAL;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices.JavaScript;

namespace Library.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly LibraryDbContext _db;
        public SubjectsController(LibraryDbContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? search)
        {
            ViewData["search"] = search;

            if (!string.IsNullOrWhiteSpace(search))
            {
                var Search = await _db.Subjects.Where(x => x.Name.Contains(search)).ToListAsync();
                return View(Search);
            }



            return View(await _db.Subjects.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _db.Subjects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View("register", new Subject());
        }

        private void LoadSubjects()
        {
            //ViewData["Contacts"] = _db.Contacts.Select(s => new SelectListItem
            //{
            //    Value = s.Id.ToString(),
            //    Text = s.Name
            //}).ToList();
            var subject = _db.Subjects.ToList();
            ViewBag.subject = new SelectList(subject, "id", "name");

        }
        [HttpPost]
        public IActionResult Create(Subject model)
        {
            if (ModelState.IsValid)
            {
                _db.Subjects.Add(model);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View("register", model);
        }

        [HttpGet]
        //Edit
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                NotFound();
            }

            LoadSubjects();

            var subject = _db.Subjects.Find(id);
            return View("register", subject);
        }

        [HttpPost]
        //Edit
        public IActionResult Edit(Subject model)
        {
            ModelState.Remove("Subjects");
            if (ModelState.IsValid)
            {
                _db.Subjects.Update(model);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View("register");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                NotFound();
            }
            LoadSubjects();
            var subject = _db.Subjects.Find(id);
            return View(subject);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult SubmitDelete(Subject model)
        {
            _db.Subjects.Remove(model);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
