using Microsoft.AspNetCore.Mvc;
using WebApplication3.DTOs;
using WebApplication3.Services;

namespace WebApplication3.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly ILogger<StudentsController> _logger;

    public StudentsController(
        IStudentService studentService,
        ILogger<StudentsController> logger)
    {
        _studentService = studentService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<StudentDto>>> GetStudents(
        [FromQuery] string? search = null,
        [FromQuery] int? departmentId = null,
        [FromQuery] string? sortBy = "id",
        [FromQuery] string? sortOrder = "asc",
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        // Validate pagination
        if (pageNumber < 1)
            return BadRequest("Page number must be at least 1.");

        if (pageSize < 1 || pageSize > 100)
            return BadRequest("Page size must be between 1 and 100.");

        // Normalize sorting parameters
        sortBy = string.IsNullOrWhiteSpace(sortBy)
            ? "id"
            : sortBy.Trim().ToLower();

        sortOrder = string.IsNullOrWhiteSpace(sortOrder)
            ? "asc"
            : sortOrder.Trim().ToLower();

        // Validate sorting
        if (sortBy != "id" && sortBy != "name" && sortBy != "age")
            return BadRequest("sortBy must be id, name, or age.");

        if (sortOrder != "asc" && sortOrder != "desc")
            return BadRequest("sortOrder must be asc or desc.");

        if (departmentId.HasValue && departmentId.Value < 1)
            return BadRequest("Department ID must be greater than 0.");

        var result = await _studentService.GetFilteredAndPagedStudentsAsync(
            search,
            departmentId,
            sortBy,
            sortOrder,
            pageNumber,
            pageSize);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<StudentDto>> GetStudentById(int id)
    {
        var student = await _studentService.GetStudentByIdAsync(id);

        if (student is null)
            return NotFound();

        return Ok(student);
    }

    [HttpPost]
    public async Task<ActionResult<StudentDto>> CreateStudent(
        [FromBody] CreateStudentDto createDto)
    {
        if (await _studentService.EmailExistsAsync(createDto.Email))
        {
            ModelState.AddModelError("Email", "A student with this email already exists.");
            return BadRequest(ModelState);
        }

        var student = await _studentService.AddStudentAsync(createDto);

        return CreatedAtAction(
            nameof(GetStudentById),
            new { id = student.Id },
            student);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<StudentDto>> UpdateStudent(
        int id,
        [FromBody] UpdateStudentDto updateDto)
    {
        if (await _studentService.EmailExistsAsync(updateDto.Email, id))
        {
            ModelState.AddModelError("Email", "A student with this email already exists.");
            return BadRequest(ModelState);
        }

        var updatedStudent = await _studentService.UpdateStudentAsync(id, updateDto);

        if (updatedStudent is null)
            return NotFound();

        return Ok(updatedStudent);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var deleted = await _studentService.DeleteStudentAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}