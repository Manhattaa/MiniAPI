using MiniAPI.Models;

namespace MiniAPI.Handlers
{
    public class Utilities
    {

        //This page is like a settings page. You can call these functions to enable them to an instance of the code.
        //First letters of the First and last names to create an ID
        public static string BuildPersonId(string firstName, string lastName)
        {
            return string.Join(' ', firstName[0], firstName[1], firstName[2], lastName[0], lastName[1], lastName[2]).Trim();
        }

        //first 3 letters of the Interest to create an ID
        public static string BuildInterestId(string title)
        {
            return string.Join(' ', title[0], title[1], title[2]).Trim();
        }

        public static string PullFirstLastName(Person person)
        {
            return $"{person.FirstName} {person.LastName}";
        }

        public static IResult ErrorHandling(Exception ex)
        {
            return Results.Text($"Error Error: {ex.Message}");
        }

    }
}
