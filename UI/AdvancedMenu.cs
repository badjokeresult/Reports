using System.Text.RegularExpressions;

using Reports.Controllers;

namespace Reports.UI;

public class AdvancedMenu : MenuBase
{
    public AdvancedMenu(IReportWorker reportWorker)
        : base(reportWorker)
    {}
    
    public override void Processing()
    {
        Regex[] patterns =
        {
            new(@"create"),
            new(@"get \w+"),
            new(@"get -a"),
            new(@"get --all"),
            new(@"change \w+"),
            new(@"delete \w+"),
            new(@"quit")
        };
        
        while (true)
        {
            Console.Write("reports > ");
            var command = Console.ReadLine();
            if (command == null)
                continue;

            for (var i = 0; i < patterns.Length; i++)
            {
                if (patterns[i].Matches(command.Trim()).Count == command.Trim().Length)
                {
                    switch (i)
                    {
                        case 0:
                            CreateReport();
                            break;
                        case 1:
                            GetReport();
                            break;
                        case 2: 
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
                            return;
                        default:
                            Console.WriteLine("Unknown command");
                            break;
                    }
                }
            }
        }
    }
}