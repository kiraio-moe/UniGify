using System.Collections.Generic;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using Kiraio.UniGify.Decoder;
using UnityEngine;

namespace Kiraio.UniGify.Components
{
    public abstract class GifViewer : MonoBehaviour
    {
        [SerializeField]
        Texture2D m_Source;

        [SerializeField]
        bool m_PlayOnStart = true;

        [SerializeField]
        string sourcePath;
        int currentFrame;
        List<GifDecoder.GifFrame> frames = new List<GifDecoder.GifFrame>();
        CancellationTokenSource cts;

        /// <summary>
        /// Path to the .gif file.
        /// </summary>
        public string SourcePath
        {
            get => sourcePath;
            set => sourcePath = value;
        }

        /// <summary>
        /// Current frame that's being displayed.
        /// </summary>
        public int CurrentFrame
        {
            get => currentFrame;
            set => currentFrame = value;
        }

        /// <summary>
        /// Decoded GIF frames with their metadata information.
        /// </summary>
        public List<GifDecoder.GifFrame> Frames
        {
            get => frames;
            set => frames = value;
        }

        /// <summary>
        /// Play the GIF on start?
        /// </summary>
        /// <value></value>
        public bool PlayOnStart
        {
            get => m_PlayOnStart;
            set => m_PlayOnStart = value;
        }

        protected virtual void Awake()
        {
            Frames = new GifDecoder().Decode(SourcePath);
        }

        protected virtual async void Start()
        {
            if (PlayOnStart)
                await StartSpriteUpdateAsync();
        }

        protected virtual void OnDestroy()
        {
            cts?.Cancel();
        }

        async UniTask StartSpriteUpdateAsync()
        {
            cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            while (!token.IsCancellationRequested)
            {
                if (Frames.Count > 0)
                {
                    UpdateSprite(CurrentFrame);
                    CurrentFrame = (CurrentFrame + 1) % Frames.Count;

                    await UniTask.Delay(Frames[CurrentFrame].Duration, cancellationToken: token);
                }
            }
        }

        /// <summary>
        /// This method will be called by the base class to update the sprite.
        /// Derived classes should override this method to update their specific components.
        /// </summary>
        /// <param name="frameIndex">The index of the frame to display.</param>
        protected abstract void UpdateSprite(int frameIndex);

        /// <summary>
        /// Create Sprite from Texture2D.
        /// </summary>
        /// <param name="texture"></param>
        /// <returns></returns>
        public Sprite Texture2DToSprite(Texture2D texture)
        {
            return Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f)
            );
        }
    }
}
