using System.Diagnostics;

using Reports.Models.Entities;
using Reports.Models.Workers;

namespace Reports.Controllers;

public class ReportWorker : IReportWorker
{
    private readonly IDbWorker _dbWorker;

    private static int _reportId = 1;

    public ReportWorker(IDbWorker dbWorker)
    {
        _dbWorker = dbWorker;
    }
    
    public void CreateReport()
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;
        
        var random = new Random();
        var timeToBuild = random.Next(1, 10);

        Stopwatch timer = new Stopwatch();
        var buildingReportTask = BuildReport(timeToBuild, cancellationToken);
        var waitingForCancellation = WaitingForCancellation(timeToBuild);
        
        timer.Start();
        buildingReportTask.Start();
        waitingForCancellation.Start();

        TimeSpan? creationTime = null;
        if (buildingReportTask.IsCompleted && waitingForCancellation.IsCompleted)
        {
            creationTime = timer.Elapsed;
        }
        else
        {
            buildingReportTask.Wait();
            waitingForCancellation.Wait();
        }

        var report = new Report(_reportId++, creationTime, creationTime != null);
        _dbWorker.CreateAsync(report);
    }

    private Task BuildReport(int timeToBuild, CancellationToken token)
    {
        Console.WriteLine("The report building is begun...");
        var task = new Task(() => 
        {
            for (var i = 0; i < timeToBuild; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("The report building was corrupted");
                    return;
                }
                Thread.Sleep(1000);
            }
            Console.WriteLine("The report was build successfully, enter any button to continue");
        }, token);
        return task;
    }

    private Task WaitingForCancellation(int timeToBuild)
    {
        Console.WriteLine("Press Esc to cancel the report building");
        var task = new Task(() => // TODO: эта штука будет просить до 10 нажатий
        {
            for (var i = 0; i < timeToBuild; i++)
            {
                var buttonClick = Console.ReadKey();
                if (buttonClick.Key == ConsoleKey.Escape)
                    return;
                Thread.Sleep(1000);
            }
        });

        return task;
    }

    public string GetReport(int id)
    {
        var report = _dbWorker.GetAsync(id).Result;

        if (report != null)
            return report.ToString();
        return "No report with such ID was found";
    }

    public IEnumerable<string> GetReports()
    {
        var reports = _dbWorker.GetAllAsync().Result;

        foreach (var report in reports)
            yield return report.ToString();
    }

    public async Task ChangeReport(int id)
    {
        throw new NotImplementedException();
    }

    public void DeleteReport(int id)
    {
        _dbWorker.DeleteAsync(id).Wait();
    }
    
}