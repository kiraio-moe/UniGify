using UnityEngine;
using UnityEngine.UI;

namespace Kiraio.UniGify.Components
{
    [AddComponentMenu("UniGify/GIF Image")]
    [RequireComponent(typeof(Image))]
    public class GifImage : GifViewer
    {
        [SerializeField]
        Image image;

        /// <summary>
        /// The Image component.
        /// </summary>
        /// <value></value>
        public Image Image
        {
            get => image;
            set => image = value;
        }

        protected override void Awake()
        {
            image = GetComponent<Image>();
            base.Awake();
        }

        protected override void UpdateSprite(int frameIndex)
        {
            if (frameIndex >= 0 && frameIndex < Frames.Count)
                image.sprite = Texture2DToSprite(Frames[frameIndex].Texture);
        }
    }
}
