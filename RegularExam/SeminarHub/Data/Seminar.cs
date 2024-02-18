using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeminarHub.Data
{
    [Comment("Seminar class")]
    public class Seminar
    {
        [Key]
        [Comment("Identifier")]
        public int Id { get; set; }
        [Required]
        [MaxLength(DataConstants.SeminarTopicMaxLenght)]
        [Comment("Topic of the seminar")]
        public string Topic { get; set; } = string.Empty;
        [Required]
        [MaxLength(DataConstants.SeminarLecturerMaxLenght)]
        [Comment("Seminars' lecturer name")]
        public string Lecturer { get; set; } = string.Empty;
        [Required]
        [MaxLength(DataConstants.SeminarDetailsMaxLenght)]
        [Comment("Details about the seminar")]
        public string Details { get; set; } = string.Empty;
        [Required]
        [Comment("User who organise the seminar")]
        public string OrganizerId { get; set; } = string.Empty;
        [Required]
        [ForeignKey(nameof(OrganizerId))]
        public IdentityUser Organizer { get; set; } = null!;
        [Required]
        [Comment("Date and time when will the seminar be")]
        public DateTime DateAndTime { get; set; }
        [Comment("Duration of the seminar")]
        public int? Duration { get; set; }
        [Required]
        [Comment("Seminar category")]
        public int CategoryId { get; set; }
        [Required]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;
        public IList<SeminarParticipant> SeminarsParticipants { get; set; } = new List<SeminarParticipant>();
    }
}
