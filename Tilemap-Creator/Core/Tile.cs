using System;

namespace TMC.Core
{
    /// <summary>
    /// Represents an 8x8 array of pixel data.
    /// </summary>
    public struct Tile
    {
        /// <summary>
        /// The pixel data.
        /// </summary>
        private int[] pixels;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tile"/> struct by copying pixels from the specified array.
        /// </summary>
        /// <param name="pixels">The pixels to be copied.</param>
        public Tile(int[] pixels)
        {
            if (pixels == null)
                throw new ArgumentNullException(nameof(pixels));

            if (pixels.Length != 64)
                throw new ArgumentException("Expected 64 bits of pixel data.", nameof(pixels));

            this.pixels = (int[])pixels.Clone();
        }

        /// <summary>
        /// Gets or sets the specified pixel value.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int this[int x, int y]
        {
            get
            {
                if (x < 0 || x >= 8)
                    throw new ArgumentOutOfRangeException(nameof(x));

                if (y < 0 || y >= 8)
                    throw new ArgumentOutOfRangeException(nameof(y));

                if (pixels == null)
                {
                    return 0;
                }

                return pixels[x + y * 8];
            }
            set
            {
                if (x < 0 || x >= 8)
                    throw new ArgumentOutOfRangeException(nameof(x));

                if (y < 0 || y >= 8)
                    throw new ArgumentOutOfRangeException(nameof(y));

                if (pixels == null)
                {
                    pixels = new int[64];
                }

                pixels[x + y * 8] = value;
            }
        }

        /// <summary>
        /// Determines whether this tile value is equivalent to the specified tile value with the specified flipping.
        /// </summary>
        /// <param name="other">The tile to compare to.</param>
        /// <param name="flipX">Determines whether <paramref name="other"/> is to be flipped from left to right.</param>
        /// <param name="flipY">Determines whether <paramref name="other"/> is to be flipped from top to bottom.</param>
        /// <returns><c>true</c> if the tiles are equivalent; otherwise, <c>false</c>.</returns>
        public unsafe bool CompareTo(ref Tile other, bool flipX = false, bool flipY = false)
        {
            fixed (int* src = &pixels[0])
            fixed (int* dst = &other.pixels[0])
            {
                for (int srcY = 0; srcY < 8; srcY++)
                {
                    for (int srcX = 0; srcX < 8; srcX++)
                    {
                        var dstX = flipX ? (7 - srcX) : srcX;
                        var dstY = flipY ? (7 - srcY) : srcY;

                        if (src[srcX + srcY * 8] != dst[dstX + dstY * 8])
                            return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Gets the pixel data.
        /// </summary>
        public int[] Pixels => pixels ?? (pixels = new int[64]);
    }
}
