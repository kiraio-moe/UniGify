using Kiraio.UniGify.Components;
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

            Texture2D sourceTexture = (Texture2D)sourceFieldInfo.GetValue(gifImage);

            if (GUI.changed)
                gifImage.Image.sprite =
                    sourceTexture != null ? gifImage.Texture2DToSprite(sourceTexture) : null;

            serializedObject.ApplyModifiedProperties();
        }
    }
}
