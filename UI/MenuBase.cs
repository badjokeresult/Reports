using Reports.Controllers;

namespace Reports.UI;

public abstract class MenuBase
{
    protected readonly IReportWorker _reportWorker;

    protected MenuBase(IReportWorker reportWorker)
    {
        _reportWorker = reportWorker;
    }
    
    public abstract void Processing();

    protected void CreateReport()
    {
        _reportWorker.CreateReport();
    }

    protected void GetReport(int id = -1)
    {
        if (id == -1)
            id = GetID();
        var reportInfo = _reportWorker.GetReport(id);
        Console.WriteLine(reportInfo);
    }

    private int GetID()
    {
        while (true)
        {
            int id = -1;
            try
            {
                Console.Write("Enter the report's ID: ");
                id = int.Parse(Console.ReadLine());
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Please enter the value");
            }
            catch (FormatException)
            {
                Console.WriteLine("Entered value is not an integer number");
            }
            return id;
        }
    }

    protected void GetReports()
    {
        var reportsInfos = _reportWorker.GetReports();
        foreach (var info in reportsInfos)
            Console.WriteLine(info);
    }

    protected void ChangeReport(int id = -1)
    {
        if (id == -1)
            id = GetID();
        _reportWorker.ChangeReport(id);
    }

    protected void DeleteReport(int id = -1)
    {
        if (id == -1)
            id = GetID();
        _reportWorker.DeleteReport(id);
    }
}