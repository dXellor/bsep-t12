namespace bsep_bll.Dtos;

public class AdvertisementDto
{
    public int Id { get; set; }
    public String Slogan { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public String Description { get; set; }
    
    public AdvertisementDto() {}

    public AdvertisementDto(int id, string slogan, DateTime start, DateTime end, string description)
    {
        Id = id;
        Slogan = slogan;
        Start = start;
        End = end;
        Description = description;
    }
    
}