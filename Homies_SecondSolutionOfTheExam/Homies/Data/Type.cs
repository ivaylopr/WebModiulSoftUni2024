using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace Homies.Data
{
    [Comment("Type of the event")]
    public class Type
    {
        [Key]
        [Comment("Identifier of the type")]
        public int Id { get; set; }
        [Required]
        [MaxLength(DataConstants.TypeNameMaxLenght)]
        [Comment("Type name")]
        public string Name { get; set; } = string.Empty;
        public IEnumerable<Event> Events { get; set; } = new List<Event>();
    }
}
//⦁	Has Id – a unique integer, Primary Key
//⦁	Has Name – a string with min length 5 and max length 15 (required)
//⦁	Has Events – a collection of type Event
