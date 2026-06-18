using System.ComponentModel.DataAnnotations;

namespace HelloRazorPage.DbModels
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите логин")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; } = string.Empty;
    }
}