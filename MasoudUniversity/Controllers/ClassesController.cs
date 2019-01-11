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
            var @class = new Class();
            @class.Enrollments = new List<Enrollment>();
            PopulateAssignedStudentData(@class);
            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Location,TeacherName")] Class @class, string[] selectedStudents)
        {
            if (selectedStudents != null)
            {
                @class.Enrollments = new List<Enrollment>();
                foreach (var student in selectedStudents)
                {
                    var StudentToAdd = new Enrollment { ClassID = @class.Id, StudentID = int.Parse(student) };
                    @class.Enrollments.Add(StudentToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(@class);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateAssignedStudentData(@class);
            return View(@class);
        }

        // GET: Classes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var @class = await _context.Classes
           .Include(c => c.Enrollments)
           .ThenInclude(c => c.Student)
           .AsNoTracking()
           .SingleOrDefaultAsync(m => m.Id == id);

            if (@class == null)
            {
                return NotFound();
            }
            PopulateAssignedStudentData(@class);
            return View(@class);
        }

        private void PopulateAssignedStudentData(Class @class)
        {
            var allStudents = _context.Students;
            var classStudents = new HashSet<int>(@class.Enrollments.Select(c => c.StudentID));
            var viewModel = new List<AssignedStudentData>();
            foreach (var Student in allStudents)
            {
                viewModel.Add(new AssignedStudentData
                {
                    SrudentID = Student.Id,
                    StudentFullName = Student.StudentFullName,
                    Assigned = classStudents.Contains(Student.Id)
                });
            }
            ViewData["Students"] = viewModel;
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedStudents)
        {

            if (id == null)
            {
                return NotFound();
            }

            var ClassToUpdate = await _context.Classes
                .Include(i => i.Enrollments)
                    .ThenInclude(i => i.Student)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (await TryUpdateModelAsync<Class>(
                ClassToUpdate,
                "",
                i => i.Title, i => i.Location, i => i.TeacherName, i => i.Enrollments))
            {

                UpdateClassStudents(selectedStudents, ClassToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateClassStudents(selectedStudents, ClassToUpdate);
            PopulateAssignedStudentData(ClassToUpdate);
            return View(ClassToUpdate);
        }

        private void UpdateClassStudents(string[] selectedStudents, Class ClassToUpdate)
        {
            if (selectedStudents == null)
            {
                ClassToUpdate.Enrollments = new List<Enrollment>();
                return;
            }

            var selectedStudentsHS = new HashSet<string>(selectedStudents);
            var ClassStudents = new HashSet<int>
                (ClassToUpdate.Enrollments.Select(e => e.Student.Id));
            foreach (var student in _context.Students)
            {
                if (selectedStudentsHS.Contains(student.Id.ToString()))
                {
                    if (!ClassStudents.Contains(student.Id))
                    {
                        ClassToUpdate.Enrollments.Add(new Enrollment { ClassID = ClassToUpdate.Id, StudentID = student.Id });
                    }
                }
                else
                {

                    if (ClassStudents.Contains(student.Id))
                    {
                        Enrollment studentToRemove = ClassToUpdate.Enrollments.SingleOrDefault(e => e.StudentID == student.Id);
                        _context.Remove(studentToRemove);
                    }
                }
            }
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
            Class @class = await _context.Classes
            .Include(c => c.Enrollments)
            .SingleAsync(c =>c.Id == id);

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
