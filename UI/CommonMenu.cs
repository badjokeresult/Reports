using Reports.Controllers;

namespace Reports.UI;

public class CommonMenu : IMenu
{
    private readonly IReportWorker _reportWorker;

    private static readonly string[] menuOptions =
    {
        "1. Create new report",
        "2. Get a report by ID",
        "3. Get all reports",
        "4. Change a report by ID",
        "5. Delete a report by ID",
        "6. Quit"
    };

    public CommonMenu(IReportWorker reportWorker)
    {
        _reportWorker = reportWorker;
    }
    
    public void Processing()
    {
        while (true)
        {
            PrintMenuOptions();
            Console.WriteLine("Enter the number of option: ");
            int option = -1;
            try
            {
                option = int.Parse(Console.ReadLine());
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Please enter a value");
            }
            catch (FormatException)
            {
                Console.WriteLine("Entered value is not an integer number");
            }

            switch (option)
            {
                case 1:
                    CreateReport();
                    break;
                case 2:
                    GetReport();
                    break;
                case 3:
                    GetReports();
                    break;
                case 4:
                    ChangeReport();
                    break;
                case 5:
                    DeleteReport();
                    break;
                case 6:
                    Console.WriteLine("Quitting the program...");
                    return;
                default:
                    Console.WriteLine("Entered number is not in necessary range");
                    break;
            }
        }
    }

    private static void PrintMenuOptions()
    {
        foreach (var option in menuOptions)
            Console.WriteLine(option);
    }

    public void CreateReport()
    {
        _reportWorker.CreateReport();
    }

    public void GetReport()
    {
        var id = GetID();
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

    public void GetReports()
    {
        var reportsInfos = _reportWorker.GetReports();
        foreach (var info in reportsInfos)
            Console.WriteLine(info);
    }

    public void ChangeReport()
    {
        var id = GetID();
        _reportWorker.ChangeReport(id);
    }

    public void DeleteReport()
    {
        var id = GetID();
        _reportWorker.DeleteReport(id);
    }
}