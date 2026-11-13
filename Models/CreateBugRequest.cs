using System.ComponentModel.DataAnnotations;

namespace BugLogger.Models;

public class CreateBugRequest
{
    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public BugSeverity? Severity { get; set; }
}
