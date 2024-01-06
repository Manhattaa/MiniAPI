 using Microsoft.EntityFrameworkCore;
using MiniAPI.Handlers;
using MiniAPI.Data;

namespace MiniAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connectionString = builder.Configuration.GetConnectionString("ApplicationContext");
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));

            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");


            //Post 
            app.MapPost("/interests", InterestHandler.PushInterests);
            app.MapPost("/people/{personId}/interests/{interestId}/links/", InterestHandler.PushInterestLinks);


            //Gets

            app.Run();
        }
    }
}
