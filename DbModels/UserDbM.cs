using Models;
using Seido.Utilities.SeedGenerator;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class UserDbM : User, ISeed<UserDbM>
{
    [Key]
    public override Guid UserId {get; set;}
    [NotMapped]
    public override List<IReview> Reviews {get => ReviewDbMs?.ToList<IReview>(); set => throw new NotImplementedException();}
    public List<ReviewDbM> ReviewDbMs {get; set;}
    public new UserDbM Seed(SeedGenerator seeder)
    {
        base.Seed(seeder);
        return this;
    }
}