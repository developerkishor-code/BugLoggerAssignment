using BugLogger.Models;

namespace BugLogger.Services;

public static class BugStatusTransitions
{
    public static bool IsValid(BugStatus current, BugStatus next)
    {
        if (current == BugStatus.New && next == BugStatus.Open)
        {
            return true;
        }

        if (current == BugStatus.Open && next == BugStatus.Resolved)
        {
            return true;
        }

        if (current == BugStatus.Resolved && next == BugStatus.Closed)
        {
            return true;
        }

        return false;
    }
}
