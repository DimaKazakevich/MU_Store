using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("AspNetUsers")]
    public class IdentityUser
    {
        public virtual string Email { get; set; }

        public virtual bool EmailConfirmed { get; set; }

        [Key, Column("Id")]
        public virtual string UserId { get; set; }

        public virtual string PasswordHash { get; set; }

        public virtual ICollection<AspNetUserRoles> Roles { get; set; }

        public virtual string SecurityStamp { get; set; }
 
        public virtual string UserName { get; set; }
    }
}