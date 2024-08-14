using Kiraio.UniGify.Components;
using UnityEditor;
using UnityEngine;

namespace Kiraio.UniGify.Editor.Components
{
    [CustomEditor(typeof(GifSpriteRenderer))]
    public class GifSpriteRendererEditor : GifViewerEditor<GifSpriteRenderer>
    {
        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnCustomInspectorGUI(GifSpriteRenderer gifSpriteRenderer)
        {
            if (gifSpriteRenderer.Renderer == null)
                gifSpriteRenderer.Renderer = gifSpriteRenderer.GetComponent<SpriteRenderer>();

            Texture2D sourceTexture = (Texture2D)sourceFieldInfo.GetValue(gifSpriteRenderer);

            if (GUI.changed)
                gifSpriteRenderer.Renderer.sprite =
                    sourceTexture != null
                        ? gifSpriteRenderer.Texture2DToSprite(sourceTexture)
                        : null;

            serializedObject.ApplyModifiedProperties();
        }
    }
}
