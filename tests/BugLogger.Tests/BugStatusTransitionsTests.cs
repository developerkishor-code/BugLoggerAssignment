using BugLogger.Models;
using BugLogger.Services;

namespace BugLogger.Tests;

public class BugStatusTransitionsTests
{
    [Fact]
    public void IsValid_AllowsNextStatus()
    {
        Assert.True(BugStatusTransitions.IsValid(BugStatus.New, BugStatus.Open));
    }

    [Fact]
    public void IsValid_RejectsSkippingAStatus()
    {
        Assert.False(BugStatusTransitions.IsValid(BugStatus.New, BugStatus.Resolved));
    }

    [Fact]
    public void IsValid_RejectsSkippingFromOpenToClosed()
    {
        Assert.False(BugStatusTransitions.IsValid(BugStatus.Open, BugStatus.Closed));
    }
}
