using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Management_System_App.Data;
using Management_System_App.Models;
using Microsoft.AspNetCore.Authorization;

namespace Management_System_App.Controllers
{
    public class studentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public studentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: students
        public async Task<IActionResult> Index()
        {
              return _context.student != null ? 
                          View(await _context.student.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.student'  is null.");
        }

        // GET: students/search-students
        public async Task<IActionResult> SearchStudents()
        {
            return View();
        }
        
        // POST: students/search-results
        public async Task<IActionResult> ShowSearchResults(string searchKeyWord)
        {
            return View("Index", await _context.student.Where(
                result => result.firstName.Contains(searchKeyWord) || result.lastName.Contains(searchKeyWord)
                ).ToListAsync());
        }

        

        // GET: students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.student == null)
            {
                return NotFound();
            }

            var student = await _context.student
                .FirstOrDefaultAsync(m => m.id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: students/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,firstName,lastName,age,currentYear,isGraduated")] student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: students/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.student == null)
            {
                return NotFound();
            }

            var student = await _context.student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,firstName,lastName,age,currentYear,isGraduated")] student student)
        {
            if (id != student.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!studentExists(student.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: students/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.student == null)
            {
                return NotFound();
            }

            var student = await _context.student
                .FirstOrDefaultAsync(m => m.id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: students/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.student == null)
            {
                return Problem("Entity set 'ApplicationDbContext.student'  is null.");
            }
            var student = await _context.student.FindAsync(id);
            if (student != null)
            {
                _context.student.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool studentExists(int id)
        {
          return (_context.student?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
