using Reports.Controllers;

namespace Reports.UI;

public class CommonMenu : MenuBase
{
    private static readonly string[] MenuOptions =
    {
        "1. Create new report",
        "2. Get a report by ID",
        "3. Get all reports",
        "4. Change a report by ID",
        "5. Delete a report by ID",
        "6. Quit"
    };

    public CommonMenu(IReportWorker reportWorker)
        : base(reportWorker)
    {}

    private static void PrintMenuOptions()
    {
        foreach (var option in MenuOptions)
            Console.WriteLine(option);
    }

    public override void Processing()
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
}