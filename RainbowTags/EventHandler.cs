namespace RainbowTags
{
    using Exiled.Events.EventArgs;
    using RainbowTags.Components;

    public static class EventHandler
    {
        public static void OnVerified(VerifiedEventArgs ev)
        {
            ev.Player.GameObject.AddComponent<RainbowTagController>();
        }
    }
}