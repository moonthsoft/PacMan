using UnityEngine;

namespace Moonthsoft.PacMan
{
    /// <summary>
    /// Class in charge of moving Pac-Man and the ghosts in the tunnel, 
    /// from one side to the other.
    /// </summary>
    public class Teleport : SceneElement
    {
        [SerializeField] private Teleport _otherTeleport;
        [SerializeField] private NodeGraph _node;

        public NodeGraph Node { get => _node; set => _node = value; }
        public bool CanTeleport { get; set; } = true;
        protected override string[] CollidableTags { get { return new string[] { "Player", "Ghost" }; } }


        protected override void EnterTrigger(Character character)
        {
            if (CanTeleport)
            {
                _otherTeleport.CanTeleport = false;

                character.transform.position = _otherTeleport.Node.transform.position;
                character.CurrentNode = _otherTeleport.Node;
            }
        }

        protected override void ExitTrigger()
        {
            CanTeleport = true;
        }
    }
}