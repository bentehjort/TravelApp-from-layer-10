using Models;
using Models.DTO;
using Seido.Utilities.SeedGenerator;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
public class ReviewDbM : Review, ISeed<ReviewDbM>
{
    [Key]
    public override Guid ReviewId { get; set; }
    [Range(1, 5)]
    public override int ReviewRating { get; set; }
    public Guid AttractionId { get; set; }
    public Guid UserId { get; set; }
    [NotMapped]
    public override Attraction Attraction { get => AttractionDbM; set => throw new NotImplementedException(); }
    [JsonIgnore]
    public AttractionDbM AttractionDbM { get; set; }
    [NotMapped]
    public override User User { get => UserDbM; set => throw new NotImplementedException(); }
    [JsonIgnore]
    public UserDbM UserDbM { get; set; }
    public new ReviewDbM Seed(SeedGenerator seeder)
    {
        base.Seed(seeder);
        return this;
    }
    #region Update from DTO
    public ReviewDbM UpdateFromDTO(ReviewCuDto org)
    {
        ReviewComment = org.ReviewComment;
        ReviewRating = org.ReviewRating;
        return this;
    }
    #endregion
    #region constructors
    public ReviewDbM() { }
    public ReviewDbM(ReviewCuDto org)
    {
        ReviewId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
    #endregion
}