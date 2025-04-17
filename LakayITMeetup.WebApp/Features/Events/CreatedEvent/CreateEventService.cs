using LakayITMeetup.WebApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LakayITMeetup.WebApp.Features.Events.CreatedEvent
{
    public class CreateEventService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public CreateEventService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            this._dbContextFactory = dbContextFactory;
        }

        public async Task CreateEventAsync(EventViewModel eventViewModel)
        {
            if (eventViewModel == null)
            {
                throw new ArgumentNullException(nameof(eventViewModel));
            }

            using (var context = _dbContextFactory.CreateDbContext())
            {
                var newEvent = new Event
                {
                    Title = eventViewModel.Title ?? string.Empty, // Ensure non-null value
                    BeginDate = eventViewModel.BeginDate,
                    BeginTime = eventViewModel.BeginTime,
                    EndDate = eventViewModel.EndDate,
                    EndTime = eventViewModel.EndTime,
                    Description = eventViewModel.Description ?? string.Empty, // Ensure non-null value
                    Location = eventViewModel.Location ?? string.Empty, // Ensure non-null value
                    MeetupLink = eventViewModel.MeetupLink ?? string.Empty, // Ensure non-null value
                    Category = eventViewModel.Category ?? string.Empty, // Ensure non-null value
                    Capacity = eventViewModel.Capacity
                };
                context.Events.Add(newEvent);
                await context.SaveChangesAsync();
            }
        }

        public string? ValidateEvent(EventViewModel eventViewModel) 
        {
            if (eventViewModel == null)
            {
                return "Event cannot be null";
            }
            string? errorMessage = eventViewModel.ValidateDates();

            if (!string.IsNullOrEmpty(errorMessage))
            {
                return errorMessage;
            }
            if (string.IsNullOrEmpty(eventViewModel.Title))
            {
                return "Title cannot be empty";
            }
            if (string.IsNullOrEmpty(eventViewModel.Description))
            {
                return "Description cannot be empty";
            }
            if (string.IsNullOrEmpty(eventViewModel.Location))
            {
                return "Location cannot be empty";
            }
            //if (string.IsNullOrEmpty(eventViewModel.MeetupLink))
            //{
            //    return "Meetup link cannot be empty";
            //}
            if (string.IsNullOrEmpty(eventViewModel.Category))
            {
                return "Category cannot be empty";
            }
            if (eventViewModel.Capacity <= 0)
            {
                return "Capacity should be greater than 0";
            }
            if (eventViewModel.Capacity > 1000)
            {
                return "Capacity should not exceed 1000";
            }
            if (eventViewModel.BeginDate < DateOnly.FromDateTime(DateTime.Now))
            {
                return "Begin date should be in the future";
            }
            if (eventViewModel.EndDate < eventViewModel.BeginDate)
            {
                return "End date should be greater than begin date";
            }
            if (eventViewModel.EndDate > eventViewModel.BeginDate.AddDays(1))
            {
                return "Event should not last more than 24 hours";
            }
            if (eventViewModel.Category == MeetupCategoriesEnum.InPerson.ToString() && string.IsNullOrWhiteSpace(eventViewModel.Location))
            {
                return "Location is required for In-Person MeetUp.";
            }
            if (eventViewModel.Category == MeetupCategoriesEnum.Online.ToString() && string.IsNullOrWhiteSpace(eventViewModel.MeetupLink))
            {
                return "The Meetup link is required for online Meetups";
            }
            if (eventViewModel.Category == MeetupCategoriesEnum.Hybrid.ToString() && string.IsNullOrWhiteSpace(eventViewModel.MeetupLink))
            {
                return "The Meetup link is required for Hybrid Meetups";
            }
            if (eventViewModel.Category == MeetupCategoriesEnum.Hybrid.ToString() && string.IsNullOrWhiteSpace(eventViewModel.Location))
            {
                return "Location is required for Hybrid Meetups";
            }

            return string.Empty;
        }
    }
}
