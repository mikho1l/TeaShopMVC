using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeaShopMVC.Models
{
    [Table("TeaTypes")]
    public class TeaType
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; } // ID 
        [Required(ErrorMessage = "Пожалуйста введите название вида")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [Display(Name = "Вид")]
        public string Name { get; set; } // название 
    }
}
