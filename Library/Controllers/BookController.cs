using Library.DAL;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly LibraryDbContext _db;
        public BookController(LibraryDbContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? search)
        {
            ViewData["search"] = search;

            if (!string.IsNullOrWhiteSpace(search))
            {
                var Search = await _db.Books.Where(x => x.Title.Contains(search)).ToListAsync();
                return View(Search);
            }



            return View(await _db.Books.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _db.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View("register", new Book());
        }

        private void LoadBooks()
        {
            //ViewData["Contacts"] = _db.Contacts.Select(s => new SelectListItem
            //{
            //    Value = s.Id.ToString(),
            //    Text = s.Name
            //}).ToList();
            var book = _db.Books.ToList();
            ViewBag.book = new SelectList(book, "id", "Title");

        }
        [HttpPost]
        public IActionResult Create(Book model)
        {
            if (ModelState.IsValid)
            {
                _db.Books.Add(model);
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

            LoadBooks();

            var book = _db.Books.Find(id);
            return View("register", book);
        }

        [HttpPost]
        //Edit
        public IActionResult Edit(Book model)
        {
            ModelState.Remove("Books");
            if (ModelState.IsValid)
            {
                _db.Books.Update(model);
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
            LoadBooks();
            var book = _db.Books.Find(id);
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult SubmitDelete(Book model)
        {
            _db.Books.Remove(model);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
