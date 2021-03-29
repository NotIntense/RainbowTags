namespace RainbowTags
{
    using System;
    using Exiled.API.Features;

    public class RainbowTagMod : Plugin<Config>
    {
        private static readonly RainbowTagMod InstanceValue = new RainbowTagMod();
        
        private RainbowTagMod()
        {
        }

        public static RainbowTagMod Instance { get; } = InstanceValue;

        public override string Author { get; } = "FruitBoi";

        public override string Name { get; } = nameof(RainbowTags);

        public override Version RequiredExiledVersion { get; } = new Version(2, 9, 4);

        public override Version Version { get; } = new Version(2, 2);

        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Player.Verified += EventHandler.OnVerified;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Verified -= EventHandler.OnVerified;
            base.OnDisabled();
        }
    }
}