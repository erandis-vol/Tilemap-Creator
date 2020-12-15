using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace TilemapCreator
{
    public partial class Tileset
    {
        public const int TileSize = 8;
        private int[] _tiles;

        public Tileset(int length)
        {
            _tiles = new int[length * 64];
        }

        private Tileset(int[] tiles)
        {
            //if (tiles is null)
            //    throw new ArgumentNullException(nameof(tiles));
            //if (tiles.Length < 64 || tiles.Length % 64 != 0)
            //    throw new ArgumentException();
            Debug.Assert(tiles != null, "tile data is null");
            Debug.Assert(tiles.Length > 64, "tile data is < 64 length");
            Debug.Assert(tiles.Length % 64 == 0, "tile data is not aligned");
            _tiles = tiles;
        }

        public static (Tileset Tileset, Palette Palette) Load(string filename, TilesetFormat format, TilesetLoadOptions options)
        {
            using (var stream = File.OpenRead(filename))
            {
                return format switch
                {
                    TilesetFormat.Bmp => LoadBmp(stream),
                    TilesetFormat.Png => LoadPng(stream),
                    TilesetFormat.Gba => (LoadGba(stream, options), null),
                    _ => throw new ArgumentException("Unsupported tileset format.", nameof(format))
                };
            }
        }

        private static Tileset LoadGba(Stream stream, TilesetLoadOptions options)
        {
            if (options.BitDepth != 4 && options.BitDepth != 8)
                throw new ArgumentException("Only 4 BPP and 8 BPP tilesets are supported.", nameof(options));

            throw new NotImplementedException();
        }

        public void Save(string filename, TilesetFormat format, TilesetSaveOptions options)
        {
            throw new NotImplementedException();
        }

        public int this[int tile, int x, int y]
        {
            get => GetPixel(tile, x, y);
            set => SetPixel(tile, x, y, value);
        }

        public int GetPixel(int tile, int x, int y)
        {
            if (tile < 0 || tile >= Length)
                throw new ArgumentOutOfRangeException(nameof(tile));
            if (x < 0 || x >= 8)
                throw new ArgumentOutOfRangeException(nameof(x));
            if (y < 0 || y >= 8)
                throw new ArgumentOutOfRangeException(nameof(y));
            return _tiles[tile * 64 + x + y * 8];
        }

        public void SetPixel(int tile, int x, int y, int value)
        {
            if (tile < 0 || tile >= Length)
                throw new ArgumentOutOfRangeException(nameof(tile));
            if (x < 0 || x >= 8)
                throw new ArgumentOutOfRangeException(nameof(x));
            if (y < 0 || y >= 8)
                throw new ArgumentOutOfRangeException(nameof(y));
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Pixel cannot be set to a negative value.");
            _tiles[tile * 64 + x + y * 8] = value;
        }

#if DEBUG
        // gets the pixel data for the specified tile
        public Span<int> GetTile(int tile)
        {
            if (tile < 0 || tile >= Length)
                throw new ArgumentOutOfRangeException(nameof(tile));
            return _tiles.AsSpan(tile * 64, 64);
        }

        // sets the pixel data for the specified tile
        public void SetTile(int tile, ReadOnlySpan<int> pixels)
        {
            if (tile < 0 || tile >= Length)
                throw new ArgumentOutOfRangeException(nameof(tile));
            if (pixels.Length != 64)
                throw new ArgumentException("Span does not define 64 pixels.", nameof(pixels));
            int j = tile * 64;
            for (int i = 0; i < pixels.Length; i++)
                _tiles[j++] = pixels[i];
        }
#endif

        // determines the sizes that will result in "perfect" dimensions
        // e.g. 1024 tiles could be 32 x 32 or 24 x 48
        public IReadOnlyList<int> GetSuggestedDimensions()
        {
            var columns = new List<int>();
            var length = _tiles.Length / 64;
            for (int i = 1; i <= length; i++)
            {
                if (length % i == 0)
                    columns.Add(i);
            }
            return columns.AsReadOnly();
        }

        // gets how many unique colors are in the tileset
        public int GetColorCount()
        {
            int maxColor = 0;
            for (int i = 0; i < _tiles.Length; i++)
            {
                var color = _tiles[i];
                if (color > maxColor)
                    maxColor = color;
            }
            return maxColor + 1; // index + 1 => count
        }

        // checks whether a palette can be used for the tileset
        public bool CheckPalette(Palette palette) => GetColorCount() <= palette.Length;

        // checks whether the specified tiles match
        public unsafe bool CompareTiles(int tile1, int tile2, bool flipX, bool flipY)
        {
            if (tile1 < 0 || tile1 >= Length)
                throw new ArgumentOutOfRangeException(nameof(tile1));
            if (tile2 < 0 || tile2 >= Length)
                throw new ArgumentOutOfRangeException(nameof(tile2));
            // TODO: investigate whether spans are faster
            fixed (int* pixels1 = &_tiles[tile1 * 64], pixels2 = &_tiles[tile2 * 64])
            {
                if (flipX || flipY)
                {
                    // TODO: we could make this even faster by covering each case separately
                    for (int ay = 0; ay < 8; ay++)
                    {
                        for (int ax = 0; ax < 8; ax++)
                        {
                            var bx = flipX ? (7 - ax) : ax;
                            var by = flipY ? (7 - ay) : ay;
                            var ai = ax + ay * 8;
                            var bi = bx + by * 8;
                            if (pixels1[ai] != pixels2[bi])
                                return false;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 64; i++)
                    {
                        if (pixels1[i] != pixels2[i])
                            return false;
                    }
                }
            }
            return true;
        }

        // draws the specified tile at (x, y)
        public void DrawTile(IRenderer renderer, int tile, Palette palette, int x, int y) =>
            DrawTile(renderer, tile, palette, x, y, false, false);

        // draws the specified tile at (x, y) with flipping
        public void DrawTile(IRenderer renderer, int tile, Palette palette, int x, int y, bool flipX, bool flipY)
        {
            if (renderer is null)
                throw new ArgumentNullException(nameof(renderer));
            if (tile < 0 || tile >= Length)
                throw new ArgumentOutOfRangeException(nameof(tile));
            if (palette is null)
                throw new ArgumentNullException(nameof(palette));

            // TODO: optimize for all flip cases
            var ti = tile * 64;
            if (flipX || flipY)
            {
                for (int i = 0; i < 64; i++)
                {
                    var tx = i % 8;
                    var ty = i / 8;
                    var dx = flipX ? (7 - tx) : tx;
                    var dy = flipY ? (y - ty) : ty;
                    var index = _tiles[ti++];
                    renderer.SetPixel(x + dx, y + dy, palette[index]);
                }
            }
            else
            {
                for (int i = 0; i < 64; i++)
                {
                    var tx = i % 8;
                    var ty = i / 8;
                    var index = _tiles[ti++];
                    renderer.SetPixel(x + tx, y + ty, palette[index]);
                }
            }
        }

        public void Draw(IRenderer renderer, int columns, Palette palette) => Draw(renderer, columns, palette, 0, 0);

        public void Draw(IRenderer renderer, int columns, Palette palette, int x, int y)
        {
            if (renderer is null)
                throw new ArgumentNullException(nameof(renderer));
            if (columns <= 0)
                throw new ArgumentOutOfRangeException(nameof(columns));
            if (palette is null)
                throw new ArgumentNullException(nameof(palette));

            var tileCount = _tiles.Length / 64;
            for (int i = 0; i < tileCount; i++)
            {
                var tx = i % columns;
                var ty = i / columns;
                DrawTile(renderer, i, palette, x + tx * 8, y + ty * 8);
            }
        }

        /// <summary>
        /// Gets the total number of tiles in the tileset.
        /// </summary>
        public int Length => _tiles.Length / 64;
    }

    public struct TilesetLoadOptions
    {
        public int BitDepth { get; set; }
    }

    public struct TilesetSaveOptions
    {
        public int Width { get; set; }

        public int BitDepth { get; set; }
    }
}
