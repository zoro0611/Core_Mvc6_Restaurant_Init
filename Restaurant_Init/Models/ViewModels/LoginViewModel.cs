using System.ComponentModel.DataAnnotations;
namespace Restaurant_Init.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "請輸入使用者名稱")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "請輸入密碼")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
