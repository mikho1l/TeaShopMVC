using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeaShopMVC.ViewModel
{
    public class Period
    {
        [Required(ErrorMessage ="Пожалуйста, укажите дату")]
        [Display(Name ="Дата начала")]
        public DateTime Start { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите дату")]
        [Display(Name = "Дата конца")]
        public DateTime End { get; set; }
        public List<BestSeller> BestSellers { get; set; }
    }
}
