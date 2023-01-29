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

        MenuBase menu;
        Console.WriteLine("Do you want to use advanced menu (1 - yes, 2 - no, default - 2): ");
        var choice = Console.ReadLine();
        menu = choice == "1"
            ? new AdvancedMenu(reportWorker)
            : new CommonMenu(reportWorker);

        menu.Processing();
    }
}