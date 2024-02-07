using Library.DAL;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Formats.Tar;

namespace Library.Controllers
{
    public class BookWriterController : Controller
    {
        private readonly LibraryDbContext _db;
        public BookWriterController(LibraryDbContext context)
        {
            _db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _db.BookWriters.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var bookWriters = await _db.BookWriters.FirstOrDefaultAsync(m => m.Id == id);
            if (bookWriters == null)
            {
                return NotFound();
            }
            return View(bookWriters);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        private void LoadWriters()
        {
            var boowriters = _db.BookWriters.ToList();
            ViewBag.bookwriters = new SelectList(boowriters, "Id", "Name");

        }
        [HttpPost]
        public IActionResult Create(BookWriter model)
        {
            if (ModelState.IsValid)
            {
                _db.BookWriters.Add(model);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View("Register",model);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                NotFound();
            }
            LoadWriters();
            var writers = _db.BookWriters.Find(id);
            return View("Register",writers);

        }
        [HttpPost]
        public IActionResult Edit(BookWriter model) 
        {
            ModelState.Remove("BookWriters");
            if (!ModelState.IsValid) 
            {
                _db.BookWriters.Update(model);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View("Register");

        }
        [HttpGet]
        public IActionResult Delete(int? id) 
        {
            if (id != null)
            {
                NotFound();
            }
            LoadWriters();
            var writers = _db.BookWriters.Find(id);
            return View(writers);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult SubmitDelete(BookWriter model)
        {
            _db.BookWriters.Remove(model);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        }
}
