using Nebuli.API.Features.Player;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RainbowTags;

public static class Extensions
{
    public static void RemoveComponent(this NebuliPlayer player, Component component)
    {
        var componentInstance = player.GameObject.GetComponent(component.GetType());
        if (componentInstance != null)
            Object.Destroy(componentInstance);
    }
    
    public static void RemoveComponent<T>(this NebuliPlayer player) where T : Component
    {
        var component = player.GameObject.GetComponent<T>();
        if (component != null)
            Object.Destroy(component);
    }
}