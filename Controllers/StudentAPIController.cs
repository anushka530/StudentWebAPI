using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIApplication.Models;

namespace WebAPIApplication.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly MyDbContext context;

        public StudentAPIController(MyDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            var data = await context.Students.ToListAsync();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentsById(int id)
        {
            var data = await context.Students.FirstOrDefaultAsync(s => s.Id == id);
            if(data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
    
    [HttpPost]
    public async Task<ActionResult<Student>> CreatedStudent(Student student)
    {
        await context.Students.AddAsync(student);
        await context.SaveChangesAsync();
        return Ok(student);

    }
        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> UpdateStudent(int id, Student student)
        {
            if(id != student.Id)
            {
                return BadRequest();
            }
            context.Entry(student).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(student);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var std = await context.Students.FindAsync(id);
            if (std == null)
            {
                return NotFound();
            }
            context.Students.Remove(std);
            await context.SaveChangesAsync();
            return Ok();
        }
}
}
