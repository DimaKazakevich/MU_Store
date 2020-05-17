using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("ClothesImages")]
    public class Image
    {
        [Key]
        public int ClothesImagesID { get; set; }
        public byte[] ImageFile { get; set; }

        public int ClothesId { get; set; }

        [ForeignKey("ClothesId")]
        public virtual Product Wear { get; set; }
    }
}