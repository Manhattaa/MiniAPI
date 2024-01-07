 using Microsoft.EntityFrameworkCore;
using MiniAPI.Handlers;
using MiniAPI.Data;
using MiniAPI.Sounds;
using System.Runtime.CompilerServices;

namespace MiniAPI
{
    class Program
    {
        static async Task Main()
        {

            var builder = WebApplication.CreateBuilder();
            string connectionString = builder.Configuration.GetConnectionString("ApplicationContext");
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));

            var app = builder.Build();

            app.MapGet("/", () => "Hello and welcome to this API.!\n\n" +
            "Please enjoy this wonderful background music as you scroll\n\n" +
            "The following GET and POST commands are used to navigate through this API \n" +
            "I would also recommend to use an application such as Swagger or Insomnia. +\n\n" +

            "GET COMMANDS:\n" +
            "/interests\n" +
            "/interests/{search?}\n" +
            "/people\n" +
            "/people/{search?}\n" +
            "/people/{personId}/interests\n" +
            "/people/{personId}/interests/links\n\n" +

            "PUSH COMMANDS: \n" +
            "/interests - Add a new interest\n" +
            "/people/ - Add a new student\n" +
            "/people/{personId}/interests/{interestId}" +
            "/people/{personId}/interests/{interestId}/links/");




            //Post - Add new content to our databases
            app.MapPost("/interests", InterestHandler.PushInterests); //Add new Interests
            app.MapPost("/people/{personId}/interests/{interestId}/links/", InterestHandler.PushInterestLinks); //Add new InterestLinks!
            app.MapPost("/people", PeopleHandler.PushPerson); //add new people to the database
            app.MapPost("/people/{personId}/interests/{interestId}", PeopleHandler.PeopleToInterests);


            //Get - Pull out the data from the database
            app.MapGet("/people/{personId}/interests/{search?}", InterestHandler.PullInterestsForPeople); //Pull out Interests for the People in the Database.
            app.MapGet("/interests/{search?}", InterestHandler.PullInterests);
            app.MapGet("/interests/page/{page?}/results/{results?}/{search?}", InterestHandler.PullInterests);
            app.MapGet("/people/{search?}", PeopleHandler.PullPeople); //Pull down people from the Database
            app.MapGet("/people/page/{page?}/results/{results?}/{search?}", PeopleHandler.PullPeople);
            app.MapGet("/people/{personId}/interests/links", PeopleHandler.PullLinkForPeople);

            await Sounds.Sounds.PlaySoundAsync("elevatormusic.wav");

            app.Run();
        }
    }
}
