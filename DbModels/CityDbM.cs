using Models;
using Seido.Utilities.SeedGenerator;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
public class CityDbM : City, ISeed<CityDbM>
{
    [Key]
    public override Guid CityId { get; set; }
    public Guid CountryId { get; set; }
    [NotMapped]
    public override Country Country {get => CountryDbM; set=> throw new NotImplementedException();}
    [JsonIgnore]
    public CountryDbM CountryDbM {get; set;}
    [NotMapped]
    public override List<IAttraction> Attractions { get => base.Attractions; set => base.Attractions = value; }
    
    [JsonIgnore]
    public List<AttractionDbM> AttractionDbMs { get; set; }
    public new CityDbM Seed(SeedGenerator seeder)
    {
        base.Seed(seeder);
        return this;
    }
}