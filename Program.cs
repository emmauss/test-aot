
using System.ComponentModel.DataAnnotations;

internal class Program
{
    static Func<TimeSpan, bool>? Validate { get; set; }
    private static void Main(string[] args)
    {
        // trying to reproduce a different bug encountered on arm but not on arm64 or win_x64, but bug doesn't occur. occurs in live project. This may only occur in shared library mode
        var timespan = TimeSpan.Zero;
        Validate = IsValid;
        Console.WriteLine($"Timespan total ms should be 0, but it's {timespan.TotalMilliseconds}. Comparison timespan.TotalMilliseconds >= 0.0 is not {Validate?.Invoke(timespan) == false}");

        // this crashes on arm, but not arm64 or win_x64
        var timer = new Timer(_ =>
        {

        }, null, TimeSpan.FromMilliseconds(60), Timeout.InfiniteTimeSpan);
        Console.WriteLine("Hello, World!");
    }

    private static bool IsValid(TimeSpan value) => value.TotalMilliseconds >= 0.0;
}