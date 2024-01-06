using MiniAPI.Data;
using MiniAPI.Models.DTO;
using MiniAPI.Models;
using Microsoft.EntityFrameworkCore;
using MiniAPI.ViewModels;
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
                    Id = Utility.BuildInterestId(interestDTO.Title),
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
        public static IResult PullInterests(ApplicationContext context, int? results, string? search)
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
                if (interests == null || !interests.Any())
                    return Results.NotFound(string.IsNullOrEmpty(search)
                        ? "Error. No interests found."
                        : $"Error. No interests found whose title starts with {search}");

                return Results.Json(interests);
            }
            catch (Exception ex)
            {
                return Utility.ErrorHandling(ex);
            }
        }
    }
}