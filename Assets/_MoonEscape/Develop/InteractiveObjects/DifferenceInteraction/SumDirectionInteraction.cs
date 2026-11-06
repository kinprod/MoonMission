using UnityEngine;

public class SumDirectionInteraction : IInteractive
{
    Vector2 IInteractive.InteractDirection(Collision2D collision2D, Vector2 objectDirection)
    {
        if (collision2D != null)
        {
            var interactiveObject = collision2D.gameObject.GetComponent<InteractiveObject>();

            if (interactiveObject != null)
            {
                return interactiveObject.Direction + objectDirection;
            }
            else
            {
                IInteractive reflectInteraction = new ReflectInteraction();
                return reflectInteraction.InteractDirection(collision2D, objectDirection);
            }
        }
        return Vector2.zero;
    }
}
