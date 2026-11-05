using UnityEngine;

namespace Interaction
{
    public interface IInteractive
    {
        public Vector2 InteractDirection(Collision2D collision2D, Vector2 objectDirection);
    }
}