using Ffitness.Models;

namespace Ffitness.Data
{
    public class SeedSubscriptions
    {
        public static void Seed(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            context.Subscriptions.Add(new Subscription { Duration = Subscription.SubscriptionDuration.Month1, Description = "One month to your goal!", Price = 30 });
            context.Subscriptions.Add(new Subscription { Duration = Subscription.SubscriptionDuration.Month3, Description = "3 months to your goal!", Price = 90 });
            context.Subscriptions.Add(new Subscription { Duration = Subscription.SubscriptionDuration.Month12, Description = "One year to your goal!", Price = 300 }); 

            context.SaveChanges();
        }
    }
}
