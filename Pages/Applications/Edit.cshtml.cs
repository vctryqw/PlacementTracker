using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlacementTracker.Data;
using PlacementTracker.Models;

namespace PlacementTracker.Pages.Applications;

public class EditModel : PageModel
{
    private readonly AppDbContext _db;

    public EditModel(AppDbContext db)
    {
        _db = db;
    }

    [BindProperty]
    public JobApplication JobApplication { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var found = await _db.JobApplications.FindAsync(id);

        if (found == null)
        {
            return NotFound();
        }

        JobApplication = found;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (JobApplication.ReminderDate > JobApplication.Deadline)
    {
        ModelState.AddModelError("JobApplication.ReminderDate", "Set the reminder on or before the deadline.");
    }
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _db.JobApplications.Update(JobApplication);
        await _db.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}