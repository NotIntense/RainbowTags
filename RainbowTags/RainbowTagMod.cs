using Exiled.API.Features;

namespace RainbowTags
{
    public class RainbowTagMod : Plugin<Config>
    {
        public static RainbowTagMod RainbowTagRef { get; private set; }
        public override string Name => nameof(RainbowTags);
        public override string Author => "FruitBoi";
        private EventHandler _handler;

        public RainbowTagMod()
        {
            RainbowTagRef = this;
        }

        public override void OnEnabled()
        {
            if (RainbowTagRef.Config.UseCustomSequence)
                RainbowTagController.Colors = RainbowTagRef.Config.CustomSequence;

            _handler = new EventHandler();
            Exiled.Events.Handlers.Player.Verified += _handler.OnVerified;
            Exiled.Events.Handlers.Server.RoundStarted += _handler.OnRoundStartEvent;
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= _handler.OnRoundStartEvent;
            Exiled.Events.Handlers.Player.Verified -= _handler.OnVerified;
            _handler = null;
        }
    }
}