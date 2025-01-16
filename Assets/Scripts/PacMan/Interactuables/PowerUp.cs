using Moonthsoft.Core.FSM;
using UnityEngine;
using Zenject;

namespace Moonthsoft.PacMan
{
    public class PowerUp : Item
    {
        private void Start()
        {
            levelManager.AddPowerUp(this);
        }

        protected override void ActionTrigger()
        {
            levelManager.ActivePowerUp();
        }
    }
}