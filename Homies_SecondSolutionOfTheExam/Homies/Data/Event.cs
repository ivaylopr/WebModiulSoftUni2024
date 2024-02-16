using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homies.Data
{
    [Comment("Event class")]
    public class Event
    {
        [Key]
        [Comment("Identifier")]
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
        [Comment("Organiser of the event")]
        public string OrganiserId { get; set; } = string.Empty;
        [Required]
        [ForeignKey(nameof(OrganiserId))]
        public IdentityUser Organiser { get; set; } = null!;
        [Required]
        [Comment("Creation time of the event")]
        public DateTime CreatedOn { get; set; }
        [Required]
        [Comment("Time of the event start")]
        public DateTime Start { get; set; }
        [Required]
        [Comment("Time when the event will finish")]
        public DateTime End { get; set; }
        [Required]
        [Comment("Event type")]
        public int TypeId { get; set; }
        [Required]
        [ForeignKey(nameof(TypeId))]
        public Type Type { get; set; }
        public IList<EventParticipant> EventsParticipants { get; set; } = new List<EventParticipant>();
    }
}
//⦁	Has Id – a unique integer, Primary Key
//⦁	Has Name – a string with min length 5 and max length 20 (required)
//⦁	Has Description – a string with min length 15 and max length 150 (required)
//⦁	Has OrganiserId – an string (required)
//⦁	Has Organiser – an IdentityUser (required)
//⦁	Has CreatedOn – a DateTime with format "yyyy-MM-dd H:mm" (required) (the DateTime format is recommended, if you are having troubles with this one, you are free to use another one)
//⦁	Has Start – a DateTime with format "yyyy-MM-dd H:mm" (required) (the DateTime format is recommended, if you are having troubles with this one, you are free to use another one)
//⦁	Has End – a DateTime with format "yyyy-MM-dd H:mm" (required) (the DateTime format is recommended, if you are having troubles with this one, you are free to use another one)
//⦁	Has TypeId – an integer, foreign key (required)
//⦁	Has Type – a Type (required)
//⦁	Has EventsParticipants – a collection of type EventParticipant
