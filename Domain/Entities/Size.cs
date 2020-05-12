using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Sizes")]
    public class Size
    {
        [Column("sizeId")]
        [Key]
        public int SizeId { get; set; }

        [Column("size")]
        public string SizeName { get; set; }

        public int ClothesId { get; set; }

        [ForeignKey("ClothesId")]
        public virtual Wear Wear { get; set; }
    }
}
