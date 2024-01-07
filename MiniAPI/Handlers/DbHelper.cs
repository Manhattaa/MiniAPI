using Microsoft.EntityFrameworkCore;
using MiniAPI.Data;
using MiniAPI.Models;

namespace MiniAPI.Handlers
{
    public class DbHelper
    {
        public static bool PersonExists(ApplicationContext context, string id)
        {
            return context.People.Any(p => p.Id == id);
        }

        public static bool InterestExists(ApplicationContext context, string id)
        {
            return context.Interests.Any(p => p.Id == id);
        }


        public static Interest PullInterest (ApplicationContext context, string interestId)
        {
            Interest interest = context.Interests
                .Where(i => i.Id == interestId)
                .Single();
            return interest;
        }

        public static Person PullPeopleInterests(ApplicationContext context, string personId)
        {
            Person person = context.People
                .Include(p => p.Interests)
                .Where(p => p.Id == personId)
                .Single();

            return person;
        }
    }
}
