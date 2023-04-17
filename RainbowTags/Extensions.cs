using Exiled.API.Features;
using UnityEngine;

namespace RainbowTags;

public static class Extensions
{
    public static void RemoveComponent(this Player player, Component component)
    {
        if (player.GameObject.GetComponent(component.GetType()) != null)
            Object.Destroy(component);
    }
    
    public static void RemoveComponent<T>(this Player player) where T : Component
    {
        var component = player.GameObject.GetComponent<T>();
        if (component != null)
            Object.Destroy(component);
    }
}