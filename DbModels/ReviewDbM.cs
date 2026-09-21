using Models;
using Seido.Utilities.SeedGenerator;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class ReviewDbM : Review, ISeed<ReviewDbM>
{
    [Key]
    public override Guid ReviewId {get; set;}
    public Guid AttractionId {get; set;}
    public Guid UserId {get; set;}
    [NotMapped]
    public override Attraction Attraction {get=> AttractionDbM; set=> throw new NotImplementedException();}

    public AttractionDbM AttractionDbM {get; set;}
    [NotMapped]
    public override User User {get=> UserDbM; set=> throw new NotImplementedException();}
    [ForeignKey("UserId")]
    public UserDbM UserDbM {get; set;}
    public new ReviewDbM Seed(SeedGenerator seeder)
    {
        base.Seed(seeder);
        return this;
    }
}