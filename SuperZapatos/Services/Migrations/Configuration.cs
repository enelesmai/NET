namespace Services.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Services.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Services.Models.ManagerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Services.Models.ManagerContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //

            List<Article> a1 = new List<Article>();
            a1.Add(new Article { id = 1, name = "Zapato Escolar de niño", description = "Zapato escolar de larga duración", price = 200, total_in_shelf = 10, total_in_vault = 2 });
            a1.Add(new Article { id = 4, name = "Zapato Militar", description = "Zapato escolar de larga duración", price = 580, total_in_shelf = 18, total_in_vault = 4 });
            List<Article> a2 = new List<Article>();
            a2.Add(new Article { id = 2, name = "Zapato Escolar de niña", description = "Zapato escolar de larga duración", price = 220, total_in_shelf = 12, total_in_vault = 7 });
            a2.Add(new Article { id = 5, name = "Bota de excusión", description = "Zapato escolar de larga duración", price = 600, total_in_shelf = 14, total_in_vault = 9 });

            List<Article> a3 = new List<Article>();
            a3.Add(new Article { id = 3, name = "Zapato de Enfermería", description = "Color blanco, charol", price = 350, total_in_shelf = 16, total_in_vault = 8 });

            context.Stores.AddOrUpdate(
                s => s.id,
                new Store { id = 1, name = "Store 1", address = "Calle 123", Articles = a1 },
                new Store { id = 1, name = "Store 2", address = "Calle 456", Articles = a2 },
                new Store { id = 1, name = "Store 3", address = "Calle 789", Articles = a3 }
                );

            //
        }
    }
}
