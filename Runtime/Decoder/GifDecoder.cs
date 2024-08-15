using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Kiraio.UniGify.Utility;
using SkiaSharp;
using UnityEngine;

namespace Kiraio.UniGify.Decoder
{
    public class GifDecoder
    {
        public struct GifFrame
        {
            /// <summary>
            /// The frame image.
            /// </summary>
            public Texture2D Texture;

            /// <summary>
            /// The frame image duration in milliseconds.
            /// </summary>
            public int Duration;
        }

        /// <summary>
        /// Decode .gif from a <paramref name="buffer"/>.
        /// </summary>
        /// <param name="buffer">The <paramref name="buffer"/>data.</param>
        /// <param name="length">How much frame we should get. <= 0 for all frames.</param>
        /// <returns></returns>
        public List<GifFrame> Decode(byte[] buffer, int length = 0)
        {
            List<GifFrame> gifFrames = new List<GifFrame>();

            using (SKCodec codec = SKCodec.Create(new SKMemoryStream(buffer)))
            {
                if (codec == null)
                {
                    Debug.LogError("Failed to create SKCodec from byte array! Invalid .gif data.");
                    return gifFrames;
                }

                SKBitmap bitmap = new SKBitmap(codec.Info.Width, codec.Info.Height);
                SKImageInfo info = bitmap.Info;

                for (int i = 0; i < (length <= 0 ? codec.FrameCount : length); i++)
                {
                    // Get the frame information for the current frame
                    codec.GetFrameInfo(i, out SKCodecFrameInfo frameInfo);

                    // Decode the frame into the bitmap
                    SKCodecResult result = codec.GetPixels(
                        info,
                        bitmap.GetPixels(),
                        new SKCodecOptions(i)
                    );

                    if (result == SKCodecResult.Success || result == SKCodecResult.IncompleteInput)
                    {
                        // Convert the SKBitmap to a Texture2D for Unity
                        Texture2D texture = new Texture2D(
                            bitmap.Width,
                            bitmap.Height,
                            TextureFormat.RGBA32,
                            false
                        );
                        texture.SetPixels32(ColorHelper.ConvertSKBitmapToColor32(bitmap));
                        texture.Apply();

                        // Add the frame with its duration to the list
                        gifFrames.Add(
                            new GifFrame { Texture = texture, Duration = frameInfo.Duration }
                        );
                    }
                    else
                    {
                        Debug.LogError($"Failed to decode frame {i} with result {result}");
                    }
                }
            }

            return gifFrames;
        }

        /// <summary>
        /// Decode the .gif from <paramref name="filePath"/>.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public List<GifFrame> Decode(string filePath)
        {
            return Decode(File.ReadAllBytes(filePath));
        }
    }
}
