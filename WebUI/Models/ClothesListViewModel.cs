using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebUI.Models
{
    public class ClothesListViewModel
    {
        public IEnumerable<Product> Clothes { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public string CurrentCategory { get; set; }

        public IEnumerable<string> GetAllSizes(IEnumerable<ICollection<Size>> sizes)
        {
            List<string> size = new List<string>();
            foreach(var item in sizes)
            {
                foreach(var item1 in item)
                {
                    size.Add(item1.SizeName);
                }
            }

            return size.Distinct();
        }
    }
}