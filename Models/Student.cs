using System.Text.Json;

namespace SAA.Models;

public class Student
{
    public int Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Dni { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; }
    public bool IsActive { get; set; }

    public Student(int id, string firstName, string lastName, string dni, DateTime dateOfBirth, string address,
        bool isActive)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Dni = dni;
        DateOfBirth = dateOfBirth;
        Address = address;
        IsActive = isActive;
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;

        var other = (Student)obj;
        return Id == other.Id && FirstName == other.FirstName && LastName == other.LastName;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, FirstName, LastName);
    }
}