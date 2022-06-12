using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Models
{
    public class Firm
    {
        public Firm()
        {
            this.Employees = new HashSet<Employee>();
        }
        [Key]
        public int Id { get; set; }
        [Required,MaxLength(100)]
        public string Name { get; set; }
        [Required, MaxLength(500)]
        public string Description { get; set; }

        [ForeignKey("FirmID")]
        public ICollection<Employee> Employees { get; set; }
    }
}
