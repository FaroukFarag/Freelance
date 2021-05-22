namespace Freelance.Migrations
{
    using Freelance.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Freelance.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Freelance.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Client"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole { Name = "Client" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Freelancer"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole { Name = "Freelancer" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "farouk.farag98@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser 
                {
                    FirstName = "Farouk",
                    LastName = "Farag",
                    PhotoPath = "~/Photos/Farouk210337775.jpg",
                    UserName = "farouk.farag98@gmail.com",
                    Email = "farouk.farag98@gmail.com"
                };

                manager.Create(user, "F22.121998f");
                manager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
