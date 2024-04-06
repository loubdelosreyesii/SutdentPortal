using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SutdentPortal.Web.Data;
using SutdentPortal.Web.Models;
using SutdentPortal.Web.Models.Entities;

namespace SutdentPortal.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public StudentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel) 
        {
            var student = new Student {
                Name=viewModel.Name,
                Email=viewModel.Email,
                Phone=viewModel.Phone,
                Subscribed=viewModel.Subscribed
            };

            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        { 
            var students = await dbContext.Students.ToListAsync();

            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        { 
            var student = await dbContext.Students.FindAsync(id);

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student studentToUpdate)
        { 
            var student = await dbContext.Students.FindAsync(studentToUpdate.Id);

            if (student is not null)
            { 
                student.Name = studentToUpdate.Name;
                student.Email = studentToUpdate.Email;
                student.Phone = studentToUpdate.Phone;
                student.Subscribed = studentToUpdate.Subscribed;

                await dbContext.SaveChangesAsync();
            }
            
            return RedirectToAction("List", "Students");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var student = await dbContext.Students.FindAsync(id);

            if (student is not null)
            {
                dbContext.Students.Remove(student);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Students");
        }
    }
}
