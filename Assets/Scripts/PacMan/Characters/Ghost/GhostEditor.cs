using UnityEngine;

namespace Moonthsoft.PacMan
{
    /// <summary>
    /// Class that draws ghost gizmos in the scene, to show their pathfainding routes and information related to their AI behavior. 
    /// Used for debugging the AI.
    /// </summary>
    [ExecuteInEditMode]
    public class GhostEditor : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private Ghost _ghost;
        [SerializeField] private bool _showGizmos;


        private void OnDrawGizmos()
        {
            if (_ghost != null && _ghost.Path != null && _showGizmos)
            {
                Gizmos.color = GetColor();

                Vector3 lastPos = transform.position;

                for (int i = 0; i < _ghost.Path.Count; ++i)
                {
                    Vector3 newPos = _ghost.Path[i].transform.position;

                    Gizmos.DrawLine(lastPos, newPos);

                    lastPos = newPos;
                }

                Gizmos.DrawSphere(lastPos, 2f / 16f);

                if (_ghost.StateController != null
                    && _ghost.StateController.CurrentState == GhostState.Chase
                    && _ghost.LevelManager != null)
                {
                    if (_ghost.Type == GhostType.Clyde)
                    {
                        Gizmos.DrawWireSphere(_ghost.LevelManager.Player.transform.position, ClydeChaseState.DIST_SCAPE);
                    }

                    if (_ghost.Type == GhostType.Inky)
                    {
                        var posPlayer = _ghost.LevelManager.Player.transform.position;
                        var posBlinky = _ghost.LevelManager.GetGhost(GhostType.Blinky).transform.position;
                        Vector3 posTarget = (2f * posPlayer) - posBlinky;

                        Gizmos.DrawLine(posTarget, posBlinky);
                        Gizmos.DrawSphere(posTarget, 2f / 16f);
                    }
                }
            }
        }

        private Color GetColor()
        {
            switch (_ghost.Type)
            {
                case GhostType.Blinky: return Color.red;
                case GhostType.Pinky: return new Color(1f, 0.753f, 0.796f); //pink
                case GhostType.Inky: return Color.cyan;
                case GhostType.Clyde: return new Color(1f, 0.5f, 0f); //orange
            }

            Debug.LogError("Type ghost is not defined: " + _ghost.Type.ToString());

            return Color.white;
        }
#endif
    }
}