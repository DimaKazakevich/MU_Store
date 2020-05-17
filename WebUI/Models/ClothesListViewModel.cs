using Domain.Entities;
using System;
using System.Collections.Generic;


namespace WebUI.Models
{
    public class ClothesListViewModel
    {
        public IEnumerable<Product> Clothes { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public string CurrentCategory { get; set; }
    }
}