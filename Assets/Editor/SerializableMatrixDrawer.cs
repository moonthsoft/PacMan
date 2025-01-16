using UnityEditor;
using UnityEngine;

namespace Moonthsoft.Core
{
    [CustomPropertyDrawer(typeof(SerializableMatrix<>))]
    public class SerializableMatrixDrawer : PropertyDrawer
    {
#if UNITY_EDITOR
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);

            SerializedProperty sizeX = property.FindPropertyRelative("_sizeRow");
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(sizeX, new GUIContent("Row:"));
            EditorGUILayout.Space();
            EditorGUILayout.EndHorizontal();

            SerializedProperty sizeY = property.FindPropertyRelative("_sizeColumn");
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(sizeY, new GUIContent("Column:"));
            EditorGUILayout.Space();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            SerializedProperty matrix = property.FindPropertyRelative("_serializedMatrix");

            for (int i = 0; i < matrix.arraySize; ++i)
            {
                EditorGUILayout.BeginHorizontal();

                SerializedProperty array = matrix.GetArrayElementAtIndex(i).FindPropertyRelative("array");

                for (int j = 0; j < array.arraySize; ++j)
                {
                    EditorGUILayout.PropertyField(array.GetArrayElementAtIndex(j), GUIContent.none);
                    EditorGUILayout.Space();
                }

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();
            }

            EditorGUI.EndProperty();
        }
#endif
    }
}
