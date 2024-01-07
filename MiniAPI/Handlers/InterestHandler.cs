using MiniAPI.Data;
using MiniAPI.Models.DTO;
using MiniAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using MiniAPI.Models.ViewModels;

namespace MiniAPI.Handlers
{
    public class InterestHandler
    {
        //FOR POST - So we can add Interests to the database
        public static IResult PushInterests(ApplicationContext context, string name, InterestDTO interestDTO)
        {
            try
            {
                Interest interest = new Interest()
                {
                    Id = Utilities.BuildInterestId(interestDTO.Title),
                    Title = interestDTO.Title,
                    Description = interestDTO.Description,
                };

                context.Interests.Add(interest);
                context.SaveChanges();

                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Results.Text($"Error! Error! {ex.Message}");
            }
        }

        public static IResult PushInterestLinks(ApplicationContext context, string personId, string interestId, Link2InterestDTO link2Interest)
        {
            try
            { //Checking if the person in question even exists
                if (!DbHelper.PersonExists(context, personId))
                    return Results.NotFound($"Error! Error! The Person: {personId} was not found.");

                //Checking if the interest in question even exists
                if (!DbHelper.InterestExists(context, interestId))
                    return Results.NotFound($"Error! Error! The Interest: {interestId} was not found.");

                //Pull data regarding people and interests from the Database.
                Person person = context.People
                    .Where(p => p.Id == personId)
                    .Single();


                Interest interest = context.Interests
                    .Where(i => i.Id == interestId)
                    .Single();
                // Add link and the objects to the InterestLink table
                context.LinksInterests
                    .Add(new Link2Interests()
                    {
                        Url = link2Interest.Url,
                        Person = person,
                        Interests = interest

                    });
                context.SaveChanges();

                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Results.Text($"An error occurred: {ex.Message}");
            }
        }



        //GET -- Here we pull the data from the database and viewmodels for the User to see.
        //Here we pull the interests from the Database.
        public static IResult PullInterests(ApplicationContext context, int? results, string? search, int? page)
        {
            try
            {
                //PULL ALL INTERESTS
                List<InterestViewModel> interests = context.Interests
                .Select(p => new InterestViewModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                }).ToList();


                if (!string.IsNullOrEmpty(search))
                {
                    interests = interests
                        .Where(i => i.Title.ToLower()
                        .StartsWith(search.ToLower()))
                        .ToList();
                }
                interests = Pagination(interests, page, results);


                if (interests == null || !interests.Any())
                    return Results.NotFound(string.IsNullOrEmpty(search)
                        ? $"Error 404. No interests found with a title that starts with {search}"
                        : "Error 404: No valid interests found.");

                return Results.Json(interests);
            }
            catch (Exception ex)
            {
                return Utilities.ErrorHandling(ex);
            }
        }
        public static IResult PullInterestsForPeople(ApplicationContext context, string personId, string? search)
        {
            try
            {
                if (!DbHelper.PersonExists(context, personId))
                    return Results.NotFound($"Error 404. The person {personId} was not found.");

                Person person = DbHelper.PullPeopleInterests(context, personId);

                List<InterestPersonViewModel> peopleInterest =
               person.Interests
               .Select(i => new InterestPersonViewModel()
               {
                   Title = i.Title,
                   Description = i.Description,
               })
               .ToList();

                //if a search was created

                if (!string.IsNullOrEmpty(search))
                {
                    peopleInterest = peopleInterest
                        .Where(i => i.Title.ToLower().StartsWith(search.ToLower())).ToList();
                }

                return Results.Json(peopleInterest);
            }
            catch (Exception ex)
            {
                return Utilities.ErrorHandling(ex);
            }
        }

        private static List<InterestViewModel> Pagination(List<InterestViewModel> interests, int? page, int? results)
        {
            if (page == null)
                page = 1;

            if (results == null)
                results = interests.Count();

            int skip = (int)((page - 1) * results);
            int take = (int)results;

            List<InterestViewModel> paginateInterests =
                interests
                .Skip(skip)
                .Take(take)
                .ToList();

            return paginateInterests;
        }
    }
}