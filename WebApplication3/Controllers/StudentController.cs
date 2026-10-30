using Microsoft.AspNetCore.Mvc;
using WebApplication3.DTOs;
using WebApplication3.Services;

namespace WebApplication3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    // GET: api/students
    [HttpGet]
    public async Task<ActionResult<List<StudentDto>>> GetAllStudents()
    {
        var students = await _studentService.GetAllStudentsAsync();
        return Ok(students);
    }

    // GET: api/students/5
    [HttpGet("{id}")]
    public async Task<ActionResult<StudentDto>> GetStudentById(int id)
    {
        if (id <= 0)
        {
            return BadRequest(new { message = "Student ID must be greater than zero." });
        }

        var student = await _studentService.GetStudentByIdAsync(id);

        if (student is null)
        {
            return NotFound(new { message = "Student not found." });
        }

        return Ok(student);
    }

    // POST: api/students
    [HttpPost]
    public async Task<ActionResult<StudentDto>> AddStudent(CreateStudentDto studentDto)
    {
        if (await _studentService.EmailExistsAsync(studentDto.Email))
        {
            return Conflict(new { message = "A student with this email already exists." });
        }

        var createdStudent = await _studentService.AddStudentAsync(studentDto);

        return CreatedAtAction(
            nameof(GetStudentById),
            new { id = createdStudent.Id },
            createdStudent);
    }

    // PUT: api/students/5
    [HttpPut("{id}")]
    public async Task<ActionResult<StudentDto>> UpdateStudent(int id, UpdateStudentDto studentDto)
    {
        if (id <= 0)
        {
            return BadRequest(new { message = "Student ID must be greater than zero." });
        }

        if (await _studentService.EmailExistsAsync(studentDto.Email, id))
        {
            return Conflict(new { message = "Another student with this email already exists." });
        }

        var updatedStudent = await _studentService.UpdateStudentAsync(id, studentDto);

        if (updatedStudent is null)
        {
            return NotFound(new { message = "Student not found." });
        }

        return Ok(updatedStudent);
    }

    // DELETE: api/students/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        if (id <= 0)
        {
            return BadRequest(new { message = "Student ID must be greater than zero." });
        }

        var deleted = await _studentService.DeleteStudentAsync(id);

        if (!deleted)
        {
            return NotFound(new { message = "Student not found." });
        }

        return NoContent();
    }
}