using Domain.Abstract;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class LoginUnitOfWork : ILoginUnitOfWork
    {
        private GenericRepository<Entities.IdentityUser> _usersRepository;

        //private GenericRepository<Entities.AspNetRoles> _aspNetRoles;

        //private GenericRepository<Entities.AspNetUserRoles> _spNetUserRoles;

        public LoginUnitOfWork([Named("Users")] GenericRepository<Entities.IdentityUser> repo)
        {
            this._usersRepository = repo;
        }

        public GenericRepository<Entities.IdentityUser> Users
        {
            get
            {
                return _usersRepository;
            }
        }
    }
}
