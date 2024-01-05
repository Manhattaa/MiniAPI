using MiniAPI.Data;

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
    }
}
