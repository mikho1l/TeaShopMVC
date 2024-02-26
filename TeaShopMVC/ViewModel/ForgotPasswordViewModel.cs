using System.ComponentModel.DataAnnotations;

namespace TeaShopMVC.ViewModel
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
