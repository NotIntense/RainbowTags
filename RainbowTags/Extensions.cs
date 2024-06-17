using Exiled.API.Features;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RainbowTags;

public static class Extensions
{
    public static void RemoveComponent(this Player player, Component component)
    {
        var componentInstance = player.GameObject.GetComponent(component.GetType());
        if (componentInstance != null)
            Object.Destroy(componentInstance);
    }
}