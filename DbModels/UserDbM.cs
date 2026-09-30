using Models;
using Seido.Utilities.SeedGenerator;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Models.DTO;

public class UserDbM : User, ISeed<UserDbM>
{
    [Key]
    public override Guid UserId {get; set;}
    [NotMapped]
    public override List<IReview> Reviews {get => ReviewDbMs?.ToList<IReview>(); set => throw new NotImplementedException();}
    
    [JsonIgnore]
    public List<ReviewDbM> ReviewDbMs {get; set;}
    public new UserDbM Seed(SeedGenerator seeder)
    {
        base.Seed(seeder);
        return this;
    }
     #region Update from DTO
    public UserDbM UpdateFromDTO(UserCuDto org)
    {
        UserName = org.UserName;
        return this;
    }
    #endregion
    #region constructors
    public UserDbM() { }
    public UserDbM(UserCuDto org)
    {
        UserId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
    #endregion
}