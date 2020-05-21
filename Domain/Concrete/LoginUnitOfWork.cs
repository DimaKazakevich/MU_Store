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
    public class LoginUnitOfWork : IDisposable, ILoginUnitOfWork
    {
        private GenericRepository<Entities.IdentityUser> _usersRepository;

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

        public void Save()
        {
            //db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                 //   db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
