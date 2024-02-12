using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homies.Data
{
    [Comment("Navigation class between User and Event")]
    public class EventParticipant
    {
        [Required]
        [Comment("Identifier of the User")]
        public string HelperId { get; set; } = string.Empty;
        [Required]
        [ForeignKey(nameof(HelperId))]
        public IdentityUser Helper { get; set; } = null!;
        [Required]
        [Comment("Identifier of the Event")]
        public int EventId { get; set; }
        [Required]
        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; } = null!;
    }
}
//⦁	HelperId – a  string, Primary Key, foreign key (required)
//⦁	Helper – IdentityUser
//⦁	EventId – an integer, Primary Key, foreign key (required)
//⦁	Event – Event
