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

    public async Task OnGetAsync()
    {
        JobApplications = await _db.JobApplications.ToListAsync();
    }
}