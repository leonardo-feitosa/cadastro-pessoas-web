using CadastroPessoasWeb.Data;
using CadastroPessoasWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadastroPessoasWeb.Controllers
{
    public sealed class PeopleController : Controller
    {
        private readonly AppDbContext _db;

        public PeopleController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var list = await _db.People.AsNoTracking()
                .OrderBy(p => p.Name)
                .ToListAsync();

            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            var person = await _db.People.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person is null) return NotFound();
            return View(person);
        }

        public IActionResult Create() => View(new Person());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Person model)
        {
            model.Name = (model.Name ?? "").Trim();
            model.Sex = (model.Sex ?? "").Trim();

            if (!ModelState.IsValid) return View(model);

            _db.People.Add(model);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var person = await _db.People.FindAsync(id);
            if (person is null) return NotFound();
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Person model)
        {
            if (id != model.Id) return BadRequest();

            model.Name = (model.Name ?? "").Trim();
            model.Sex = (model.Sex ?? "").Trim();

            if (!ModelState.IsValid) return View(model);

            var person = await _db.People.FirstOrDefaultAsync(p => p.Id == id);
            if (person is null) return NotFound();

            person.Name = model.Name;
            person.Age = model.Age;
            person.Sex = model.Sex;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var person = await _db.People.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person is null) return NotFound();
            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _db.People.FindAsync(id);
            if (person is null) return NotFound();

            _db.People.Remove(person);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}