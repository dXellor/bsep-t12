namespace bsep_bll.Dtos.Monitoring;

public class FileResponseDto
{
    public MemoryStream MemoryStream { get; set; }
    public string ContentType { get; set; }
    public string FilePath { get; set; }

    public FileResponseDto(MemoryStream memoryStream, string contentType, string filePath)
    {
        MemoryStream = memoryStream;
        ContentType = contentType;
        FilePath = filePath;
    }
}