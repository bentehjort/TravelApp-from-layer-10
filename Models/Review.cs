using Seido.Utilities.SeedGenerator;
namespace Models;
public class Review : IReview, ISeed<Review>
{
    public virtual Guid ReviewId {get; set;}
    public virtual string ReviewComment {get; set;}
    public virtual int ReviewRating {get; set;}
    public virtual Attraction Attraction {get; set;}
    public virtual User User {get; set;}
    public bool Seeded {get; set;} = false;
    public Review Seed (SeedGenerator seeder)
    {
        Seeded = true;
        ReviewId = Guid.NewGuid();
        ReviewComment = seeder.LatinSentence;
        ReviewRating = seeder.Next(1, 6);
        return this;
    }
}