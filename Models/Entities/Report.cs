using System.Text;

namespace Reports.Models.Entities;

public class Report
{
    public int Id { get; }
    public TimeSpan? CreationTime { get; set; }
    public bool IsSuccessful { get; set; }

    public Report(int id, TimeSpan? creationTime, bool isSuccessful)
    {
        Id = id;
        CreationTime = creationTime;
        IsSuccessful = isSuccessful;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        
        builder.AppendLine($"ID : {Id}");
        builder.AppendLine(IsSuccessful
            ? $"Creation Time : {CreationTime!.Value}"
            : "Creation Time is unknown");
        builder.AppendLine($"Is successful : {IsSuccessful}");

        return builder.ToString();
    }
}