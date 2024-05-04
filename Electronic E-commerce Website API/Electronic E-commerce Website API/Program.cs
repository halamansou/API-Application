
using Electronic_E_commerce_Website_API.Models;
using Electronic_E_commerce_Website_API.Repository;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Electronic_E_commerce_Website_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string txt = "";

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

          

            builder.Services.AddDbContext<ECommerceApiContext>(op => op.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("ecommerce")));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(txt,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

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

            app.UseCors(txt);

            app.MapControllers();

            app.Run();
        }
    }
}
