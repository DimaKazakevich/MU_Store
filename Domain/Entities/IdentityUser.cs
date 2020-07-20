using Microsoft.AspNet.Identity;
using System;
using System.Collections;
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

        ///

        public virtual int AccessFailedCount { get; set; }

        public virtual ICollection Claims { get; }

        public virtual bool LockoutEnabled { get; set; }

        public virtual DateTime? LockoutEndDateUtc { get; set; }

        public virtual ICollection Logins { get; }

        public virtual string PhoneNumber { get; set; }

        public virtual bool PhoneNumberConfirmed { get; set; }

        public virtual bool TwoFactorEnabled { get; set; }

        public virtual ICollection<ShippingDetails> ShippingDetails { get; set; }
    }
}