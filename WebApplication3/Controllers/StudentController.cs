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

    [HttpGet]
    public async Task<ActionResult<List<StudentDto>>> GetAllStudents()
    {
        var students = await _studentService.GetAllStudentsAsync();
        return Ok(students);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StudentDto>> GetStudentById(int id)
    {
        var student = await _studentService.GetStudentByIdAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        return Ok(student);
    }

    [HttpPost]
    public async Task<ActionResult<StudentDto>> CreateStudent([FromBody] CreateStudentDto createDto)
    {
        try
        {
            var createdStudent = await _studentService.CreateStudentAsync(createDto);
            return CreatedAtAction(nameof(GetStudentById), new { id = createdStudent.Id }, createdStudent);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(int id, [FromBody] UpdateStudentDto updateDto)
    {
        try
        {
            var updated = await _studentService.UpdateStudentAsync(id, updateDto);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var deleted = await _studentService.DeleteStudentAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}