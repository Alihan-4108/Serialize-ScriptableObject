using System;
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

            if (!IsValidType(fieldInfo.FieldType))
            {
                HelpBox(position, "[SerializeSO] can only be used on ScriptableObject or Component types.", MessageType.Error);
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

                    var attr = (SerializeSOAttribute)attribute;

                    DrawHeader(property, attr.color);

                    var style = new GUIStyle(GUI.skin.window);
                    style.padding = new RectOffset(10, 10, 10, 10);

                    EditorGUILayout.BeginVertical(style);

                    cachedEditor.OnInspectorGUI();

                    EditorGUILayout.EndVertical();
                }
            }
        }

        private void DrawHeader(SerializedProperty property, string hexColor)
        {
            var attr = (SerializeSOAttribute)attribute;

            string headerLabel = attr.label != "" ? attr.label : property.objectReferenceValue.name;

            ColorUtility.TryParseHtmlString(hexColor, out Color color);

            Rect headerRect = EditorGUILayout.GetControlRect(false, 24f);

            var labelStyle = new GUIStyle(EditorStyles.boldLabel);
            labelStyle.normal.textColor = color;
            labelStyle.alignment = attr.titleAlignment;
            labelStyle.padding = new RectOffset(8, 0, 0, 0);

            LabelField(headerRect, headerLabel, labelStyle);
        }

        private static bool IsValidType(Type type)
        {
            if (typeof(GameObject).IsAssignableFrom(type)) return false;
            if (typeof(Transform).IsAssignableFrom(type)) return false;

            return typeof(ScriptableObject).IsAssignableFrom(type) || typeof(Component).IsAssignableFrom(type);
        }
    }
}
