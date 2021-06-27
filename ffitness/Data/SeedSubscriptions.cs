using Ffitness.Models;

namespace Ffitness.Data
{
    public class SeedSubscriptions
    {
        public static void Seed(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            context.Subscriptions.Add(new Subscription { Duration = Subscription.SubscriptionDuration.Month1, Price = 30 });
            context.Subscriptions.Add(new Subscription { Duration = Subscription.SubscriptionDuration.Month12, Price = 300 });
            context.Subscriptions.Add(new Subscription { Duration = Subscription.SubscriptionDuration.Month3, Price = 90 });

            context.SaveChanges();
        }
    }
}
