using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class WearSizesViewModel
    {
        public IEnumerable<string> Sizes { get; set; }

        public int ClothesID { get; set; }
    }
}