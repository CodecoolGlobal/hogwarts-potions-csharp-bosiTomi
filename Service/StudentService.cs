using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HogwartsPotions.DataAccess;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Service;

public class StudentService : IStudentService
{
    private readonly HogwartsContext _context;

    public StudentService(HogwartsContext context)
    {
        _context = context;
    }

    public async Task AddStudent(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
    }

    public Task<Student> GetStudent(long studentId)
    {
        return _context.Students.FirstOrDefaultAsync(student => student.ID == studentId);
    }

    public Task<Student> GetStudentByUsername(string username)
    {
        return _context.Students.FirstOrDefaultAsync(student => student.Name == username);
    }

    public Task<List<Student>> GetAllStudents()
    {
        return _context.Students.ToListAsync();
    }

    public async Task UpdateStudent(Student student)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteStudent(long id)
    {
        var studentToDelete = await GetStudent(id);
        if (studentToDelete != null)
        {
            _context.Students.Remove(studentToDelete);
            await _context.SaveChangesAsync();
        }
    }
    public bool ValidateLogin(LoginForm user)
    {
        // Hash the entered password
        string hashedPassword = PasswordHash.HashPassword(user.Password);

        return _context.Students.AsEnumerable().Any(u => u.Name == user.Username && FixedTimeEquals(u.Password, hashedPassword));
    }

    private bool FixedTimeEquals(string str1, string str2)
    {
        if (str1 == null || str2 == null)
        {
            return false;
        }

        return CryptographicOperations.FixedTimeEquals(Encoding.UTF8.GetBytes(str1), Encoding.UTF8.GetBytes(str2));
    }


    private bool CheckRegistrationStatus(string user)
    {
        var u = _context.Students.FirstOrDefault(u => u.Name == user);
        return u == null;
    }

    public bool Register(Student user)
    {
        if (CheckRegistrationStatus(user.Name))
        {
            user.Password = PasswordHash.HashPassword(user.Password);
            _context.Students.Add(user);
            _context.SaveChanges();
            return true;
        }

        return false;
    }
}