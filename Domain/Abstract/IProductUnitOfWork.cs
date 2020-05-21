using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IProductUnitOfWork
    {
        GenericRepository<Size> Sizes { get; }
        GenericRepository<Image> Images { get; }
        GenericRepository<Product> Products { get; }
    }
}
