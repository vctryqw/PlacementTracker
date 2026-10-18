using System.ComponentModel.DataAnnotations;

namespace PlacementTracker.Models;

public class JobApplication
{
    public int Id { get; set; }
    public string CompanyName { get; set; } = "";
    public string Position { get; set; } = "";
    public string? Location { get; set; }
    public string? Link { get; set; }
    public ApplicationStatus Status { get; set; }
    public DateTime? ApplicationDate { get; set; }
    public DateTime? Deadline { get; set; }
    public DateTime? InterviewDate { get; set; }
    public DateTime? ReminderDate { get; set; }
    public string? ReminderNote { get; set; }
    public string? Notes { get; set; }
}

public enum ApplicationStatus
{
    Interested,
    Applied,
    Screening,
    Interview,
    Offer,
    Rejected
}