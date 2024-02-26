using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeaShopMVC.Models;

namespace TeaShopMVC.ViewModel
{
    public class Filter
    {
        public FilterCriteria Criteria { get; set; }
        public SelectList Categories { get; set; }
        public SelectList SortOrder { get; set; }
        public IEnumerable<Tea> Teas {get;set;}
    }
}
