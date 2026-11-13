using BugLogger.Models;

namespace BugLogger.Services;

public class BugService
{
    private readonly object _lock = new();
    private readonly List<Bug> _bugs = [];
    private int _nextId = 1;

    public Bug Create(string title, BugSeverity severity)
    {
        lock (_lock)
        {
            Bug bug = new Bug
            {
                Id = _nextId,
                Title = title,
                Severity = severity,
                Status = BugStatus.New
            };

            _nextId++;
            _bugs.Add(bug);
            return bug;
        }
    }

    public IReadOnlyList<Bug> GetAll(BugStatus? status, BugSeverity? severity)
    {
        List<Bug> matchingBugs = new List<Bug>();

        lock (_lock)
        {
            foreach (Bug bug in _bugs)
            {
                if (status.HasValue && bug.Status != status.Value)
                {
                    continue;
                }

                if (severity.HasValue && bug.Severity != severity.Value)
                {
                    continue;
                }

                matchingBugs.Add(bug);
            }
        }

        return matchingBugs;
    }

    public Bug? GetById(int id)
    {
        lock (_lock)
        {
            foreach (Bug bug in _bugs)
            {
                if (bug.Id == id)
                {
                    return bug;
                }
            }
        }

        return null;
    }

    public void UpdateStatus(Bug bug, BugStatus status)
    {
        lock (_lock)
        {
            bug.Status = status;
        }
    }
}
