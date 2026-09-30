namespace Models.DTO;
public class ReviewDto
{
    public virtual Guid ReviewId {get; set;}
    public virtual string ReviewComment {get; set;}
    public virtual int ReviewRating {get; set;}

    public virtual Guid? AttractionId {get; set;}
    public virtual string AttractionName {get; set;}
    public virtual Guid? UserId {get; set;}
    public virtual string UserName {get; set;}

    public ReviewDto(){}

    public ReviewDto(IReview org)
    {
        ReviewId = org.ReviewId;
        ReviewComment = org.ReviewComment;
        ReviewRating = org.ReviewRating;
        AttractionId = org.Attraction?.AttractionId;
        AttractionName = org.Attraction?.AttractionName;
        UserId = org.User?.UserId;
        UserName = org.User?.UserName;
    }

}