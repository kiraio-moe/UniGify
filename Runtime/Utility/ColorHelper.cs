using SkiaSharp;
using UnityEngine;

namespace Kiraio.UniGify.Utility
{
    public static class ColorHelper
    {
        public static Color32[] ConvertSKBitmapToColor32(SKBitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            Color32[] colors = new Color32[width * height];

            SKPixmap pixmap = bitmap.PeekPixels();
            byte[] pixelData = pixmap.GetPixelSpan().ToArray();

            // SkiaSharp stores data in BGRA format; Unity uses RGBA format
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int srcIndex = (y * width + x) * 4;

                    byte b = pixelData[srcIndex];
                    byte g = pixelData[srcIndex + 1];
                    byte r = pixelData[srcIndex + 2];
                    byte a = pixelData[srcIndex + 3];

                    // Handle premultiplied alpha by dividing the RGB values by the alpha
                    if (a > 0)
                    {
                        r = (byte)((r * 255) / a);
                        g = (byte)((g * 255) / a);
                        b = (byte)((b * 255) / a);
                    }

                    // Ensure the alpha (transparency) channel is applied correctly
                    colors[(height - y - 1) * width + x] = new Color32(r, g, b, a);
                }
            }

            return colors;
        }
    }
}
