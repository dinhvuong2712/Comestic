using System.ComponentModel.DataAnnotations;

namespace Comestics.Models
{
    public class CreateAccountModel
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string confirmPassword { get; set; }
        [Required]
        [StringLength(10)]
        public string Phone { get; set; }
        [Required]
        [StringLength(50)]
        public string email { get; set; }
        [StringLength(100)]
        public string address { get; set; }
    }
}