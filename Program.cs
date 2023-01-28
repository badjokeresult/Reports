using Reports.Controllers;
using Reports.Models.Workers;
using Reports.UI;

namespace Reports;

public static class Program
{
    public static void Main()
    {
        var dbWorker = new DbWorker();
        var reportWorker = new ReportWorker(dbWorker);
        var menu = new CommonMenu(reportWorker);
        
        menu.Processing();
    }
}