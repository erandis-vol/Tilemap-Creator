using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace TMC.Core
{
    /// <summary>
    /// Encapsulates a GDI+ bitmap, providing direct access to its pixel data.
    /// </summary>
    public sealed class DirectBitmap : IDisposable
    {
        private bool isDisposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectBitmap"/> class with the specified size.
        /// </summary>
        /// <param name="width">The width, in pixels, of the <see cref="DirectBitmap"/>.</param>
        /// <param name="height">The height, in pixels, of the <see cref="DirectBitmap"/>.</param>
        public DirectBitmap(int width, int height)
        {
            Width  = width;
            Height = height;
            Pixels = new int[width * height];
            Handle = GCHandle.Alloc(Pixels, GCHandleType.Pinned);
            Bitmap = new Bitmap(
                width,
                height,
                width * 4,
                PixelFormat.Format32bppPArgb,
                Handle.AddrOfPinnedObject()
            );
        }

        /// <summary>
        /// Creates a new <see cref="DirectBitmap"/> from the specified <see cref="Image"/>.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        public static DirectBitmap FromImage(Image image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            var fb = new DirectBitmap(image.Width, image.Height);
            using (var g = fb.CreateGraphics())
            {
                g.DrawImage(image, 0, 0, image.Width, image.Height);
            }
            return fb;
        }

        ~DirectBitmap()
        {
            Dispose(false);
        }

        #region Methods

        /// <summary>
        /// Releases all resources used by this <see cref="DirectBitmap"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                Bitmap.Dispose();
                Handle.Free();

                isDisposed = true;
            }
        }

        /// <summary>
        /// Returns the specified pixel.
        /// </summary>
        /// <param name="x">The x-coordinate of the pixel.</param>
        /// <param name="y">The y-coordinate of the pixel.</param>
        /// <returns>The color of the specified pixel.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="x"/> or <paramref name="y"/> is outside the bounds of the image.
        /// </exception>
        public Color GetPixel(int x, int y)
        {
            if (x < 0 || x >= Width)
                throw new ArgumentOutOfRangeException(nameof(x));

            if (y < 0 || y >= Height)
                throw new ArgumentOutOfRangeException(nameof(y));

            return Color.FromArgb(Pixels[x + y * Width]);
        }

        /// <summary>
        /// Sets the specified pixel.
        /// </summary>
        /// <param name="x">The x-coordinate of the pixel.</param>
        /// <param name="y">The y-coordinate of the pixel.</param>
        /// <param name="color">The color to be set.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="x"/> or <paramref name="y"/> is outside the bounds of the image.
        /// </exception>
        public void SetPixel(int x, int y, Color color)
        {
            if (x < 0 || x >= Width)
                throw new ArgumentOutOfRangeException(nameof(x));

            if (y < 0 || y >= Height)
                throw new ArgumentOutOfRangeException(nameof(y));

            Pixels[x + y * Width] = color.ToArgb();
        }

        /// <summary>
        /// Creates a GDI+ drawing surface for the <see cref="DirectBitmap"/>.
        /// </summary>
        /// <returns></returns>
        public Graphics CreateGraphics()
        {
            return Graphics.FromImage(Bitmap);
        }

        public static implicit operator Bitmap(DirectBitmap fb) => fb.Bitmap;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the bitmap wrapped by this <see cref="DirectBitmap"/>
        /// </summary>
        private Bitmap Bitmap { get; }

        /// <summary>
        /// Gets the handle of the pixels.
        /// </summary>
        private GCHandle Handle { get; }

        /// <summary>
        /// Gets the pixels of this <see cref="DirectBitmap"/>.
        /// </summary>
        public int[] Pixels { get; }

        /// <summary>
        /// Gets the width, in pixels, of this <see cref="DirectBitmap"/>.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Gets the height, in pixels, of this <see cref="DirectBitmap"/>.
        /// </summary>
        public int Height { get; }

        #endregion
    }
}
