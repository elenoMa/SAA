using System.Text.Json;

namespace SAA.Models;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; }

    public Subject(int id, string name, bool isActive)
    {
        Id = id;
        Name = name;
        IsActive = isActive;
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;

        var other = (Subject)obj;
        return Id == other.Id && Name == other.Name && IsActive == other.IsActive;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, IsActive);
    }
}