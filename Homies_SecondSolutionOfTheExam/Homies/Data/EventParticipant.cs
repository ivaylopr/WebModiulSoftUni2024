using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homies.Data
{
    public class EventParticipant
    {
        [Required]
        public string HelperId { get; set; }
        [Required]
        [ForeignKey(nameof(HelperId))]
        public IdentityUser Helper { get; set; }
        [Required]
        public int EventId { get; set; }
        [Required]
        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; }
    }
}
//⦁	HelperId – a  string, Primary Key, foreign key (required)
//⦁	Helper – IdentityUser
//⦁	EventId – an integer, Primary Key, foreign key (required)
//⦁	Event – Event
