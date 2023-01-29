using System.Diagnostics;

using Reports.Models.Entities;
using Reports.Models.Workers;

namespace Reports.Controllers;

public class ReportWorker : IReportWorker
{
    private readonly IDbWorker _dbWorker;
    private int _reportId;

    public ReportWorker(IDbWorker dbWorker)
    {
        _dbWorker = dbWorker;
        _reportId = _dbWorker.GetLastRowId().Result ?? 1;
    }
    
    public void CreateReport()
    {
        var report = BuildReport(false);
        _dbWorker.CreateAsync(report);
    }

    private Report BuildReport(bool isExisting)
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;
        
        var random = new Random();
        var timeToBuild = random.Next(1, 10);

        Stopwatch timer = new Stopwatch();
        var buildingReportTask = BuildingReportInfo(timeToBuild, cancellationToken);
        var waitingForCancellation = WaitingForCancellation(timeToBuild, cancellationTokenSource, buildingReportTask);
        
        timer.Start();
        buildingReportTask.Start();
        waitingForCancellation.Start();

        buildingReportTask.Wait();
        waitingForCancellation.Wait();
        
        TimeSpan? creationTime = timer.Elapsed;

        var id = isExisting
            ? _reportId
            : _reportId++;
        return new Report(id, creationTime, !cancellationToken.IsCancellationRequested);
    }

    private Task BuildingReportInfo(int timeToBuild, CancellationToken token)
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

    private Task WaitingForCancellation(int timeToBuild, CancellationTokenSource tokenSource, Task mainTask)
    {
        Console.WriteLine("Press Backspace to cancel the report building");
        var task = new Task(() =>
        {
            for (var i = 0; i < timeToBuild; i++)
            {
                if (mainTask.IsCompletedSuccessfully)
                    return;
                
                var buttonClick = Console.ReadKey();
                if (buttonClick.Key == ConsoleKey.Backspace)
                {
                    tokenSource.Cancel();
                    return;
                }

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

    public void ChangeReport(int id)
    {
        var report = BuildReport(true);
        _dbWorker.ChangeAsync(id, report);
    }

    public void DeleteReport(int id)
    {
        _dbWorker.DeleteAsync(id).Wait();
    }
    
}