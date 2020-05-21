using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class UserRepository : GenericRepository<IdentityUser>
    {
        public UserRepository(DbContext context) : base(context)
        {
            _entities = context;
        }
    }
}
