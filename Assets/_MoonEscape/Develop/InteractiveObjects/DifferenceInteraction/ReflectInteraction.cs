using Unity.VisualScripting;
using UnityEngine;

public class ReflectInteraction : IInteractive
{
    Vector2 IInteractive.InteractDirection(Collision2D collision2D, Vector2 objectDirection)
    {
        var collisionNormal = collision2D.contacts[0].normal;
        var reflectedDireciton = Vector2.Reflect(objectDirection, collisionNormal);

        return reflectedDireciton;
    }
}
