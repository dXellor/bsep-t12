using bsep_dll.Helpers.Encryption;

namespace bsep_dll.Models;

public class Advertisement
{
    public virtual User? User { get; set; }
    public int Id { get; set; }
    public int UserId { get; set; }
    public String Slogan { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public String Description { get; set; }
    
    public Advertisement() {}

    public Advertisement(int id, int userId, string slogan, DateTime start, DateTime end, string description)
    {
        Id = id;
        UserId = userId;
        Slogan = slogan;
        Start = start;
        End = end;
        Description = description;
    }
    
    public void EncryptData(string key, string iv)
    {
        Slogan = EncryptionUtils.EncryptWithEncodingType(Slogan, key, iv, EncodingType.UTF8);
        UserId = Int32.Parse(EncryptionUtils.EncryptWithEncodingType(UserId.ToString(), key, iv, EncodingType.UTF8));
        Start = DateTime.Parse(EncryptionUtils.EncryptWithEncodingType(Start.ToString("o"), key, iv, EncodingType.UTF8));
        End = DateTime.Parse(EncryptionUtils.EncryptWithEncodingType(End.ToString("o"), key, iv, EncodingType.UTF8));
        Description = EncryptionUtils.EncryptWithEncodingType(Description, key, iv, EncodingType.UTF8);
    }
    
    public void DecryptData(string key, string iv)
    {
        Slogan = EncryptionUtils.DecryptWithEncodingType(Slogan, key, iv, EncodingType.UTF8);
        UserId = Int32.Parse(EncryptionUtils.DecryptWithEncodingType(UserId.ToString(), key, iv, EncodingType.UTF8));
        Start = DateTime.Parse(EncryptionUtils.DecryptWithEncodingType(Start.ToString("o"), key, iv, EncodingType.UTF8));
        End = DateTime.Parse(EncryptionUtils.DecryptWithEncodingType(End.ToString("o"), key, iv, EncodingType.UTF8));
        Description = EncryptionUtils.DecryptWithEncodingType(Description, key, iv, EncodingType.UTF8);
    }
}