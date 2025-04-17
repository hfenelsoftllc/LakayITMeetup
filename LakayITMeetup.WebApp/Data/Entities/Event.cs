using System.ComponentModel.DataAnnotations;

namespace LakayITMeetup.WebApp.Data.Entities
{
    public class Event
    {
        public int EventId { get; set; }
        
        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "The title must be at most 100 characters long.")]
        public string? Title { get; set; }

        [Required]
        public DateOnly BeginDate { get; set; }

        [Required]
        public TimeOnly BeginTime { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }

        [Required]
        public TimeOnly EndTime { get; set; }

        [StringLength(maximumLength: 500, ErrorMessage = "The description must be at most 500 characters long.")]
        public string? Description { get; set; }

        public string? Location { get; set; }

        public string MeetupLink { get; set; }

        [Required]
        public string Category { get; set; }

        [Range(1, int.MaxValue)]
        public int Capacity { get; set; }

        public int OrganizerId { get; set; }




    }
}
