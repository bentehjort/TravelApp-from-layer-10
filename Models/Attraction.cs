using Seido.Utilities.SeedGenerator;
namespace Models;
public class Attraction : IAttraction, ISeed<Attraction>
{
    public virtual Guid AttractionId{get; set;}
    public virtual string AttractionName {get; set;}
    public virtual string AttractionDescription {get; set;}
    public virtual string AttractionCategory {get; set;}
    public virtual City City {get; set;}
    public virtual List<IReview> Reviews {get; set;} = null;
    public bool Seeded {get; set;} = false;
    public Attraction Seed (SeedGenerator seeder)
    {
        Seeded = true;
        AttractionId = Guid.NewGuid();
        AttractionName = seeder.LatinSentence;
        AttractionDescription = seeder.LatinSentence;
        List<string> categories = new List<string> { "Restaurang", "Café", "Arkitektur", "Museum" };
        AttractionCategory = seeder.FromList<string>(categories);
        return this;
    }
}