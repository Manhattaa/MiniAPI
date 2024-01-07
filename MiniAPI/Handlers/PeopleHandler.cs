using MiniAPI.Data;
using MiniAPI.Models.DTO;

namespace MiniAPI.Handlers
{
    public class PeopleHandler
    {
        //POST -- Here we handle the POST commands. This is to ADD people to our database.

        public static IResult PushPerson(ApplicationContext context, PersonDTO personDTO)
        {
            try
            {
                Person person = new Person()
                {
                    Id = Utilities.BuildPersonId(personDTO.FirstName, personDTO.LastName),
                    FirstName = personDTO.FirstName
                    LastName = personDTO.LastName
                    PhoneNumber = personDTO.PhoneNumber
                };
                context.People.Add(person);
                context.SaveChanges();
                return Results.Ok($"Person {Utilities.PullFirstLastName(person)} has been added.");
            }
            catch (Exception ex)
            {

                return Utilities.ErrorHandling(ex);
            }
        }
    }
}
