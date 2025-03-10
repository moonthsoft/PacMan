namespace Moonthsoft.PacMan
{
    /// <summary>
    /// Class for the points that Pac-man "eats" in the level.
    /// </summary>
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