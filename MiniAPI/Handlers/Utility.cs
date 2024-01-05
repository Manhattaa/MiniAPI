using MiniAPI.Models;

namespace MiniAPI.Handlers
{
    public class Utility
    {
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
    }
}
