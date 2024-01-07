using MiniAPI.Data;
using MiniAPI.Models.DTO;
using MiniAPI.Models;
using MiniAPI.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MiniAPI.Handlers;

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
                    FirstName = personDTO.FirstName,
                    LastName = personDTO.LastName,
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
        public static IResult PeopleToInterests(ApplicationContext context, string personId, string interestId) 
        {
            try
            {
                //check if both entities even exists
                if (!DbHelper.PersonExists(context, personId))
                    return Results.NotFound($"Error 404: The human known as {personId} does not exist");

                if (!DbHelper.InterestExists(context, personId))
                    return Results.NotFound($"Error 404: The interest: {interestId} was not found.");


                Person person = DbHelper.PullPeopleInterests(context, personId);
                Interest interest = DbHelper.PullInterest(context, interestId);

                person.Interests
                .Add(interest);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                return Utilities.ErrorHandling(ex);
            }
        }
        //GET -- Here we handle GET commands. This is to PULL people from our Database
        public static IResult PullPeople(ApplicationContext context, string? search, int? page, int? results)
        {
            try
            {
                //PULL ALL PPL
                List<PeopleViewModel> people = context.People
                .Select(p => new PeopleViewModel()
                {
                    Id = p.Id,
                    Name = $"{p.FirstName} {p.LastName}"
                })
                .ToList();

                if (!string.IsNullOrEmpty(search))
                    people = Search(context, people, search);

                people = Pagination(people, page, results);

                //Errorhandling
                if (people == null || !people.Any())
                    return Results.NotFound(string.IsNullOrEmpty(search)
                        ? $"Error 404. No person was found with a name that starts with {search}"
                        : "Error 404: No humans were found with that name.");
                return Results.Json(people);

            }
            catch (Exception ex)
            {
                return Utilities.ErrorHandling(ex);
            }
        }
        public static IResult PullLinkForPeople(ApplicationContext context, string personId)
        {
            try
            {
                //check if person exists in Database
                if (!DbHelper.PersonExists(context, personId))
                    return Results.NotFound($"Error 404: The human known as {personId} does not exist...yet");

                List<InterestLinkViewModel> link2Interests =
                    context.Links2Interests
                    .Where(il => il.Person.Id == personId)
                    .Select(il => new InterestLinkViewModel
                    {
                        Url = il.Url
                    })
                    .ToList();

                return Results.Json(link2Interests);
            }
            catch (Exception ex)
            {

                return Utilities.ErrorHandling(ex);
            }
        }
        private static List<PeopleViewModel> Pagination(List<PeopleViewModel> people, int? page, int? results)
        {
            if (page == null)
                page = 1;

            if (results == null)
                results = people.Count();

            int skip = (int)((page - 1) * results);
            int take = (int)results;

            List<PeopleViewModel> Paginate =
                people
                .Skip(skip)
                .Take(take)
                .ToList();

            return Paginate;
        }
        private static List<PeopleViewModel> Search(ApplicationContext context, List<PeopleViewModel> people, string? search)
        {
            people = people
            .Where(p => p.Name.ToLower()
            .StartsWith(search.ToLower()))
            .ToList();

            return people;
        }
    }
}
