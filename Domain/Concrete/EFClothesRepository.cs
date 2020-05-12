using Domain.Abstract;
using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Concrete
{
    public class EFClothesRepository : IClothesRepository
    {
        private EFDBContext _context = new EFDBContext();

        public IEnumerable<Wear> Clothes => _context.Clothes;

        public IEnumerable<ClothesImages> ClothesImages => _context.ClothesImages;

        public IEnumerable<AspNetUserRoles> AspNetUserRoles => _context.AspNetUserRoles;

        public IEnumerable<IdentityUser> IdentityUser => _context.IdentityUser;

        public IEnumerable<AspNetRoles> AspNetRoles => _context.AspNetRoles;

        public IEnumerable<Size> Sizes => _context.Sizes;
    }
}
