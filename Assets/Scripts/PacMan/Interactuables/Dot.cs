using UnityEngine;

namespace Moonthsoft.PacMan
{
    public class Dot : Item
    {
        private void Start()
        {
            levelManager.AddDot(this);
        }

        protected override void ActionTrigger()
        {
            levelManager.RemoveDot();
        }
    }
}