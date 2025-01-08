using Microsoft.AspNetCore.Identity;

namespace TheEventsApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Event> Events { get; set; }
    }

}
