using UnityEditor;
using UnityEngine;
using static UnityEditor.EditorGUI;

namespace Alihan4108.SerializeScriptableObject
{
    [CustomPropertyDrawer(typeof(SerializeSOAttribute))]
    public class SerializeSODrawer : PropertyDrawer
    {
        private bool serializeButton;
        private const float WidthSymbol = 30.0f;
        private Editor cachedEditor;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.ObjectReference)
            {
                base.OnGUI(position, property, label);
                return;
            }

            if (!typeof(ScriptableObject).IsAssignableFrom(fieldInfo.FieldType))
            {
                EditorGUI.HelpBox(position, $"[SerializeSO] yalnızca ScriptableObject tiplerinde kullanılabilir.", MessageType.Error);
                return;
            }

            var createSerializeSO = (SerializeSOAttribute)attribute;

            float spacing = 5f;
            float objectFieldWidth = position.width - WidthSymbol - spacing;

            Rect serializeButtonRect = new Rect(position.x, position.y, WidthSymbol, position.height);
            Rect objectFieldRect = new Rect(serializeButtonRect.xMax + spacing, position.y, objectFieldWidth, position.height);

            DrawEditButton(serializeButtonRect, property);

            ObjectField(objectFieldRect, property, label);

            EditorGUIUtility.labelWidth = 0;
        }

        private void DrawEditButton(Rect position, SerializedProperty property)
        {
            using (new DisabledScope(property.objectReferenceValue == null))
            {
                serializeButton = GUI.Toggle(position, serializeButton, EditorGUIUtility.IconContent("d_Grid.PaintTool@2x"), EditorStyles.miniButton);

                if (serializeButton && property.objectReferenceValue != null)
                {
                    Editor.CreateCachedEditor(property.objectReferenceValue, null, ref cachedEditor);

                    EditorGUILayout.BeginVertical(new GUIStyle(GUI.skin.window));

                    cachedEditor.OnInspectorGUI();

                    EditorGUILayout.EndVertical();
                }
            }
        }
    }
}