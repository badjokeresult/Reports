using Reports.Models.Entities;

namespace Reports.Models.Workers;

public interface IDbWorker
{
    public Task CreateAsync(Report report);
    public Task<Report?> GetAsync(int id);
    public Task<IEnumerable<Report>> GetAllAsync();
    public Task ChangeAsync(int id, Report newReport);
    public Task DeleteAsync(int id);
    public Task<int?> GetLastRowId();
}