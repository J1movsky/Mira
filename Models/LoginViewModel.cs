using System.ComponentModel.DataAnnotations;

namespace Mira.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Required]
        [UIHint("Password")]
        [Display(Name = "Пороль")]
        public string Password { get; set; }

        [Display (Name = "Запомнить меня?")]
        public bool RememberMe { get; set;}
    }
}
