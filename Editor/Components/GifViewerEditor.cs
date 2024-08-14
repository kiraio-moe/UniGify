using System.Reflection;
using Kiraio.UniGify.Components;
using UnityEditor;
using UnityEngine;

namespace Kiraio.UniGify.Editor.Components
{
    public abstract class GifViewerEditor<T> : UnityEditor.Editor
        where T : GifViewer
    {
        protected FieldInfo sourceFieldInfo;
        SerializedProperty sourceProperty;
        protected SerializedProperty playOnStartProperty;

        protected virtual void OnEnable()
        {
            // Get serialized properties
            sourceFieldInfo = typeof(GifViewer).GetField(
                "m_Source",
                BindingFlags.NonPublic | BindingFlags.Instance
            );
            sourceProperty = serializedObject.FindProperty(sourceFieldInfo.Name);
            playOnStartProperty = serializedObject.FindProperty(
                typeof(GifViewer)
                    .GetField("m_PlayOnStart", BindingFlags.NonPublic | BindingFlags.Instance)
                    .Name
            );
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update(); // Update the serialized object

            // Draw the default inspector
            EditorGUILayout.PropertyField(sourceProperty);
            playOnStartProperty.boolValue = EditorGUILayout.ToggleLeft(
                "Play On Start?",
                playOnStartProperty.boolValue
            );

            // Apply changes to the serialized object
            serializedObject.ApplyModifiedProperties();

            // Get a reference to the target component
            T gifViewer = (T)target;
            Texture2D sourceTexture = (Texture2D)sourceFieldInfo.GetValue(gifViewer);

            if (GUI.changed)
                gifViewer.SourcePath = AssetDatabase.GetAssetPath(sourceTexture) ?? string.Empty;

            OnCustomInspectorGUI(gifViewer);
        }

        // This method will be overridden by derived classes to add additional UI elements
        protected virtual void OnCustomInspectorGUI(T gifViewer) { }
    }
}
