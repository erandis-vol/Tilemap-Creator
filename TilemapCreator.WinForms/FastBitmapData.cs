using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace TilemapCreator
{
    // provides a fast way to access bitmap image data
    public unsafe struct FastBitmapData : IDisposable, IRenderer
    {
        private readonly Bitmap _bitmap;
        private readonly BitmapData _bitmapData;
        private readonly int* _scan0;
        private readonly int _stride;
        private bool _isLocked;

        public FastBitmapData(Bitmap bitmap, Rectangle rect, ImageLockMode flags)
        {
            if (bitmap is null)
                throw new ArgumentNullException(nameof(bitmap));
            if (Image.GetPixelFormatSize(bitmap.PixelFormat) != 32)
                throw new ArgumentException("Unsupported image format.", nameof(bitmap));

            _bitmap = bitmap;
            _bitmapData = bitmap.LockBits(rect, flags, bitmap.PixelFormat);
            _scan0 = (int*)_bitmapData.Scan0;
            _stride = _bitmapData.Stride / 4;
            _isLocked = true;
            //Debug.WriteLine("FastBitmapData: Locking image.");
        }

        public void Dispose() => Unlock();

        public void Unlock()
        {
            if (_isLocked)
            {
                //Debug.WriteLine("FastBitmapData: Unlocking image.");
                _bitmap.UnlockBits(_bitmapData);
            }
        }

        public unsafe int GetPixelInt32(int x, int y)
        {
            if (!_isLocked)
                throw new InvalidOperationException("The bitmap has not been locked.");
            return *(_scan0 + x + y * _stride);
        }

        public Color GetPixel(int x, int y) => Color.FromArgb(GetPixelInt32(x, y));

        public unsafe void SetPixel(int x, int y, int color)
        {
            if (!_isLocked)
                throw new InvalidOperationException("The bitmap has not been locked.");
            *(_scan0 + x + y * _stride) = color;
        }

        public void SetPixel(int x, int y, Color color) => SetPixel(x, y, color.ToArgb());

        public void SetPixel(int x, int y, Bgr555 color) => SetPixel(x, y, color.ToRgb32());
    }

    public static class BitmapExtensions
    {
        public static FastBitmapData FastLock(this Bitmap bmp)
        {
            return new FastBitmapData(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite);
        }

        public static FastBitmapData FastLock(this Bitmap bmp, ImageLockMode flags)
        {
            return new FastBitmapData(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height), flags);
        }

        public static Bitmap ChangeFormat(this Bitmap bmp, PixelFormat newFormat)
        {
            //using (var temp = new Bitmap(bmp))
            //{
            //    return temp.Clone(new Rectangle(0, 0, temp.Width, temp.Height), newFormat);
            //}

            // seems that Bitmap.Clone() does not work correctly?
            var clone = new Bitmap(bmp.Width, bmp.Height, newFormat);
            using (var g = Graphics.FromImage(clone))
                g.DrawImage(bmp, new Rectangle(0, 0, clone.Width, clone.Height));
            return clone;
        }
    }
}
