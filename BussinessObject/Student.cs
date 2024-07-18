using System;
using System.Collections.Generic;

namespace BussinessObject;

public partial class Student
{
    public int StudentId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();
}
