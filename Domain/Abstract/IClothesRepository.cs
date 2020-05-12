using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Abstract
{
    public interface IClothesRepository
    {
        IEnumerable<Wear> Clothes { get; }
        IEnumerable<ClothesImages> ClothesImages { get; }
        IEnumerable<AspNetUserRoles> AspNetUserRoles { get; }
        IEnumerable<IdentityUser> IdentityUser { get; }
        IEnumerable<AspNetRoles> AspNetRoles { get; }
        IEnumerable<Size> Sizes { get; }
    }
}