namespace Models.DTO;

public class DatabaseCountedDto
{
    public int NrSeededAttractions {get; set;} = 0;
    public int NrUnseededAttractions {get; set;} = 0;
    public int NrSeededCountries {get; set;} = 0;
    public int NrUnseededCountries {get; set;} = 0;
    public int NrSeededCities {get; set;} = 0;
    public int NrUnseededCities {get; set;} = 0;
    public int NrSeededReviews {get; set;} = 0;
    public int NrUnseededReviews {get; set;} = 0;
    public int NrSeededUsers {get; set;} = 0;
    public int NrUnseededUsers {get; set;} = 0;
}