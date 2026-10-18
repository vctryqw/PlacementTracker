using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlacementTracker.Data;
using PlacementTracker.Models;

namespace PlacementTracker.Pages.Applications;

public class CreateModel : PageModel
{
    private readonly AppDbContext _db;

    public CreateModel(AppDbContext db)
    {
        _db = db;
    }

    [BindProperty]
    public JobApplication JobApplication { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _db.JobApplications.Add(JobApplication);

        await _db.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}