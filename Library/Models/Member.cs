using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
       
        public DateOnly? BirthDate { get; set; }
        [Required]
        public int NationalCode { get; set; }
        public string? Adress {  get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
