using System;
using System.Collections.Generic;

namespace BussinessObject;

public partial class Score
{
    public int StudentId { get; set; }

    public int CourseId { get; set; }

    public decimal? StudentScore { get; set; }

    public string? StudentDescription { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
