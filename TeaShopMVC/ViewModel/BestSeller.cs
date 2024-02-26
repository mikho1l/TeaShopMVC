using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeaShopMVC.Models;

namespace TeaShopMVC.ViewModel
{
    public class BestSeller
    {
        public int TeaId { get; set; }
        public int Quantity { get; set; }
        public Tea Tea { get; set; }
    }
}
