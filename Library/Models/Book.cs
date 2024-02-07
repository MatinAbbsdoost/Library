using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace Library.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } 

        public DateOnly PublishDate { get; set; }

        public string? Subject {  get; set; }
         
        public int SheetCount { get; set; }
    }
}
