
using car_management_backend.Context;
using car_management_backend.Models;
using car_management_backend.Repository.Implementations;
using car_management_backend.Repository.Interfaces;
using car_management_backend.Service.Implementations;
using car_management_backend.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace car_management_backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IGarageRepository, GarageRepository>();
            builder.Services.AddScoped<IGarageService, GarageService>();
            builder.Services.AddScoped<ICarRepository, CarRepository>();
            builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddScoped<ICarGarageRepository, CarGarageRepository>();
            builder.Services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();
            builder.Services.AddScoped<IMaintenanceService, MaintenanceService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins("http://localhost:3000") // Add your React app's URL
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddControllers();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowReactApp");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
