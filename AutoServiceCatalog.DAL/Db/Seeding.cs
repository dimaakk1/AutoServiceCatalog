using AutoServiceCatalog.DAL.Entities;
using Bogus;
using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceCatalog.DAL.Db
{
    public static class Seeding
    {
        public static async Task SeedAsync(CarServiceContext context)
        {
            if (context.Categories.Any())
                return; // БД вже заповнена

            var faker = new Faker();

            // ----------------- Categories -----------------
            var categories = new List<Category>
            {
                new Category { Name = "Engine" },
                new Category { Name = "Brakes" },
                new Category { Name = "Electrical" },
                new Category { Name = "Suspension" }
            };
            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();

            // ----------------- Suppliers -----------------
            var suppliers = new Faker<Supplier>()
                .RuleFor(s => s.Name, f => f.Company.CompanyName())
                .RuleFor(s => s.Phone, f => f.Phone.PhoneNumber())
                .Generate(5);

            await context.Suppliers.AddRangeAsync(suppliers);
            await context.SaveChangesAsync();

            // ----------------- Parts -----------------
            var carPartsNames = new[]
            {
                "Brake Pads","Brake Discs","Oil Filter","Air Filter","Fuel Filter",
                "Spark Plug","Battery","Radiator","Shock Absorber","Starter Motor",
                "Alternator","Control Arm Bushing","Ball Joint","Thermostat",
                "Turbocharger","Headlight (Low Beam)","Headlight (High Beam)",
                "ABS Sensor","Timing Belt","Serpentine Belt"
            };

            var parts = new Faker<Part>()
                .RuleFor(p => p.Name, f => f.PickRandom(carPartsNames))
                .RuleFor(p => p.Price, f => Math.Round(f.Random.Decimal(20, 500), 2))
                .RuleFor(p => p.CategoryId, f => f.PickRandom(categories).CategoryId)
                .Generate(10);

            await context.Parts.AddRangeAsync(parts);
            await context.SaveChangesAsync(); // тут PartId автоматично присвоїться

            // ----------------- PartDetails (1:1) -----------------
            var manufacturers = new[] { "Bosch", "Valeo", "Denso", "Delphi", "NGK", "Hella", "Magneti Marelli", "Philips" };
            var warranties = new[] { "6 months", "12 months", "24 months" };

            var details = parts.Select(p => new PartDetail
            {
                Manufacturer = faker.PickRandom(manufacturers),
                Warranty = faker.PickRandom(warranties),
                PartId = p.PartId, // FK автоматично
                Part = p
            }).ToList();

            await context.PartDetails.AddRangeAsync(details);
            await context.SaveChangesAsync();

            // ----------------- PartSuppliers (M:N) -----------------
            var random = new Random();
            var partSuppliers = new List<PartSupplier>();

            foreach (var part in parts)
            {
                var selectedSuppliers = suppliers
                    .OrderBy(s => random.Next())
                    .Take(random.Next(1, 4)) // 1-3 suppliers per part
                    .ToList();

                foreach (var supplier in selectedSuppliers)
                {
                    partSuppliers.Add(new PartSupplier
                    {
                        PartId = part.PartId,
                        SupplierId = supplier.SupplierId
                    });
                }
            }

            await context.Set<PartSupplier>().AddRangeAsync(partSuppliers);
            await context.SaveChangesAsync();
        }
    }
}
