using Models;
using Seido.Utilities.SeedGenerator;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class CountryDbM : Country, ISeed<CountryDbM>
{
    [Key]
    public override Guid CountryId {get; set;}
    [NotMapped]
    public override List<ICity> Cities {get => CityDbMs?.ToList<ICity>(); set => throw new NotImplementedException();}
    [JsonIgnore]
    public List<CityDbM> CityDbMs {get; set;}
    public new CountryDbM Seed(SeedGenerator seeder)
    {
        base.Seed(seeder);
        return this;
    }
}