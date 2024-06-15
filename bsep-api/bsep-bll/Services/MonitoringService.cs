using bsep_bll.Contracts;
using bsep_bll.Dtos.Monitoring;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace bsep_bll.Services;

public class MonitoringService: IMonitoringService
{
    private readonly IConfiguration _configuration;

    public MonitoringService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<FileResponseDto> GetLatestLogs()
    {
        var logPath = GetLatestLogPath();
        if (string.IsNullOrEmpty(logPath) || !File.Exists(logPath))
        {
            return null;
        }
        
        var memory = new MemoryStream();
        await  using var stream = new FileStream(logPath, FileMode.Open);
        await stream.CopyToAsync(memory);
        memory.Position = 0;
        return new FileResponseDto(memory, "text/plain", logPath);
    }

    private string GetLatestLogPath()
    {
        var dir = new DirectoryInfo(_configuration["Monitoring:LogsDirectory"]!);
        var logFile = dir.GetFiles("*.txt").OrderByDescending(f => f.LastWriteTime).FirstOrDefault();
        return logFile != null ? Path.Combine(_configuration["Monitoring:LogsDirectory"]!, logFile.Name) : string.Empty;
    }
}