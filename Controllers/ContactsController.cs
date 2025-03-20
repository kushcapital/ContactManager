using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactManager.Data;
using ContactManager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContactManager.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ContactManagerContext _context;

        public ContactsController(ContactManagerContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var contacts = _context.Contacts.Include(c => c.Category);
            return View(await contacts.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id, string slug)
        {
            if (id == null) return NotFound();
            var contact = await _context.Contacts
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contact == null || (slug != null && slug != contact.Slug)) return NotFound();
            return View(contact);
        }

        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Phone,Email,Organization,CategoryId")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.DateAdded = DateTime.Now;
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", contact.CategoryId);
            return View(contact);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null) return NotFound();
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", contact.CategoryId);
            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactId,FirstName,LastName,Phone,Email,Organization,CategoryId,DateAdded")] Contact contact)
        {
            if (id != contact.ContactId) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.ContactId)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Details), new { id = contact.ContactId, slug = contact.Slug });
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", contact.CategoryId);
            return View(contact);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var contact = await _context.Contacts
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contact == null) return NotFound();
            return View(contact);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.ContactId == id);
        }
    }
}
