using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;   

namespace PlacementTracker.Models;

public enum ApplicationStatus
{
    Interested,
    Applied,
    Screening,
    Interview,
    Offer,
    Rejected
}

public class JobApplication
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Company name")]   
    public string CompanyName { get; set; } = "";

    [Required]
    public string Position { get; set; } = "";

    public string? Location { get; set; }

    [Display(Name = "Job advert link")]   
    public string? Link { get; set; }

    public ApplicationStatus Status { get; set; }

    [DataType(DataType.Date)]   
    [Display(Name = "Date applied")]   
    public DateTime? ApplicationDate { get; set; }

    [DataType(DataType.Date)]   
    public DateTime? Deadline { get; set; }

    [DataType(DataType.Date)]   
    [Display(Name = "Interview date")]   
    public DateTime? InterviewDate { get; set; }

    [DataType(DataType.Date)]   
    [Display(Name = "Remind me on")]   
    public DateTime? ReminderDate { get; set; }

    [Display(Name = "Reminder note")]   
    public string? ReminderNote { get; set; }

    public string? Notes { get; set; }

    [NotMapped]
    public int? DaysLeft
    {
        get
        {
            if (Deadline == null)
            {
                return null;
            }
            return (Deadline.Value.Date - DateTime.Today).Days;
        }
    }

    [NotMapped]
    public string DeadlineLabel
    {
        get
        {
            if (DaysLeft == null || Status == ApplicationStatus.Offer || Status == ApplicationStatus.Rejected)
            {
                return "";
            }
            if (DaysLeft < 0) return "Passed";
            if (DaysLeft == 0) return "Due today";
            if (DaysLeft == 1) return "Due tomorrow";
            return DaysLeft + " days left";
        }
    }


    [NotMapped]
    public string DeadlineClass
    {
        get
        {
            if (DaysLeft < 0) return "deadline passed";
            if (DaysLeft <= 3) return "deadline urgent";
            if (DaysLeft <= 7) return "deadline soon";
            return "deadline ok";
        }
    }
}