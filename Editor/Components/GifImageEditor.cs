using Kiraio.UniGify.Components;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Kiraio.UniGify.Editor.Components
{
    [CustomEditor(typeof(GifImage))]
    public class GifImageEditor : GifViewerEditor<GifImage>
    {
        SerializedProperty imageProperty;

        protected override void OnEnable()
        {
            base.OnEnable();
            imageProperty = serializedObject.FindProperty("image");
        }

        protected override void OnCustomInspectorGUI(GifImage gifImage)
        {
            if (gifImage.Image == null)
                gifImage.Image = gifImage.GetComponent<Image>();

            Texture2D sourceTexture = (Texture2D)sourceFieldInfo.GetValue(gifImage);

            // Ensure the sprite is set
            if (GUI.changed && sourceTexture != null)
                gifImage.Image.sprite = gifImage.Texture2DToSprite(sourceTexture);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
