using Microsoft.EntityFrameworkCore;

using Reports.Models.ApplicationContext;
using Reports.Models.Entities;

namespace Reports.Models.Workers;

public class DbWorker : IDbWorker
{
    public async Task CreateAsync(Report report)
    {
        await using var db = new AppDbContext();

        await db.Reports.AddAsync(report);

        await db.SaveChangesAsync();
    }

    public async Task<Report?> GetAsync(int id)
    {
        await using var db = new AppDbContext();

        var report = await db.Reports.FirstOrDefaultAsync(r => r.Id == id);

        return report;
    }

    public async Task<IEnumerable<Report>> GetAllAsync()
    {
        await using var db = new AppDbContext();

        var reports = await db.Reports.ToListAsync();

        return reports;
    }

    public async Task ChangeAsync(int id, Report newReport)
    {
        await using var db = new AppDbContext();

        var oldReport = await db.Reports.FirstOrDefaultAsync(r => r.Id == id);

        if (oldReport != null)
        {
            oldReport.CreationTime = newReport.CreationTime;
            oldReport.IsSuccessful = newReport.IsSuccessful;
            
            await db.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        await using var db = new AppDbContext();

        var report = db.Reports.FirstOrDefault(r => r.Id == id);
        if (report != null)
            db.Reports.Remove(report);

        await db.SaveChangesAsync();
    }

    public async Task<int?> GetLastRowId()
    {
        await using var db = new AppDbContext();

        var report = await db.Reports
            .OrderByDescending(r => r.Id)
            .FirstOrDefaultAsync();

        return report?.Id;
    }
}