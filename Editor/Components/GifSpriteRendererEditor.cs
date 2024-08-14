using Kiraio.UniGify.Components;
using UnityEditor;
using UnityEngine;

namespace Kiraio.UniGify.Editor.Components
{
    [CustomEditor(typeof(GifSpriteRenderer))]
    public class GifSpriteRendererEditor : GifViewerEditor<GifSpriteRenderer>
    {
        SerializedProperty rendererProperty;

        protected override void OnEnable()
        {
            base.OnEnable();
            rendererProperty = serializedObject.FindProperty("spriteRenderer");
        }

        protected override void OnCustomInspectorGUI(GifSpriteRenderer gifSpriteRenderer)
        {
            if (gifSpriteRenderer.Renderer == null)
                gifSpriteRenderer.Renderer = gifSpriteRenderer.GetComponent<SpriteRenderer>();

            Texture2D sourceTexture = (Texture2D)sourceFieldInfo.GetValue(gifSpriteRenderer);

            // Ensure the sprite is set
            if (GUI.changed && sourceTexture != null)
                gifSpriteRenderer.Renderer.sprite = gifSpriteRenderer.Texture2DToSprite(
                    sourceTexture
                );

            serializedObject.ApplyModifiedProperties();
        }
    }
}
