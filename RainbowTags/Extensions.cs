using UnityEngine;
using Object = UnityEngine.Object;

namespace RainbowTags;

public static class Extensions
{
    public static void RemoveComponent(this GameObject player, Component component)
    {
        Component componentInstance = player.GetComponent(component.GetType());

        if (componentInstance != null)
            Object.Destroy(componentInstance);
    }
}