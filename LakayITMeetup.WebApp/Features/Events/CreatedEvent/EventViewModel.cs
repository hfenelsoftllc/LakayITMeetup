using System.ComponentModel.DataAnnotations;

namespace LakayITMeetup.WebApp.Features.Events.CreatedEvent
{
    public class EventViewModel
    {
        public int EventId { get; set; }
        [Required]
        [StringLength(maximumLength:100, ErrorMessage = "The title must be at most 100 characters long.")]
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

        public EventViewModel()
        {
            BeginDate = DateOnly.FromDateTime(DateTime.Now);
            EndDate = DateOnly.FromDateTime(DateTime.Now);
            BeginTime = TimeOnly.FromDateTime(DateTime.Now);
            EndTime = TimeOnly.FromDateTime(DateTime.Now);
            Category = MeetupCategoriesEnum.InPerson.ToString();
        }

        public string? ValidateDates() 
        { 
            if(BeginTime >= EndTime)
            {
                return "Begin Time should be less than End Time";
            }
            if (BeginTime == EndTime)
            {
                return "Begin Time should be earlier  than End Time";
            }
            DateTime combinedBeginDateTime = new DateTime(BeginDate.Year, BeginDate.Month, BeginDate.Day, BeginTime.Hour, BeginTime.Minute, BeginTime.Second);
            DateTime combinedEndDateTime = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndTime.Hour, EndTime.Minute, EndTime.Second);
            
            if(combinedBeginDateTime < DateTime.Now)
            {
                return "Begin Date and Time shoould be in the future";
            }
            if(combinedEndDateTime<= combinedBeginDateTime)
            {
                return "End Date and Time should be later than Begin Date and Time.";
            }
            if (combinedEndDateTime - combinedBeginDateTime > TimeSpan.FromDays(1))
            {
                return "The event should not last more than 24 hours";
            }
            return string.Empty;
        }

        public string? ValidateCapacity()
        {
            if (Capacity <= 0)
            {
                return "Capacity should be greater than 0 attendee";
            }
            return string.Empty;
        }

        public string? ValidateLocation()
        {
            if (Category == MeetupCategoriesEnum.InPerson.ToString() && string.IsNullOrWhiteSpace(Location))
            {
                return "Location is required for In-Person MeetUp.";
            }
            return string.Empty;
        }

        public string? ValidateMeetupLink()
        {
            if (Category == MeetupCategoriesEnum.Online.ToString() && string.IsNullOrWhiteSpace(MeetupLink))
            {
                return "The Meetup link is required for online Meetups";
            }
            return string.Empty;
        }

    }
}
