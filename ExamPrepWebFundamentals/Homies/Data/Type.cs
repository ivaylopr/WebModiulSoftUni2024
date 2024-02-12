using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Homies.Data
{
    public class Type
    {
        [Key]
        [Comment("Type identifier")]
        public int Id { get; set; }
        [Required]
        [Comment("Type name")]
        [MaxLength()]
        public string Name { get; set; }
        [Comment("Collection of events of the type")]
        public IList<Event> Events { get; set; } = new List<Event>();
    }
}

