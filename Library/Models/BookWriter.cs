using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace Library.Models
{
    public class BookWriter
    {

        [ForeignKey("MemberId")]
        [Key]
        public int Id { get; set; }
        [ForeignKey("BookId")]
    
        public string? Title { get; set; }
     
       

    }
}
