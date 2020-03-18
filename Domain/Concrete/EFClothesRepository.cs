using Domain.Abstract;
using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Concrete
{
    public class EFClothesRepository : IClothesRepository
    {
        private EFDBContext _context = new EFDBContext();

        public IEnumerable<Wear> Clothes
        {
            get { return _context.Clothes; }
        }
    }
}
