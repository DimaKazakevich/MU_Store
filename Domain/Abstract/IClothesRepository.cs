using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Abstract
{
    public interface IClothesRepository
    {
        IEnumerable<Wear> Clothes { get; }
    }
}
