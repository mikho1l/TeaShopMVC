using System.Collections.Generic;

namespace TeaShopMVC.Models
{
    public enum SortOrder
    {
        NameAsc,//по имени по возрастанию
        NameDesc,//по имени по убыванию
        PriceAsc,//по цене
        PriceDesc,
        TypeAsc,//по виду
        TypeDesc,
        WeightAsc,//по весу
        WeightDesc,
        AmountAsc,
        AmountDesc
    }
    public class SortOrderClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SortOrder SortOrder { get; set; }
        public static List<SortOrderClass> GetAllSortings()
        {
            var list = new List<SortOrderClass>();
            list.Add(new SortOrderClass { Id = 1, Name = "Сначала дешевле", SortOrder = SortOrder.PriceAsc });
            list.Add(new SortOrderClass { Id = 2, Name = "Сначала дороже", SortOrder = SortOrder.PriceDesc });
            return list;
        }
    }
}
