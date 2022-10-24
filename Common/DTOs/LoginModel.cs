

namespace Common.DTOs
{
    public class LoginModel
    {
        [Required, MaxLength(120), Display(Name = "Enter Username or Email")]
        public string Username { get; set; }

        [Required, MaxLength(256)]
        public string Password { get; set; }
    }
}
