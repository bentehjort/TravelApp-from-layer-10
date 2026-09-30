namespace Models.DTO;

//Creating a DTO to use for returning more readable Attraction data to client
public class AttractionDto
{
    public virtual Guid AttractionId { get; set; }
    public virtual string AttractionName { get; set; }
    public virtual string AttractionDescription { get; set; }
    public virtual string AttractionCategory { get; set; }
    public virtual Guid? CityId { get; set; }
    public virtual string CityName { get; set; }
    public virtual Guid? CountryId { get; set; }
    public virtual string CountryName { get; set; }
    public virtual List<ReviewDto> Reviews {get; set;}

    public AttractionDto() { }

    public AttractionDto(IAttraction org)
    {
        AttractionId = org.AttractionId;
        AttractionName = org.AttractionName;
        AttractionDescription = org.AttractionDescription;
        AttractionCategory = org.AttractionCategory;
        CityId = org.City?.CityId;
        CityName = org.City?.CityName;
        CountryId = org.City?.Country?.CountryId;
        CountryName = org.City?.Country?.CountryName;
        Reviews = org.Reviews?.Select(r=> new ReviewDto(r)).ToList();
    }
}

