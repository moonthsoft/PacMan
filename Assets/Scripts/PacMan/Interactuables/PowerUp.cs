namespace Moonthsoft.PacMan
{
    /// <summary>
    /// Class for the power ups that make ghosts vulnerable.
    /// </summary>
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