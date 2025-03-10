using UnityEditor;

namespace Moonthsoft.PacMan
{
    /// <summary>
    /// Show a ShowGizmos checkbox to appear in the Graph class inspector.
    /// If its value changes, it repaints the scene to show or hide the graph gizmos.
    /// Gizmo drawing is done in the NodeGraphEditor class.
    /// </summary>
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