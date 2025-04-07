using UnityEngine;
using Moonthsoft.Core.Definitions.Direction;
using Moonthsoft.Core.Utils.Direction;

namespace Moonthsoft.PacMan
{
    /// <summary>
    /// Class that draws gizmos in the scene to show the position of the graph nodes and their connections, 
    /// so that you can visually check that the graph is correct.
    /// </summary>
    [ExecuteInEditMode]
    public class NodeGraphEditor : MonoBehaviour
    {
#if UNITY_EDITOR
        private readonly NodeGraph[] _lastNodes = new NodeGraph[4];

        [SerializeField] private NodeGraph _node;


        private void OnEnable()
        {
            if (_node != null)
            {
                for (int i = 0; i < _lastNodes.Length; ++i)
                {
                    _lastNodes[i] = _node.GetNode(i);
                }
            }
        }

        private void Update()
        {
            if (_node != null)
            {
                for (int i = 0; i < _lastNodes.Length; ++i)
                {
                    var nodeAux = _node.GetNode(i);

                    if (_lastNodes[i] != nodeAux)
                    {
                        _lastNodes[i]?.SetNode(null, (int)DirectionUtility.ReverseDirection(i));

                        _lastNodes[i] = nodeAux;

                        nodeAux?.SetNode(_node, (int)DirectionUtility.ReverseDirection(i));
                    }

                    if (_lastNodes[i] != null)
                    {
                        if (i == (int)Direction.Up
                            && (_lastNodes[i].transform.position.x != transform.position.x
                            || _lastNodes[i].transform.position.y <= transform.position.y))
                        {
                            Debug.LogError("Node incorrectly placed in Up position for node " + gameObject.name);
                        }
                        else if (i == (int)Direction.Down
                            && (_lastNodes[i].transform.position.x != transform.position.x
                            || _lastNodes[i].transform.position.y >= transform.position.y))
                        {
                            Debug.LogError("Node incorrectly placed in Down position for node " + gameObject.name);
                        }
                        else if (i == (int)Direction.Left
                            && (_lastNodes[i].transform.position.x >= transform.position.x
                            || _lastNodes[i].transform.position.y != transform.position.y)
                            && !_node.InTunnel)
                        {
                            Debug.LogError("Node incorrectly placed in Left position for node " + gameObject.name);
                        }
                        else if (i == (int)Direction.Right
                            && (_lastNodes[i].transform.position.x <= transform.position.x
                            || _lastNodes[i].transform.position.y != transform.position.y)
                            && !_node.InTunnel)
                        {
                            Debug.LogError("Node incorrectly placed in Right position for node " + gameObject.name);
                        }
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (_node != null && Graph.ShowGizmos)
            {
                Gizmos.color = Color.red;

                Gizmos.DrawSphere(transform.position, 2f / 16f);

                for (int i = 0; i < _lastNodes.Length; ++i)
                {
                    var nodeAux = _node.GetNode(i);

                    if (nodeAux != null && (!_node.InTunnel || !nodeAux.InTunnel))
                    {
                        Vector3 posAux = GetOffestLine((Direction)i);

                        Gizmos.DrawLine(transform.position + posAux, nodeAux.transform.position + posAux);
                    }
                }
            }
        }

        private Vector3 GetOffestLine(Direction direction)
        {
            Vector3 posAux = Vector3.zero;

            switch (direction)
            {
                case Direction.Up: posAux.x += 1f; break;
                case Direction.Down: posAux.x -= 1f; break;
                case Direction.Left: posAux.y += 1f; break;
                case Direction.Right: posAux.y -= 1f; break;
            }

            return posAux * (1f / 32f);
        }
#endif
    }
}