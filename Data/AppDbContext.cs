using Microsoft.EntityFrameworkCore;
using PlacementTracker.Models;

namespace PlacementTracker.Data;

public class AppDbContext : DbContext
{
 public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }   

    public DbSet<JobApplication> JobApplications { get; set; }
}