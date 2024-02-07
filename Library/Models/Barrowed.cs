using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Barrowed
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("MemberId")]
        [Required]
        public int BookName { get; set; }
        [Required]
        public DateOnly StartDate { get; set; }
        [Required]
        public DateOnly EndDate { get; set;}
    }
}
