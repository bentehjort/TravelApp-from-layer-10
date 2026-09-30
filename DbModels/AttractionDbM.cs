using Models;
using Models.DTO;
using Seido.Utilities.SeedGenerator;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
public class AttractionDbM : Attraction, ISeed<AttractionDbM>
{
    [Key]
    public override Guid AttractionId { get; set; }
    public Guid? CityId { get; set; }
    [NotMapped]
    public override City City { get => CityDbM; set => throw new NotImplementedException(); }
    [JsonIgnore]
    public CityDbM CityDbM { get; set; }
    [NotMapped]
    public override List<IReview> Reviews { get => ReviewDbMs?.ToList<IReview>(); set => throw new NotImplementedException(); }
    [JsonIgnore]
    public List<ReviewDbM> ReviewDbMs { get; set; }
    public new AttractionDbM Seed(SeedGenerator seeder)
    {
        base.Seed(seeder);
        return this;
    }
    #region Update from DTO
    public AttractionDbM UpdateFromDTO(AttractionCuDto org)
    {
        AttractionName = org.AttractionName;
        AttractionDescription = org.AttractionDescription;
        AttractionCategory = org.AttractionCategory;
        return this;
    }
    #endregion
    #region constructors
    public AttractionDbM() { }
    public AttractionDbM(AttractionCuDto org)
    {
        AttractionId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
    #endregion
}
