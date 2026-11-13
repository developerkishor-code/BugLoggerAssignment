namespace BugLogger.Models;

public class Bug
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public BugSeverity Severity { get; set; }

    public BugStatus Status { get; set; }
}
