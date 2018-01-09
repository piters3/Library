namespace Library.Migrations
{
    using Library.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            //ContextKey = "Library.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //AddUsersAndRolesFailured(context);
            AddUsersAndRoles(context);
        }

        private static void AddUsersAndRoles(ApplicationDbContext context)
        {
            var adminRole = new IdentityRole { Name = "admin", Id = Guid.NewGuid().ToString() };
            var userRole = new IdentityRole { Name = "user", Id = Guid.NewGuid().ToString() };
            context.Roles.Add(adminRole);
            context.Roles.Add(userRole);

            var hasher = new PasswordHasher();

            var admin = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@admin.com",
                PasswordHash = hasher.HashPassword("admin"),
                EmailConfirmed = true,
                Enabled = true,
                RegisterDate = DateTime.Now,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            admin.Roles.Add(new IdentityUserRole { RoleId = adminRole.Id, UserId = admin.Id });

            context.Users.Add(admin);

            var user = new ApplicationUser
            {
                UserName = "user",
                Email = "user@user.com",
                PasswordHash = hasher.HashPassword("user"),
                EmailConfirmed = true,
                Enabled = true,
                RegisterDate = DateTime.Now,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            user.Roles.Add(new IdentityUserRole { RoleId = userRole.Id, UserId = user.Id });

            context.Users.Add(user);
        }

        private static void AddUsersAndRolesFailured(ApplicationDbContext context)
        {
            if (!context.Roles.Any())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(roleStore);
                var roleAdmin = new IdentityRole { Name = "Admin" };
                var roleUser = new IdentityRole { Name = "user" };

                manager.Create(roleAdmin);
                manager.Create(roleUser);
            }

            if (!context.Users.Any())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new ApplicationUserManager(userStore);

                var admin = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@admin.com",
                    EmailConfirmed = true
                };

                userManager.Create(admin, "admin");
                //userManager.AddToRole(admin.Id, "Admin");

                var user = new ApplicationUser
                {
                    UserName = "user",
                    Email = "user@user.com",
                    EmailConfirmed = true
                };

                userManager.Create(user, "user");
                //userManager.AddToRole(user.Id, "user");
            }
        }
    }
}
