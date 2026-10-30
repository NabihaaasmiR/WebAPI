using WebApplication3.DTOs;
using WebApplication3.Models;

namespace WebApplication3.Services;

public class StudentService : IStudentService
{
    private readonly List<Student> _students = new()
    {
        new Student { Id = 1, Name = "Arun", Email = "arun@example.com", Age = 21 },
        new Student { Id = 2, Name = "Priya", Email = "priya@example.com", Age = 22 }
    };

    private int _nextId = 3;

    public List<StudentDto> GetAllStudents()
    {
        return _students.Select(s => new StudentDto
        {
            Id = s.Id,
            Name = s.Name,
            Email = s.Email,
            Age = s.Age
        }).ToList();
    }

    public StudentDto? GetStudentById(int id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student == null) return null;

        return new StudentDto
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            Age = student.Age
        };
    }

    public StudentDto AddStudent(CreateStudentDto studentDto)
    {
        var student = new Student
        {
            Id = _nextId++,
            Name = studentDto.Name,
            Email = studentDto.Email,
            Age = studentDto.Age
        };

        _students.Add(student);

        return new StudentDto
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            Age = student.Age
        };
    }

    public StudentDto? UpdateStudent(int id, UpdateStudentDto studentDto)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student == null) return null;

        student.Name = studentDto.Name;
        student.Email = studentDto.Email;
        student.Age = studentDto.Age;

        return new StudentDto
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            Age = student.Age
        };
    }

    public bool DeleteStudent(int id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student == null) return false;

        _students.Remove(student);
        return true;
    }

    public bool EmailExists(string email, int? excludeStudentId = null)
    {
        return _students.Any(s =>
            s.Email.Equals(email, StringComparison.OrdinalIgnoreCase)
            && (!excludeStudentId.HasValue || s.Id != excludeStudentId.Value));
    }
}