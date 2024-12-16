
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Repositories;
using QuizApp.Services;

namespace QuizApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<QuizAppDbContext>(options => options.UseSqlServer(connString));




            // Add services to the container.

            builder.Services.AddScoped<IUserRepository, UserRepository>(); // Register UserRepository
            builder.Services.AddScoped<IUserService, UserService>(); // Register UserService

            builder.Services.AddControllers();

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
