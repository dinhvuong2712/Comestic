using System.ComponentModel.DataAnnotations;

namespace Comestics.Models
{
    public class User
    {
        [Required(AllowEmptyStrings = false,ErrorMessage ="Mời bạn nhập tài khoản")]
        public string Username { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mời bạn nhập mật khẩu")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}