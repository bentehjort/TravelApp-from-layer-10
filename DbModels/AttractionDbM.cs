using Models;
using Seido.Utilities.SeedGenerator;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class AttractionDbM : Attraction, ISeed<AttractionDbM>
{
    [Key]
    public override Guid AttractionId { get; set; }
    public Guid CityId { get; set; }
    [NotMapped]
    public override City City { get=> CityDbM; set=> throw new NotImplementedException(); }
    public CityDbM CityDbM { get; set; }
    [NotMapped]
    public override List<IReview> Reviews { get => ReviewDbMs?.ToList<IReview>(); set => throw new NotImplementedException(); }
    public List<ReviewDbM> ReviewDbMs { get; set; }
    public new AttractionDbM Seed(SeedGenerator seeder)
    {
        base.Seed(seeder);
        return this;
    }
}