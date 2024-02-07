using Library.DAL;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    public class AuthorController : Controller
    {
        private readonly LibraryDbContext _db;
        public AuthorController(LibraryDbContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? search)
        {
            ViewData["search"] = search;

            if (!string.IsNullOrWhiteSpace(search))
            {
                var Search = await _db.Authors.Where(x => x.Name.Contains(search)
                || x.LastName.Contains(search))
                .ToListAsync();
                return View(Search);
            }



            return View(await _db.Authors.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _db.Authors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View("register", new Author());
        }

        private void LoadAuthors()
        {
            //ViewData["Contacts"] = _db.Contacts.Select(s => new SelectListItem
            //{
            //    Value = s.Id.ToString(),
            //    Text = s.Name
            //}).ToList();
            var author = _db.Authors.ToList();
            ViewBag.author = new SelectList(author, "id", "name", "lastname");

        }
        [HttpPost]
        public IActionResult Create(Author model)
        {
            if (ModelState.IsValid)
            {
                _db.Authors.Add(model);
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

            LoadAuthors();

            var author = _db.Authors.Find(id);
            return View("register", author);
        }

        [HttpPost]
        //Edit
        public IActionResult Edit(Author model)
        {
            ModelState.Remove("author");
            if (ModelState.IsValid)
            {
                _db.Authors.Update(model);
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
            LoadAuthors();
            var author = _db.Authors.Find(id);
            return View(author);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult SubmitDelete(Author model)
        {
            _db.Authors.Remove(model);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
