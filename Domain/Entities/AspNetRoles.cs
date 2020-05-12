using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class AspNetRoles
    {
        [Key, Column("Id")]
        public string UserId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<AspNetUserRoles> Roles { get; set; }
    }
}
