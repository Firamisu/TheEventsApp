using System.ComponentModel.DataAnnotations;

namespace TheEventsApp.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MaxParticipants { get; set; }
        [Required]
        public ApplicationUser Organizer { get; set; }

        public ICollection<ApplicationUser> Participants { get; set; }

    }
}


