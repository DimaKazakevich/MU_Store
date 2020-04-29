﻿using Microsoft.AspNet.Identity.EntityFramework;

namespace WebUI.Models
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext() : base("EFDBContext") { }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
    }
}