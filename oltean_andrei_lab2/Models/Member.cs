using System.ComponentModel.DataAnnotations;

namespace oltean_andrei_lab2.Models
{
    public class Member
    {
        public int ID { get; set; }

        [RegularExpression(@"^[A-Z][a-z]+$", ErrorMessage = "Numele trebuie sa inceapa cu majuscula si sa contina doar litere.")]
        [StringLength(30, MinimumLength = 3)]
        public string? FirstName { get; set; }

        [RegularExpression(@"^[A-Z][a-z]+$", ErrorMessage = "Numele trebuie sa inceapa cu majuscula si sa contina doar litere.")]
        [StringLength(30, MinimumLength = 3)]
        public string? LastName { get; set; }

        [StringLength(70)]
        public string? Adress { get; set; }

        [Required]
        public string Email { get; set; }

        [RegularExpression(@"^\(?[0-9]{3}\)?[-. ]?[0-9]{3}[-. ]?[0-9]{4}$", ErrorMessage = "Formatul telefonului trebuie sa fie (07x) xxx xxxx sau 07x.xxx.xxxx sau 07x xxx xxxx")]
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