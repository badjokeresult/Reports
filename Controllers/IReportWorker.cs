namespace Reports.Controllers;

public interface IReportWorker
{
    public void CreateReport();
    public string GetReport(int id);
    public IEnumerable<string> GetReports();
    public void ChangeReport(int id);
    public void DeleteReport(int id);
}