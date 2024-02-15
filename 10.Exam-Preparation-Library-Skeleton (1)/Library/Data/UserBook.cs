using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Data
{
    [Comment("User Books")]
    public class UserBook
    {
        [Comment("Collector of the book identifier")]
        public string CollectorId { get; set; }
        [ForeignKey(nameof(CollectorId))]
        public IdentityUser Collector { get; set; }
        
        [Comment("Book that was collected identifier")]
        public int BookId { get; set; }
        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; }
    }
}