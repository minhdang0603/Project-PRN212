using System;
using System.Collections.Generic;

namespace BussinessObject;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public int CourseHours { get; set; }

    public string? CourseDescription { get; set; }

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();
}
