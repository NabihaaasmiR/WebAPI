using Microsoft.AspNetCore.Mvc;
using WebApplication3.DTOs;
using WebApplication3.Models;
using WebApplication3.Services;

namespace WebApplication3.Controllers;

[ApiController]
[Route("api/students")]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    // GET: api/students
    [HttpGet]
    public ActionResult<List<StudentDto>> GetAllStudents()
    {
        var students = _studentService.GetAllStudents();
        var studentDtos = students.Select(s => new StudentDto
        {
            Id = s.Id,
            Name = s.Name,
            Email = s.Email,
            Age = s.Age
        }).ToList();

        return Ok(studentDtos);
    }

    // GET: api/students/1
    [HttpGet("{id}")]
    public ActionResult<StudentDto> GetStudentById(int id)
    {
        var student = _studentService.GetStudentById(id);

        if (student == null)
            return NotFound(new { message = "Student not found." });

        var studentDto = new StudentDto
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            Age = student.Age
        };

        return Ok(studentDto);
    }

    // POST: api/students
    [HttpPost]
    public ActionResult<StudentDto> AddStudent(CreateStudentDto studentDto)
    {
        var student = new Student
        {
            Name = studentDto.Name,
            Email = studentDto.Email,
            Age = studentDto.Age
        };

        var createdStudent = _studentService.AddStudent(student);

        var responseDto = new StudentDto
        {
            Id = createdStudent.Id,
            Name = createdStudent.Name,
            Email = createdStudent.Email,
            Age = createdStudent.Age
        };

        return CreatedAtAction(
            nameof(GetStudentById),
            new { id = responseDto.Id },
            responseDto);
    }

    // PUT: api/students/1
    [HttpPut("{id}")]
    public ActionResult<StudentDto> UpdateStudent(int id, UpdateStudentDto studentDto)
    {
        var updatedStudent = new Student
        {
            Name = studentDto.Name,
            Email = studentDto.Email,
            Age = studentDto.Age
        };

        var student = _studentService.UpdateStudent(id, updatedStudent);

        if (student == null)
            return NotFound(new { message = "Student not found." });

        var responseDto = new StudentDto
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            Age = student.Age
        };

        return Ok(responseDto);
    }

    // DELETE: api/students/1
    [HttpDelete("{id}")]
    public IActionResult DeleteStudent(int id)
    {
        var deleted = _studentService.DeleteStudent(id);

        if (!deleted)
            return NotFound(new { message = "Student not found." });

        return NoContent();
    }
}