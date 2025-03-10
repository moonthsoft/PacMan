using UnityEngine;

namespace Moonthsoft.PacMan
{
    /// <summary>
    /// Base class for persistent elements of the scene, 
    /// such as the teleport that handles teleportation in the tunnel.
    /// </summary>
    public abstract class SceneElement : Interactuable
    {
        protected abstract void ExitTrigger();


        private void OnTriggerExit2D(Collider2D collider)
        {
            if (CheckColision(collider))
            {
                ExitTrigger();
            }
        }
    }
}