using System;
using System.Reflection;
using Celeste;
using Monocle;

public class TestTickMissEntity : Entity
{
    private long lastTick = 0;
    private long lastTime = 0;
    private DateTime lastMissedTick = DateTime.Now;

    public TestTickMissEntity()
    {
        Console.WriteLine($"[{Assembly.GetExecutingAssembly().GetName().Name}] Started at {DateTime.Now}");
    }

    public override void Update()
    {
        var level = Scene as Level;

        var ticks = GetTicks(level);
        // Filter out first couple frames when values are 0 and session time doesn't change
        if (lastTick != 0 && lastTime != 0 && level.Session.Time != lastTime)
        {
            if (ticks < 500) return;

            if (ticks-1 != lastTick)
            {
                Console.WriteLine($"[{Assembly.GetExecutingAssembly().GetName().Name}] Missed tick at {DateTime.Now}");
                // This seems to always output at 833ms
                Console.WriteLine($"[{Assembly.GetExecutingAssembly().GetName().Name}] Diff: {DateTime.Now - lastMissedTick}");

                lastMissedTick = DateTime.Now;
            }
        }

        lastTime = level.Session.Time;
        lastTick = ticks;
    }

    private long GetTicks(Level level)
    {
        return level.Session.Time / TimeSpan.FromSeconds(Engine.RawDeltaTime).Ticks;
    }
}

