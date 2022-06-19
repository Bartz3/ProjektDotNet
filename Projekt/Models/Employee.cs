using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Models
{
    
    public class Employee
    {

        [Key]
        public int Id { get; set; }
        [Display(Name = "Login")]
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
        [DataType(DataType.PhoneNumber)]
        public string? phoneNumber { get; set; }
        public byte[]? photo{ get; set; }
        public Roles? role { get; set; }

        public int? FirmID { get; set; }
        //[ForeignKey("EmployeeID")]
        [Display(Name = "Firma")]
        public Firm? Firm { get; set; }
    }

}
