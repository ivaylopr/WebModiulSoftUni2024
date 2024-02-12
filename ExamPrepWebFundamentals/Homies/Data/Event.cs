using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homies.Data
{
    public class Event
    {
        [Key]
        [Comment("Identifier of the event")]
        public int Id { get; set; }
        [Required]
        [MaxLength(DataConstants.EventNameMaxLenght)]
        [Comment("Name of the event")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(DataConstants.EventDescriptionMaxLenght)]
        [Comment("Description of the event")]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Comment("Identifier of the event organiser ")]
        public string OrganiserId { get; set; } = string.Empty;

        [ForeignKey(nameof(OrganiserId))]
        [Required]
        public IdentityUser Organiser { get; set; } = null!;
        [Required]
        [Comment("The date and  time of the event creation")]
        public DateTime CreatedOn { get; set; }
        [Required]
        [Comment("Time of the event start ")]
        public DateTime Start { get; set; }
        [Required]
        [Comment("Time of the event end")]
        public DateTime End { get; set; }
        [Required]
        [Comment("Identifier of the event Type")]
        public int TypeId { get; set; }
        [Required]
        [ForeignKey(nameof(TypeId))]
        
        public Type Type { get; set; } = null!;
        public IList<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();
    }
}
