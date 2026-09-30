using System.ComponentModel.DataAnnotations;

namespace Models.DTO;


public class AttractionCuDto
{
    public virtual Guid? AttractionId { get; set; }
    public virtual string AttractionName { get; set; }
    public virtual string AttractionDescription { get; set; }
    public virtual string AttractionCategory { get; set; }
    public virtual Guid? CityId { get; set; }
    public virtual string CityName { get; set; }
    public virtual Guid? CountryId { get; set; }
    public virtual string CountryName { get; set; }

    public AttractionCuDto() { }

//Mapping IAttraction to AttractionDto. Decided to use this DTO for all CRUD methods, even though it might not be
//Best practice for reading and deleting because we now fetch more data than we need which lessens performance.
//Decided it was OK because i dont think we will be fetching big enough objects for it to affect performance noticeably
    public AttractionCuDto(IAttraction org)
    {
        AttractionId = org.AttractionId;
        AttractionName = org.AttractionName;
        AttractionDescription = org.AttractionDescription;
        AttractionCategory = org.AttractionCategory;
        CityId = org.City?.CityId;
        CityName = org.City?.CityName;
        CountryId = org.City?.Country?.CountryId;
        CountryName = org.City?.Country?.CountryName;
    }
}
public class ReviewCuDto
{
    public virtual Guid? ReviewId {get; set;}
    public virtual string ReviewComment {get; set;}
    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
    public virtual int ReviewRating {get; set;}

    public virtual Guid? AttractionId {get; set;}
    public virtual Guid? UserId {get; set;}

    public ReviewCuDto(){}

    public ReviewCuDto(IReview org)
    {
        ReviewId = org.ReviewId;
        ReviewComment = org.ReviewComment;
        ReviewRating = org.ReviewRating;
        AttractionId = org.Attraction?.AttractionId;
        UserId = org.User?.UserId;
    }

}
public class UserCuDto
{
    public virtual Guid? UserId {get; set;}
    public virtual string UserName {get; set;}
    
    public UserCuDto(){}

    public UserCuDto(IUser org)
    {
        UserId = org.UserId;
        UserName = org.UserName;
    }
}