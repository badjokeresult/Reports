namespace Reports.Controllers;

public interface IReportWorker
{
    public void CreateReport();
    public string GetReport(int id);
    public IEnumerable<string> GetReports();
    public Task ChangeReport(int id);
    public void DeleteReport(int id);
}