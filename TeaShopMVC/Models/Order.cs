using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TeaShopMVC.Models
{
    public class Order
    {
        [HiddenInput(DisplayValue = false)]// ID покупки
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]

        [Required]
        [Range(1, 50000, ErrorMessage = "Недопустимая сумма")]
        [Display(Name = "Общая стоимость")]
        public int TotalPrice { get; set; }

        public DateTime Date { get; set; } // дата покупки
        public IEnumerable<Item> Items { get; set; }

        [Required(ErrorMessage = "Пожалуйста введите фамилию")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 50 символов")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Пожалуйста введите свое имя")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 50 символов")]
        [Display(Name = "Имя")]
        public string Name { get; set; }
[Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Номер телефона")]
        public string DeliveryMethod { get; set; }

        public string Status { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ClientId { get; set; }
    }
}
