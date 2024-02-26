using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeaShopMVC.Models
{
    [Table("Teas")]
    public class Tea
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int? TypeId { get; set; }
        [Display(Name = "Вид")]
        public TeaType Type { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 50 символов")]
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        [StringLength(500, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 500 символов")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Required]
        [Range(0, 20000, ErrorMessage = "Недопустимая цена")]
        [Display(Name = "Цена")]
        public int Price { get; set; }
        [Required]
        [Range(0, 2000, ErrorMessage = "Недопустимое количество")]
        [Display(Name = "Количество")]
        public int Amount { get; set; }
        [Required]
        [Range(0, 20000, ErrorMessage = "Недопустимый вес")]
        [Display(Name = "Вес (грамм)")]
        public int Weight { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageUrl { get; set; }
    }
}
