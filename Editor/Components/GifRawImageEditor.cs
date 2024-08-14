using Kiraio.UniGify.Components;
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

            Texture2D sourceTexture = (Texture2D)sourceFieldInfo.GetValue(gifRawImage);

            if (GUI.changed)
                gifRawImage.RawImage.texture = sourceTexture != null ? sourceTexture : null;

            serializedObject.ApplyModifiedProperties();
        }
    }
}
