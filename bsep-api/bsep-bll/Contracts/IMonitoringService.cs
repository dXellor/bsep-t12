using bsep_bll.Dtos.Monitoring;

namespace bsep_bll.Contracts;

public interface IMonitoringService
{
    Task<FileResponseDto> GetLatestLogs();
}