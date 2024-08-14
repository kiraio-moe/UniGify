using UnityEngine;

namespace Kiraio.UniGify.Components
{
    [AddComponentMenu("UniGify/GIF Sprite Renderer")]
    [RequireComponent(typeof(SpriteRenderer))]
    public class GifSpriteRenderer : GifViewer
    {
        [SerializeField]
        SpriteRenderer spriteRenderer;

        public SpriteRenderer Renderer
        {
            get => spriteRenderer;
            set => spriteRenderer = value;
        }

        protected override void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            base.Awake();
        }

        protected override void UpdateSprite(int frameIndex)
        {
            if (frameIndex >= 0 && frameIndex < Frames.Count)
                spriteRenderer.sprite = Texture2DToSprite(Frames[frameIndex].Texture);
        }
    }
}
