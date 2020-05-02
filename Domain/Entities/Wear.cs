using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Clothes")]
    public class Wear
    {
        [Key]
        public int Article { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<ClothesImages> Images { get; set; }
        public virtual ICollection<Size> Sizes { get; set; }
    }
}