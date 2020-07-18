using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Comestics.Areas.Admin.Data
{
    public class UpdateProductModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Tên sản phẩm")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Bạn hãy xem lại số lượng ký tự nhập vào")]
        public string Name { get; set; }
        public string Image { get; set; }
        public int Category { get; set; }
        [Required]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }
        public int Price { get; set; }
        public int Amounts { get; set; }
    }
}