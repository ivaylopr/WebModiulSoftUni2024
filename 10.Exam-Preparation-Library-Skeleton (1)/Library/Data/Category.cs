﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Library.Data
{
    public class Category
    {
        [Key]
        [Comment("Identifier")]
        public int Id { get; set; }
        [Required]
        [MaxLength(DataConstants.CategoryNameMaxLenght)]
        [Comment("Category name")]
        public string Name { get; set; }
        [Comment("Books of the category")]
        public IEnumerable<Book> Books { get; set; }=new List<Book>();
    }
}
//Has Id – a unique integer, Primary Key
//• Has Name – a string with min length 5 and max length 50 (required)
//• Has Books – a collection of type Books