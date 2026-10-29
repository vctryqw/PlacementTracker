using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PlacementTracker.Data;
using PlacementTracker.Models;

namespace PlacementTracker.Pages;

public class IndexModel : PageModel
{
    private readonly AppDbContext _db;

    public IndexModel(AppDbContext db)
    {
        _db = db;
    }

    public List<JobApplication> RemindersDue { get; set; } = new();
    public List<JobApplication> UpcomingDeadlines { get; set; } = new();

    public async Task OnGetAsync()
    {

        var all = await _db.JobApplications.ToListAsync();

        RemindersDue = all
            .Where(a => a.ReminderDate != null
                     && a.ReminderDate.Value.Date <= DateTime.Today
                     && a.Status != ApplicationStatus.Offer
                     && a.Status != ApplicationStatus.Rejected)
            .OrderBy(a => a.ReminderDate)
            .ToList();

        UpcomingDeadlines = all
            .Where(a => a.DaysLeft >= 0
                     && a.DaysLeft <= 14
                     && a.Status != ApplicationStatus.Offer
                     && a.Status != ApplicationStatus.Rejected)
            .OrderBy(a => a.Deadline)
            .ToList();
    }

    public async Task<IActionResult> OnPostDismissAsync(int id)
    {
        var found = await _db.JobApplications.FindAsync(id);

        if (found != null)
        {
            found.ReminderDate = null;
            found.ReminderNote = null;
            await _db.SaveChangesAsync();
        }

        return RedirectToPage();
    }
}