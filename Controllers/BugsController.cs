using BugLogger.Models;
using BugLogger.Services;
using Microsoft.AspNetCore.Mvc;

namespace BugLogger.Controllers;

[ApiController]
[Route("bugs")]
public class BugsController : ControllerBase
{
    private readonly BugService _bugService;

    public BugsController(BugService bugService)
    {
        _bugService = bugService;
    }

    [HttpPost]
    public ActionResult<Bug> Create(CreateBugRequest request)
    {
        if (!request.Severity.HasValue)
        {
            return BadRequest("Severity is required.");
        }

        Bug bug = _bugService.Create(request.Title, request.Severity.Value);
        return CreatedAtAction(nameof(GetById), new { id = bug.Id }, bug);
    }

    [HttpGet]
    public ActionResult<IReadOnlyList<Bug>> GetAll(
        [FromQuery] BugStatus? status,
        [FromQuery] BugSeverity? severity)
    {
        return Ok(_bugService.GetAll(status, severity));
    }

    [HttpGet("{id:int}", Name = nameof(GetById))]
    public ActionResult<Bug> GetById(int id)
    {
        Bug? bug = _bugService.GetById(id);

        if (bug == null)
        {
            return NotFound();
        }

        return Ok(bug);
    }

    [HttpPut("{id:int}/status")]
    public IActionResult UpdateStatus(int id, UpdateBugStatusRequest request)
    {
        Bug? bug = _bugService.GetById(id);
        if (bug == null)
        {
            return NotFound();
        }

        if (!request.Status.HasValue)
        {
            return BadRequest("Status is required.");
        }

        BugStatus newStatus = request.Status.Value;
        if (!BugStatusTransitions.IsValid(bug.Status, newStatus))
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Invalid bug status transition",
                Detail = "Cannot change status from " + bug.Status + " to " + newStatus +
                    ". Allowed transitions are New -> Open -> Resolved -> Closed."
            });
        }

        _bugService.UpdateStatus(bug, newStatus);
        return Ok(bug);
    }
}
