using Kiraio.UniGify.Components;
using Kiraio.UniGify.Decoder;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Kiraio.UniGify.Editor.Components
{
    [CustomEditor(typeof(GifImage))]
    public class GifImageEditor : GifViewerEditor<GifImage>
    {
        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnCustomInspectorGUI(GifImage gifImage)
        {
            if (gifImage.Image == null)
                gifImage.Image = gifImage.GetComponent<Image>();

            TextAsset gifAsset = (TextAsset)sourceFieldInfo.GetValue(gifImage);
            GifDecoder decoder = new GifDecoder();

            if (GUI.changed)
                gifImage.Image.sprite =
                    gifAsset != null
                        ? gifImage.Texture2DToSprite(
                            decoder.Decode(gifAsset.GetData<byte>().ToArray(), 1)[0].Texture
                        )
                        : null;

            serializedObject.ApplyModifiedProperties();
        }
    }
}
