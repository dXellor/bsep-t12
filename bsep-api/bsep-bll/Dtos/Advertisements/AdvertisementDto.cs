namespace bsep_bll.Dtos.Advertisements;

public class AdvertisementDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public String Slogan { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public String Description { get; set; }
    
    public AdvertisementDto() {}

    public AdvertisementDto(int id, int userId, string slogan, DateTime start, DateTime end, string description)
    {
        Id = id;
        UserId = userId;
        Slogan = slogan;
        Start = start;
        End = end;
        Description = description;
    }
    
}