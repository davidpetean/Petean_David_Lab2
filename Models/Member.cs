using System.ComponentModel.DataAnnotations;

namespace Petean_David_Lab2.Models
{
    public class Member
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "Address")]
        public string? Adress { get; set; }

        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string? Phone { get; set; }

        [Display(Name = "Full Name")]
        public string? FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public ICollection<Borrowing>? Borrowings { get; set; }
    }
}
