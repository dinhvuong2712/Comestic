using System.ComponentModel.DataAnnotations;

namespace Comestics.Models
{
    public class ContactModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [StringLength(10)]
        public string Phone { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
    }
}