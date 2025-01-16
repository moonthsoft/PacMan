using UnityEditor;
using UnityEngine;

namespace Moonthsoft.PacMan
{
    [CustomEditor(typeof(Graph))]
    public class GraphEditor : Editor
    {
        private bool _lastShowGizmos = Graph.ShowGizmos;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Graph.ShowGizmos = EditorGUILayout.Toggle("Show Gizmos", Graph.ShowGizmos);

            if (_lastShowGizmos != Graph.ShowGizmos)
            {
                _lastShowGizmos = Graph.ShowGizmos;

                SceneView.RepaintAll();
            }
        }
    }
}