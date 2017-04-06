namespace Cicero.Persistence.Migrations
{
    using Core.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Cicero.Persistence.Cicerov2Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Cicero.Persistence.Cicerov2Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Roles.AddOrUpdate(role => role.CodeName,
                new Role("administrator", "Administrator"),
                new Role("user", "User"));

            context.Users.AddOrUpdate(user => user.UserName,
                new User("Admin", "Bert", "Limerkens", "bert.limerkens@gmail.com", "dfdfdf"));

            context.SaveChanges();
        }
    }
}
