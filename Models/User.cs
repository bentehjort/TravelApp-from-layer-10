using Seido.Utilities.SeedGenerator;
namespace Models;
public class User : IUser, ISeed<User>
{
    public virtual Guid UserId {get; set;}
    public virtual string UserName {get; set;}
    public virtual List<IReview> Reviews {get; set;} = null;
    public bool Seeded {get; set;} = false;
    public User Seed (SeedGenerator seeder)
    {
        Seeded = true;
        UserId = Guid.NewGuid();
        UserName = seeder.FullName;
        return this;
    }
}