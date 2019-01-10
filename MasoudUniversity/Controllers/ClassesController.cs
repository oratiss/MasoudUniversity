using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MasoudUniversity.Data;
using MasoudUniversity.Models;
using MasoudUniversity.Models.ViewModels;

namespace MasoudUniversity.Controllers
{
    public class ClassesController : Controller
    {
        private readonly SchoolContext _context;

        public ClassesController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Classes
        public async Task<IActionResult> Index(int? id, int? StudentID)
        {
            var viewModel = new SchoolIndexData();
            viewModel.Classes = await _context.Classes
                  .Include(c => c.Enrollments)
                  .ThenInclude(i => i.Student)
                  .AsNoTracking()
                  .OrderBy(i => i.Title)
                  .ToListAsync();

            if (id != null)
            {
                ViewData["ClassID"] = id.Value;
                Class sampleClass = viewModel.Classes.Where(
                    c => c.Id == id.Value).Single();
                viewModel.Students = sampleClass.Enrollments.Select(s => s.Student);
            }

            if (StudentID != null)
            {
                ViewData["StudentID"] = StudentID.Value;
                viewModel.Enrollments = viewModel.Students.Where(
                    x => x.Id == StudentID).Single().Enrollments;
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Index2(int? id, int? StudentID)
        {
            var viewModel = new SchoolIndexData();
            viewModel.Classes = await _context.Classes
                  .Include(c => c.Enrollments)
                  .ThenInclude(i => i.Student)
                  .AsNoTracking()
                  .OrderBy(i => i.Title)
                  .ToListAsync();

            if (id != null)
            {
                ViewData["ClassID"] = id.Value;
                Class sampleClass = viewModel.Classes.Where(
                    c => c.Id == id.Value).Single();
                viewModel.Students = sampleClass.Enrollments.Select(s => s.Student);
            }

            if (StudentID != null)
            {
                ViewData["StudentID"] = StudentID.Value;
                var selectedClass = viewModel.Students.Where(x => x.Id == StudentID).Single();
                await _context.Entry(selectedClass).Collection(x => x.Enrollments).LoadAsync();
                foreach (Enrollment enrollment in selectedClass.Enrollments)
                {
                    await _context.Entry(enrollment).Reference(x => x.Student).LoadAsync();
                }
                viewModel.Enrollments = selectedClass.Enrollments;
            }

            return View(viewModel);
        }


        // GET: Classes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Location,TeacherName")] Class @class)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@class);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@class);
        }

        // GET: Classes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Classes.SingleOrDefaultAsync(m => m.Id == id);
            if (@class == null)
            {
                return NotFound();
            }
            return View(@class);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Location,TeacherName")] Class @class)
        {
            if (id != @class.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@class);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassExists(@class.Id))
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
            return View(@class);
        }

        // GET: Classes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Classes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @class = await _context.Classes.SingleOrDefaultAsync(m => m.Id == id);
            _context.Classes.Remove(@class);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassExists(int id)
        {
            return _context.Classes.Any(e => e.Id == id);
        }
    }
}
