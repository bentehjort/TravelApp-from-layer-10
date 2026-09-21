namespace Models;
public interface ICountry
{
    public Guid CountryId {get; set;}
    public string CountryName {get; set;}
    public List<ICity> Cities {get; set;}
}
public interface ICity
{
    public Guid CityId {get; set;}
    public string CityName {get; set;}
    public Country Country {get; set;}
    public List<IAttraction> Attractions {get; set;}
}
public interface IAttraction
{
    public Guid AttractionId {get; set;}
    public string AttractionName {get; set;}
    public string AttractionDescription {get; set;}
    public string AttractionCategory {get; set;}
    public City City {get; set;}
    public List<IReview> Reviews {get; set;}
}
public interface IReview
{
    public Guid ReviewId {get; set;}
    public string ReviewComment {get; set;}
    public int ReviewRating {get; set;}
    public Attraction Attraction {get; set;}
    public User User {get; set;}
}
public interface IUser
{
    public Guid UserId {get; set;}
    public string UserName {get; set;}
    public List<IReview> Reviews {get; set;}
}