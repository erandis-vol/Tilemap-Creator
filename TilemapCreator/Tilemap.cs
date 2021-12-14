using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TilemapCreator
{
    // maps tiles to create an image
    public class Tilemap
    {
        private TilemapTile[] _tiles;

        public Tilemap(int width, int height)
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException(nameof(width));
            if (height <= 0)
                throw new ArgumentOutOfRangeException(nameof(height));
            Width = width;
            Height = height;
            _tiles = new TilemapTile[width * height];
        }

        public ref TilemapTile this[int index] => ref _tiles[index];

        public ref TilemapTile this[int x, int y]
        {
            get
            {
                if (x < 0 || x >= Width)
                    throw new ArgumentOutOfRangeException(nameof(x));
                if (y < 0 || y >= Height)
                    throw new ArgumentOutOfRangeException(nameof(y));
                return ref _tiles[x + y * Width];
            }
        }

        public void SetTile(int x, int y, int tile, bool flipX, bool flipY)
        {
            if (x < 0 || x >= Width)
                throw new ArgumentOutOfRangeException(nameof(x));
            if (y < 0 || y >= Height)
                throw new ArgumentOutOfRangeException(nameof(y));
            var index = x + y * Width;
            Debug.Assert(index >= 0 && index < _tiles.Length);
            _tiles[index].Tile = tile;
            _tiles[index].FlipX = flipX;
            _tiles[index].FlipY = flipY;
        }

        public void SetPalette(int x, int y, byte palette)
        {
            if (x < 0 || x >= Width)
                throw new ArgumentOutOfRangeException(nameof(x));
            if (y < 0 || y >= Height)
                throw new ArgumentOutOfRangeException(nameof(y));
            _tiles[x + y * Width].Palette = palette;
        }

        // resizes the tilemap, copying as much old information as can be copied
        public void Resize(int newWidth, int newHeight)
        {
            if (newWidth <= 0 || newHeight <= 0)
                throw new ArgumentOutOfRangeException(newWidth <= 0 ? nameof(newWidth) : nameof(newHeight));

            var temp = new TilemapTile[newWidth * newHeight];
            var copyWidth = Math.Min(Width, newWidth);
            var copyHeight = Math.Min(Height, newHeight);

            for (int y = 0; y < copyHeight; y++)
            {
                for (int x = 0; x < copyWidth; x++)
                {
                    temp[x + y * newWidth] = _tiles[x + y * Width];
                }
            }

            _tiles = temp;
            Width = newWidth;
            Height = newHeight;
        }

        // draws the tilemap to the specified renderer
        public void Draw(IRenderer renderer, Tileset tileset, Palette palette)
        {
            Debug.Assert(tileset.CheckPalette(palette), "palette is incompatible with tileset");
            for (int ay = 0; ay < Height; ay++)
            {
                for (int ax = 0; ax < Width; ax++)
                {
                    ref var tile = ref _tiles[ax + ay * 8];
                    var tileX = ax << 3;
                    var tileY = ay << 3;
                    tileset.DrawTile(renderer, tile.Tile, palette, tileX, tileY, tile.FlipX, tile.FlipY);
                }
            }
        }

        public int Width { get; private set; }

        public int Height { get; private set; }
    }
}
