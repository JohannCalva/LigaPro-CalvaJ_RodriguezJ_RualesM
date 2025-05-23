﻿using LigaPro_CalvaJ_RodriguezJ_RualesM.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace LigaPro_CalvaJ_RodriguezJ_RualesM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<LigaProJJMContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("LigaProJJMContext") ?? throw new InvalidOperationException("Connection string 'LigaProJJMContext' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Agregamos los repositorios
            builder.Services.AddScoped<EquipoRepository>();
            builder.Services.AddScoped<JugadorRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
