using Microsoft.EntityFrameworkCore;

namespace LakayITMeetup.WebApp.Data.Entities
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        
        public DbSet<Event> Events { get; set; }
    }
}
