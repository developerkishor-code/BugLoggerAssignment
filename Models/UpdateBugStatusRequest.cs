using System.ComponentModel.DataAnnotations;

namespace BugLogger.Models;

public class UpdateBugStatusRequest
{
    [Required]
    public BugStatus? Status { get; set; }
}
