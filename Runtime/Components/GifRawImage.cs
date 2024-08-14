using UnityEngine;
using UnityEngine.UI;

namespace Kiraio.UniGify.Components
{
    [AddComponentMenu("UniGify/GIF Raw Image")]
    [RequireComponent(typeof(RawImage))]
    public class GifRawImage : GifViewer
    {
        [SerializeField]
        RawImage rawImage;

        /// <summary>
        /// The Raw Image component.
        /// </summary>
        /// <value></value>
        public RawImage RawImage
        {
            get => rawImage;
            set => rawImage = value;
        }

        protected override void Awake()
        {
            rawImage = GetComponent<RawImage>();
            base.Awake();
        }

        protected override void UpdateSprite(int frameIndex)
        {
            if (frameIndex >= 0 && frameIndex < Frames.Count)
                rawImage.texture = Frames[frameIndex].Texture;
        }
    }
}
