using WebApplication3.DTOs;
using WebApplication3.Models;
using WebApplication3.Repositories;

namespace WebApplication3.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<List<StudentDto>> GetAllStudentsAsync()
    {
        var students = await _studentRepository.GetAllAsync();
        return students.Select(MapToDto).ToList();
    }

    public async Task<StudentDto?> GetStudentByIdAsync(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        return student == null ? null : MapToDto(student);
    }

    public async Task<StudentDto> CreateStudentAsync(CreateStudentDto createDto)
    {
        if (await _studentRepository.EmailExistsAsync(createDto.Email))
        {
            throw new ArgumentException("A student with this email already exists.");
        }

        if (createDto.Age < 17 || createDto.Age > 60)
        {
            throw new ArgumentException("Age must be between 17 and 60.");
        }

        var student = new Student
        {
            Name = createDto.Name,
            Email = createDto.Email,
            Age = createDto.Age,
            DepartmentId = createDto.DepartmentId
        };

        await _studentRepository.AddAsync(student);

        var created = await _studentRepository.GetByIdAsync(student.Id);
        return MapToDto(created ?? student);
    }

    public async Task<bool> UpdateStudentAsync(int id, UpdateStudentDto updateDto)
    {
        var existingStudent = await _studentRepository.GetByIdAsync(id);
        if (existingStudent == null)
        {
            return false;
        }

        if (await _studentRepository.EmailExistsAsync(updateDto.Email, id))
        {
            throw new ArgumentException("Another student with this email already exists.");
        }

        if (updateDto.Age < 17 || updateDto.Age > 60)
        {
            throw new ArgumentException("Age must be between 17 and 60.");
        }

        existingStudent.Name = updateDto.Name;
        existingStudent.Email = updateDto.Email;
        existingStudent.Age = updateDto.Age;
        existingStudent.DepartmentId = updateDto.DepartmentId;

        await _studentRepository.UpdateAsync(existingStudent);
        return true;
    }

    public async Task<bool> DeleteStudentAsync(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        if (student == null)
        {
            return false;
        }

        await _studentRepository.DeleteAsync(student);
        return true;
    }

    private static StudentDto MapToDto(Student student)
    {
        return new StudentDto
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            Age = student.Age,
            DepartmentName = student.Department?.DepartmentName
        };
    }
}