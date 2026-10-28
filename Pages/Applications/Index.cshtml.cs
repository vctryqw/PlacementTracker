using Microsoft.AspNetCore.Mvc;   // NEW
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PlacementTracker.Data;
using PlacementTracker.Models;

namespace PlacementTracker.Pages.Applications;

public class IndexModel : PageModel
{
    private readonly AppDbContext _db;

    public IndexModel(AppDbContext db)
    {
        _db = db;
    }

    public List<JobApplication> JobApplications { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string? CompanyFilter { get; set; }

    [BindProperty(SupportsGet = true)]
    public ApplicationStatus? StatusFilter { get; set; }

    public List<string> Companies { get; set; } = new();

    public async Task OnGetAsync()
    {

        Companies = await _db.JobApplications
            .Select(a => a.CompanyName)
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync();

        IQueryable<JobApplication> query = _db.JobApplications;

        if (!string.IsNullOrEmpty(CompanyFilter))
        {
            query = query.Where(a => a.CompanyName == CompanyFilter);
        }

        if (StatusFilter != null)
        {
            query = query.Where(a => a.Status == StatusFilter);
        }

        JobApplications = await query
            .OrderBy(a => a.Deadline == null)
            .ThenBy(a => a.Deadline)
            .ToListAsync();
    }
}