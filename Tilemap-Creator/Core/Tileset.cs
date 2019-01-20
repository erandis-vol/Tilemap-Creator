using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace TMC.Core
{
    public class Tileset
    {
        public const int TileSize = 8;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tileset"/> class from the specified source image.
        /// </summary>
        /// <param name="source">The source image.</param>
        public Tileset(Bitmap source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (source.Width < 8 || source.Height < 8)
                throw new ArgumentException("Image must be at least 8x8 pixels.", nameof(source));

            // Create the tiles
            var width = source.Width / 8;
            var height = source.Height / 8;
            Tiles = new Tile[width * height];

            // Copy image data from source
            using (var fb = DirectBitmap.FromImage(source))
            {
                // Create the palette
                Palette = Palette.Create(source);

                // Create the tiles
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        ref var tile = ref Tiles[x + y * width];

                        for (int j = 0; j < 8; j++)
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                var color = Color.FromArgb(fb.Pixels[(x * 8 + i) + (y * 8 + j) * fb.Width]);

                                // Find the color index
                                var index = Palette.IndexOf(color);
                                if (index < 0)
                                    throw new IndexOutOfRangeException();

                                // Copy to the tile
                                tile[i, j] = index;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Initialzies a new instance of the <see cref="Tileset"/> with the specified tile array.
        /// </summary>
        /// <param name="tiles">The tiles.</param>
        protected Tileset(Tile[] tiles, Palette palette)
        {
            Tiles = tiles;
            Palette = palette;
        }

        #region Methods

        /// <summary>
        /// Gets the specified tile.
        /// </summary>
        /// <param name="index">The index of the tile.</param>
        /// <returns></returns>
        public ref Tile this[int index] => ref Tiles[index];

        /// <summary>
        /// Creates a new <see cref="Tileset"/> from the specified source image.
        /// </summary>
        /// <param name="bmp">The source image.</param>
        /// <param name="allowFlipping">Determines whether tile flipping is permitted.</param>
        public static (Tileset Tileset, Tilemap Tilemap) Create(Bitmap bmp, bool allowFlipping)
        {
            var width = bmp.Width / 8;
            var height = bmp.Height / 8;

            // Create initial tileset (with all tiles)
            var tileset = new Tileset(bmp);

            // Create empty tilemap
            var tilemap = new Tilemap(width, height);

            // Define the first tile
            tilemap[0] = new TilemapEntry();

            // Scan the tileset for repeated tiles
            var tiles = new List<Tile> { tileset[0] };
            var current = 1;

            for (int i = 1; i < tileset.Length; i++)
            {
                ref var tile = ref tileset[i];

                // The current tile
                var index = current;
                var flipX = false;
                var flipY = false;

                // Compare the tile against all unique tiles
                for (int j = 0; j < tiles.Count; j++)
                {
                    var other = tiles[j];

                    // Test tile configurations
                    if (tile.Equals(ref other, false, false))
                    {
                        index = j;
                        break;
                    }
                    else if (allowFlipping)
                    {
                        if (tile.Equals(ref other, true, false))
                        {
                            index = j;
                            flipX = true;
                            break;
                        }

                        if (tile.Equals(ref other, false, true))
                        {
                            index = j;
                            flipY = true;
                            break;
                        }

                        if (tile.Equals(ref other, true, true))
                        {
                            index = j;
                            flipX = true;
                            flipY = true;
                            break;
                        }
                    }
                }

                // Update the tilemap
                tilemap[i] = new TilemapEntry((short)index, flipX, flipY);

                // Update the tileset
                if (index >= current)
                {
                    tiles.Add(tile);
                    current++;
                }
            }

            // The process is now finished
            return (new Tileset(tiles.ToArray(), tileset.Palette), tilemap);
        }

        /// <summary>
        /// Creates a new <see cref="DirectBitmap"/> representing all tiles.
        /// </summary>
        /// <param name="columns">The number of columns in a single row of tiles.</param>
        /// <returns></returns>
        public DirectBitmap ToImage(int columns)
        {
            if (columns <= 0)
                throw new ArgumentOutOfRangeException(nameof(columns));

            var rows = (Tiles.Length / columns) + (Tiles.Length % columns > 0 ? 1 : 0);
            var fb = new DirectBitmap(columns * 8, rows * 8);

            for (int i = 0; i < Tiles.Length; i++)
            {
                // Get the destination
                var x = i % columns;
                var y = i / columns;

                // Get the tile to draw
                ref var tile = ref Tiles[i];

                // Draw the tile
                for (int j = 0; j < 8; j++)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        fb.SetPixel(x * 8 + k, y * 8 + j, Palette[tile[k, j]]);
                    }
                }
            }

            return fb;
        }

        public void Save(TilesetFileOptions tilesetFileOptions)
        {
            var filename = tilesetFileOptions.FileName;
            var format   = tilesetFileOptions.Format;
            var columns  = tilesetFileOptions.Columns;

            switch (tilesetFileOptions.Format)
            {
                case TilesetFormat.BMP:
                    SaveBMP(tilesetFileOptions.FileName, tilesetFileOptions.Columns);
                    break;

                case TilesetFormat.BIN:
                    SaveBIN(tilesetFileOptions.FileName);
                    break;

                default:
                    throw new NotSupportedException($"Tileset format {format} is not supported for saving.");
            }
        }

        private void SaveBMP(string filename, int columns)
        {
            var width = columns * 8;
            var height = (Tiles.Length / columns + (Tiles.Length % columns > 0 ? 1 : 0)) * 8;

            // Creates a pixel buffer for the tiles
            var pixels = new int[width * height];
            for (int i = 0; i < Tiles.Length; i++)
            {
                ref var tile = ref Tiles[i];

                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        pixels[(x + i % columns * 8) + (y + i / columns * 8) * width] = tile[x, y];
                    }
                }
            }

            using (var bw = new BinaryWriter(File.Create(filename)))
            {
                if (Palette.Length <= 16)
                {
                    var rowSize = ((4 * width + 31) / 32) * 4;
                    var pixelSize = rowSize * height;
                    var paddingSize = rowSize % 4;

                    // Bitmap file header
                    bw.Write((ushort)0x4D42);               // 'BM'
                    bw.Write(pixelSize + (16 * 4) + 54);    // filesize = header + color table + pixel data
                    bw.Write(0x293A);                       // embed a friendly message
                    bw.Write(54 + (16 * 4));                // offset of pixel data

                    // BITMAPINFOHEADER
                    bw.Write(40);               // header size = 40 bytes
                    bw.Write(width);            // width in pixels
                    bw.Write(height);           // height in pixels
                    bw.Write((ushort)1);        // 1 color plane
                    bw.Write((ushort)4);        // 8 bpp
                    bw.Write(0);                // no compression
                    bw.Write(pixelSize);        // size of raw data + padding
                    bw.Write(2835);             // print resoltion of image (~72 dpi)
                    bw.Write(2835);             //
                    bw.Write(16);               // color table size, 16 because MUST be 2^n
                    bw.Write(0);                // all colors are important

                    // color table
                    for (int i = 0; i < 16; i++)
                    {
                        var color = (i < Palette.Length ? Palette[i] : Color.Black);

                        bw.Write(color.B);
                        bw.Write(color.G);
                        bw.Write(color.R);
                        bw.Write(byte.MaxValue);
                    }

                    // pixel data
                    for (int y = height - 1; y >= 0; y--)
                    {
                        // copy colors for this row
                        for (int x = 0; x < width; x += 2)
                        {
                            bw.Write((byte)((pixels[x + y * width] << 4) | pixels[x + 1 + y * width]));
                        }

                        // include the last pixel in odd number widths
                        if (width % 2 != 0)
                        {
                            bw.Write((byte)(pixels[(width - 1) + y * width] << 4));
                        }

                        // pad end of row with 0's
                        for (int x = 0; x < paddingSize; x++)
                        {
                            bw.Write(byte.MinValue);
                        }
                    }
                }
                else if (Palette.Length <= 256)
                {
                    var rowSize = ((8 * width + 31) / 32) * 4;
                    var pixelSize = rowSize * height;
                    var paddingSize = rowSize % 4;

                    // Bitmap file header
                    bw.Write((ushort)0x4D42);               // 'BM'
                    bw.Write(pixelSize + (256 * 4) + 54);   // filesize = header + color table + pixel data
                    bw.Write(0x293A);                       // embed a friendly message
                    bw.Write(54 + (256 * 4));               // offset of pixel data

                    // BITMAPINFOHEADER
                    bw.Write(40);               // header size = 40 bytes
                    bw.Write(width);            // width in pixels
                    bw.Write(height);           // height in pixels
                    bw.Write((ushort)1);        // 1 color plane
                    bw.Write((ushort)8);        // 8 bpp
                    bw.Write(0);                // no compression
                    bw.Write(pixelSize);        // size of raw data + padding
                    bw.Write(2835);             // print resoltion of image (~72 dpi)
                    bw.Write(2835);             //
                    bw.Write(256);              // color table size, 256 because MUST be 2^n
                    bw.Write(0);                // all colors are important

                    // color table
                    for (int i = 0; i < 256; i++)
                    {
                        var color = (i < Palette.Length ? Palette[i] : Color.Black);

                        bw.Write(color.B);
                        bw.Write(color.G);
                        bw.Write(color.R);
                        bw.Write(byte.MaxValue);
                    }

                    // pixel data
                    for (int y = height - 1; y >= 0; y--)
                    {
                        // copy colors for this row
                        for (int x = 0; x < width; x++)
                        {
                            ref var tile = ref Tiles[x + y * columns];
                            bw.Write((byte)tile[0, 0]);
                        }

                        // pad end of row with 0's
                        for (int x = 0; x < paddingSize; x++)
                        {
                            bw.Write(byte.MinValue);
                        }
                    }
                }
                else
                {
                    var rowSize = ((24 * width + 31) / 32) * 4;
                    var pixelSize = rowSize * height;
                    var paddingSize = rowSize % 4;

                    // Bitmap file header
                    bw.Write((ushort)0x4D42);   // 'BM'
                    bw.Write(pixelSize + 54);   // filesize = header + pixel data
                    bw.Write(0x293A);           // embed a friendly message
                    bw.Write(54);               // offset of pixel data

                    // BITMAPINFOHEADER
                    bw.Write(40);               // header size = 40 bytes
                    bw.Write(width);            // width in pixels
                    bw.Write(height);           // height in pixels
                    bw.Write((ushort)1);        // 1 color plane
                    bw.Write((ushort)24);       // 24 bpp
                    bw.Write(0);                // no compression
                    bw.Write(pixelSize);        // size of raw data + padding
                    bw.Write(2835);             // print resoltion of image (~72 dpi)
                    bw.Write(2835);             //
                    bw.Write(0);                // empty color table
                    bw.Write(0);                // all colors are important

                    // Pixel data
                    for (int y = height - 1; y >= 0; y--)
                    {
                        // Copy colors for this row
                        for (int x = 0; x < width; x++)
                        {
                            var color = Palette[pixels[x + y * width]];
                            bw.Write(color.B);
                            bw.Write(color.G);
                            bw.Write(color.R);
                        }

                        // Pad end of row with 0's
                        for (int x = 0; x < paddingSize; x++)
                        {
                            bw.Write(byte.MinValue);
                        }
                    }
                }
            }
        }

        private void SaveBIN(string filename)
        {
            if (Palette.Length <= 16)
            {
                File.WriteAllBytes(filename, BitDepth.Encode4(Tiles));
            }
            else if (Palette.Length <= 256)
            {
                File.WriteAllBytes(filename, BitDepth.Encode8(Tiles));
            }
            else
            {
                throw new InvalidOperationException("Tileset has too many colors to save.");
            }
        }

        /// <summary>
        /// Returns an array of column values that will result in a perfect tileset.
        /// </summary>
        /// <returns></returns>
        public int[] GetPerfectColumns()
        {
            var columns = new List<int>();

            for (int i = 1;i <= Tiles.Length; i++)
            {
                if (Tiles.Length % i == 0) columns.Add(i);
            }

            return columns.ToArray();
        }

        /// <summary>
        /// Reduces the number of colors to no more than the amount specified.
        /// </summary>
        /// <param name="colorCount">The maximum number of colors.</param>
        public void ReduceColors(int colorCount)
        {
            if (Palette == null || Palette.Length <= colorCount)
                return;

            // Create the quantizer and add the palette
            var quantizer = new OctreeQuantizer();
            quantizer.AddColors(Palette);

            // Create the reduced palette
            var reducedPalette = quantizer.GetPalette(colorCount);

            // Update all tiles to reflect the reduced colors
            for (int i = 0; i < Tiles.Length; i++)
            {
                ref var tile = ref Tiles[i];

                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        // Get the original color
                        var pixel = Palette[tile[x, y]];

                        // Get the closets match from the quantizer
                        var index = quantizer.GetPaletteIndex(pixel);

                        // Update the pixel
                        tile[x, y] = index;
                    }
                }
            }

            // Replace the old palette
            Palette = new Palette(reducedPalette);
        }

        /// <summary>
        /// Swaps the colors by the order specified in a new palette.
        /// </summary>
        /// <param name="swapped">Specifies the order of the new palette.</param>
        /// <remarks>
        /// In cases where palettes have repeated colors, this can have the side-effect of
        /// "optimizing" the colors to the first palette index that matches.
        /// </remarks>
        public void SwapColors(Palette swapped)
        {
            if (swapped == null)
                throw new ArgumentNullException(nameof(swapped));

            if (swapped.Length != Palette.Length)
                throw new ArgumentException();

            // Create a map between palettes
            int[] map = new int[swapped.Length];
            for (int i = 0; i < swapped.Length; i++)
            {
                int index = swapped.IndexOf(Palette[i]);
                if (index == -1)
                {
                    throw new ArgumentException("Swapped palette does not contain all colors.", nameof(swapped));
                }

                map[i] = index;
            }

            // Update all tiles using map
            for (int i = 0; i < Tiles.Length; i++)
            {
                ref Tile tile = ref Tiles[i];

                for (int j = 0; j < 64; j++)
                {
                    tile[j] = map[tile[j]];
                }
            }

            // Replace the palette
            Palette = new Palette(swapped);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of tiles.
        /// </summary>
        public int Length => Tiles.Length;

        /// <summary>
        /// Gets the tiles.
        /// </summary>
        public Tile[] Tiles { get; }

        /// <summary>
        /// Gets the palette.
        /// </summary>
        public Palette Palette { get; private set; }

        #endregion
    }
}
