using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlacementTracker.Data;
using PlacementTracker.Models;

namespace PlacementTracker.Pages.Applications;

public class DeleteModel : PageModel
{
    private readonly AppDbContext _db;

    public DeleteModel(AppDbContext db)
    {
        _db = db;
    }

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

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var found = await _db.JobApplications.FindAsync(id);

        if (found != null)
        {
            _db.JobApplications.Remove(found);
            await _db.SaveChangesAsync();
        }

        return RedirectToPage("Index");
    }
}