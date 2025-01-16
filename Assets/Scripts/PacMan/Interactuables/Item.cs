using UnityEngine;
using Zenject;

namespace Moonthsoft.PacMan
{
    public abstract class Item : Interactuable
    {
        protected LevelManager levelManager;
        [Inject] private void InjectLevelManager(LevelManager levelManagerInjected) { levelManager = levelManagerInjected; }

        protected abstract void ActionTrigger();

        protected override string[] CollidableTags { get { return new string[] { "Player" }; } }

        protected sealed override void EnterTrigger(Character character)
        {
            ActionTrigger();

            gameObject.SetActive(false);
        }
    }
}
