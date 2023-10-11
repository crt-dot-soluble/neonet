
namespace neonet.lib;


/// <summary>
/// Represents the current and elapsed game times
/// </summary>
public class GameTime
{
    public TimeSpan TotalTime { get; set; }
    public TimeSpan ElapsedTime { get; set; }


    public GameTime()
    {
        TotalTime = TimeSpan.Zero;
        ElapsedTime = TimeSpan.Zero;
    }


    public GameTime(TimeSpan totalTime, TimeSpan elapsedTime)
    {
        TotalTime = totalTime;
        ElapsedTime = elapsedTime;
    }
}