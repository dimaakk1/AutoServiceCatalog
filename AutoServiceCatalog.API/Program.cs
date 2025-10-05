
using AutoServiceCatalog.DAL.Db;
using AutoServiceCatalog.DAL.Repositories.Intarfaces;
using AutoServiceCatalog.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using AutoServiceCatalog.BLL.Automapper;
using Microsoft.Extensions.DependencyInjection;
using AutoServiceCatalog.BLL.Services.Interfaces;
using AutoServiceCatalog.BLL.Services;
using AutoServiceCatalog.DAL.UOW;
namespace AutoServiceCatalog.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<CarServiceContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IPartRepository, PartRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
            builder.Services.AddScoped<IPartDetailRepository, PartDetailRepository>();
            builder.Services.AddScoped<IPartSupplierRepository, PartSupplierRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IPartService, PartService>();
            builder.Services.AddScoped<ISupplierService, SupplierService>();
            builder.Services.AddScoped<IPartDetailService, PartDetailService>();
            builder.Services.AddScoped<IPartSupplierService, PartSupplierService>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


            builder.Services.AddAutoMapper(typeof(MappingProfile));


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CarServiceContext>();
                context.Database.Migrate();
                Seeding.SeedAsync(context).GetAwaiter().GetResult();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
