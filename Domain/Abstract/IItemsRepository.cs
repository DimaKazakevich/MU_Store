using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Abstract
{
    public interface IItemsRepository
    {
        IEnumerable<Product> Clothes { get; }
        IEnumerable<Image> ClothesImages { get; }
        IEnumerable<AspNetUserRoles> AspNetUserRoles { get; }
        IEnumerable<IdentityUser> IdentityUser { get; }
        IEnumerable<AspNetRoles> AspNetRoles { get; }
        IEnumerable<Size> Sizes { get; }

        void SaveChanges();
        void Add(Product wear);
        void Add(Image image);
    }
}