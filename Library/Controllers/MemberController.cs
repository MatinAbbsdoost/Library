using Library.DAL;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    public class MemberController : Controller
    {
        private readonly LibraryDbContext _db;
        public MemberController(LibraryDbContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? search)
        {
            ViewData["search"] = search;

            if (!string.IsNullOrWhiteSpace(search))
            {
                var Search = await _db.Members.Where(x => x.Name.Contains(search)
                || x.LastName.Contains(search) || x.PhoneNumber.Contains(search))
                .ToListAsync();
                return View(Search);
            }



            return View(await _db.Members.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _db.Members
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View("register", new Member());
        }

        private void LoadContacts()
        {
            //ViewData["Contacts"] = _db.Contacts.Select(s => new SelectListItem
            //{
            //    Value = s.Id.ToString(),
            //    Text = s.Name
            //}).ToList();
            var members = _db.Members.ToList();
            ViewBag.contacts = new SelectList(members, "id", "name", "lastname", "phonenumber");

        }
        [HttpPost]
        public IActionResult Create(Member model)
        {
            if (ModelState.IsValid)
            {
                _db.Members.Add(model);
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

            LoadContacts();

            var member = _db.Members.Find(id);
            return View("register", member);
        }

        [HttpPost]
        //Edit
        public IActionResult Edit(Member Model)
        {
            ModelState.Remove("member");
            if (ModelState.IsValid)
            {
                _db.Members.Update(Model);
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
            LoadContacts();
            var member = _db.Members.Find(id);
            return View(member);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult SubmitDelete(Member model)
        {
            _db.Members.Remove(model);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


    }
}
