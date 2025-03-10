using Zenject;

namespace Moonthsoft.PacMan
{
    /// <summary>
    /// Interactable that serves as a basis for Items, that is, 
    /// elements in which when Pac-Man collides, they do an action and then deactivate.
    /// </summary>
    public abstract class Item : Interactuable
    {
        protected LevelManager levelManager;

        protected override string[] CollidableTags { get { return new string[] { "Player" }; } }


        protected abstract void ActionTrigger();

        [Inject] private void InjectLevelManager(LevelManager levelManagerInjected) { levelManager = levelManagerInjected; }


        protected sealed override void EnterTrigger(Character character)
        {
            ActionTrigger();

            gameObject.SetActive(false);
        }
    }
}
