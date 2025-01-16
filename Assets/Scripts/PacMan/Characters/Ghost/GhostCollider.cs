using Moonthsoft.PacMan;
using UnityEngine;

public class GhostCollider : Interactuable
{
    [SerializeField] private Ghost ghost;

    protected override string[] CollidableTags { get { return new string[] { "Player" }; } }

    private GhostState State { get { return ghost.StateController.CurrentState; } }


    protected sealed override void EnterTrigger(Character character)
    {
        if (State != GhostState.Eated)
        {
            if (State == GhostState.Frightened)
            {
                ghost.Die();
            }
            else
            {
                ghost.LevelManager.PlayerDie();
            }
        }
    }
}
