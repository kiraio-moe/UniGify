using Kiraio.UniGify.Components;
using Kiraio.UniGify.Decoder;
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

            TextAsset gifAsset = (TextAsset)sourceFieldInfo.GetValue(gifSpriteRenderer);
            GifDecoder decoder = new GifDecoder();

            if (GUI.changed)
                gifSpriteRenderer.Renderer.sprite =
                    gifAsset != null
                        ? gifSpriteRenderer.Texture2DToSprite(
                            decoder.Decode(gifAsset.GetData<byte>().ToArray(), 1)[0].Texture
                        )
                        : null;

            serializedObject.ApplyModifiedProperties();
        }
    }
}
