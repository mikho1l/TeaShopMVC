using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TeaShopMVC.Models
{
    public class Item
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int OrderId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int TeaId { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "Недопустимое количество")]
        [Display(Name = "Количество")]
        public int Quantity { get; set; }

        public Tea Tea { get; set; }
    }
}
