using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scanton.Models;

namespace Scanton.Controllers
{
    public class StoreProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StoreProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StoreProfile storeProfile)
        {
            if (ModelState.IsValid)
            {
                _context.StoreProfiles.Add(storeProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            return View(storeProfile);
        }

        public IActionResult List()
        {
            var stores = _context.StoreProfiles.ToList();
            return View(stores);
        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var store = await _context.StoreProfiles.FindAsync(id);
            if (store == null) return NotFound();

            return View(store);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StoreProfile store)
        {
            if (id != store.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(store);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Blogs.Any(b => b.Id == id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(List));
            }
            return View(store);
        }
    }
}
