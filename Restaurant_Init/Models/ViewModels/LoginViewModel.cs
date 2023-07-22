using System.ComponentModel.DataAnnotations;
namespace Restaurant_Init.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "請輸入使用者帳號")]
        //顯示名稱
        [Display(Name = "登入帳號")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "請輸入密碼")]
        [Display(Name = "登入密碼")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
