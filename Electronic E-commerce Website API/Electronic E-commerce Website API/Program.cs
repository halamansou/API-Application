using Electronic_E_commerce_Website_API.Models;
using Electronic_E_commerce_Website_API.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Electronic_E_commerce_Website_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Add DbContext and connection string configuration
            builder.Services.AddDbContext<ECommerceContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ecommerce")));

            builder.Services.AddScoped<GenericRepository<User>>();
            builder.Services.AddScoped<GenericRepository<Product>>();
            builder.Services.AddScoped<GenericRepository<Order>>();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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
