using System;

namespace Celeste.Mod.TickMissReproducer;

public class TickMissReproducer : EverestModule
{
    public override Type SettingsType => typeof(TickMissReproducerModuleSettings);
    public static TickMissReproducerModuleSettings Settings => (TickMissReproducerModuleSettings)instance._Settings;

    public override Type SessionType => typeof(TickMissReproducerModuleSession);
    public static TickMissReproducerModuleSession Session => (TickMissReproducerModuleSession)instance._Session;

    private static TickMissReproducer instance = null!;

    public TickMissReproducer()
    {
        instance = this;
    }

    public override void Load()
    {
        Everest.Events.Level.OnLoadLevel += (level, _, _) => level.Add(new TestTickMissEntity());
    }

    public override void Unload() { }
}
