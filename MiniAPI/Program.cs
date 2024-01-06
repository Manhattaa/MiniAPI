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


            //Post - Add new content to our databases
            app.MapPost("/interests", InterestHandler.PushInterests); //Add new Interests
            app.MapPost("/people/{personId}/interests/{interestId}/links/", InterestHandler.PushInterestLinks); //Add new InterestLinks!


            //Get - Pull out the data from the database
            app.MapGet("/people/{personId}/interests/{search?}", InterestHandler.PullInterestsForPeople); //Pull out Interests for the People in the Database.
            app.MapGet("/interests/{search?}", InterestHandler.PullInterests);
            app.MapGet("/interests/page/{page?}/results/{results?}/{search?}", InterestHandler.PullInterests);

            app.Run();
        }
    }
}
