using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Models
{
    
    public class Employee
    {
        public Employee()
        {
            this.Firms = new HashSet<Firm>();
        }
        [Key]
        public int Id { get; set; }
        [Display(Name = "Nazwa użytkownika")]
        public string userName { get; set; }
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Display(Name = "Imię")
        , RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", 
            ErrorMessage = "Imię jest niepoprawne!")]
        public string? firstName { get; set; }
        [Display(Name = "Nazwisko")
        , RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
        ErrorMessage = "Nazwisko jest niepoprawne!")]
        public string? lastName { get; set; }
        [EmailAddressAttribute, Display(Name = "E-mail")]
        public string? email { get; set; }

        public string? phoneNumber { get; set; }
        public byte[]? photo{ get; set; }
        public Roles? role { get; set; }

        [ForeignKey("EmployeeID")]
        public ICollection<Firm> Firms { get; set; }
    }

    public enum Roles
    {
        Admin,
        Hired,
        User
    }
}
