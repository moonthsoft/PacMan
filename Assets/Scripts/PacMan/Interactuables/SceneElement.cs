using UnityEngine;

namespace Moonthsoft.PacMan
{
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