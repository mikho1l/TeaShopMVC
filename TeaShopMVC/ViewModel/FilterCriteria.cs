using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeaShopMVC.Models;

namespace TeaShopMVC.ViewModel
{
    public class FilterCriteria
    {
        public string SearchWord { get; set; }
        public int MaxPrice { get; set; }
        public int MinPrice { get; set; }
        public int CategoryID { get; set; }
        public bool InStock { get; set; }
        public int SortOrderId { get; set; }
        public int MaxDefaultPrice { get; set; }
        public int MinDefaultPrice { get; set; }
    }
}
