using Kiraio.UniGify.Components;
using Kiraio.UniGify.Decoder;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Kiraio.UniGify.Editor.Components
{
    [CustomEditor(typeof(GifRawImage))]
    public class GifRawImageEditor : GifViewerEditor<GifRawImage>
    {
        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnCustomInspectorGUI(GifRawImage gifRawImage)
        {
            if (gifRawImage.RawImage == null)
                gifRawImage.RawImage = gifRawImage.GetComponent<RawImage>();

            TextAsset gifAsset = (TextAsset)sourceFieldInfo.GetValue(gifRawImage);
            GifDecoder decoder = new GifDecoder();

            if (GUI.changed)
                gifRawImage.RawImage.texture =
                    gifAsset != null
                        ? decoder.Decode(gifAsset.GetData<byte>().ToArray(), 1)[0].Texture
                        : null;

            serializedObject.ApplyModifiedProperties();
        }
    }
}
