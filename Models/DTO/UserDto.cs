namespace Models.DTO;
public class UserDto
{
    public virtual Guid UserId {get; set;}
    public virtual string UserName {get; set;}
    //public virtual List<Guid> Reviews {get; set;}
    public virtual List<string> ReviewComment {get; set;}
    
    public UserDto(){}

    public UserDto(IUser org)
    {
        UserId = org.UserId;
        UserName = org.UserName;
        //Reviews = org.Reviews?.Select(r=> r.ReviewId).ToList();
        ReviewComment = org.Reviews?.Select(r=> r.ReviewComment).ToList();
    }
}