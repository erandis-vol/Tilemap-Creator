using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMC
{
    public class Tileset : IDisposable
    {
        public const int TileSize = 8;

        Sprite[] tiles;

        public Tileset(Sprite source)
        {
            // convert a single sprite into an array of 8x8 sprites
            if (source.Width < TileSize || source.Height < TileSize)
                throw new Exception($"Sprite must be at least {TileSize}x{TileSize} pixels!");

            // copy tile Sprites from source Sprite
            int tiledWidth = source.Width / TileSize;
            int tiledHeight = source.Height / TileSize;
            tiles = new Sprite[tiledWidth * tiledHeight];

            int i = 0;
            for (int y = 0; y < tiledHeight; y++)
            {
                for (int x = 0; x < tiledWidth; x++)
                {
                    tiles[i++] = new Sprite(source,
                        new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize));
                }
            }
        }

        public Tileset(Sprite[] tiles)
        {
            this.tiles = tiles;
        }

        public void Dispose()
        {
            if (tiles != null)
            {
                for (int i = 0; i < tiles.Length; i++) tiles[i]?.Dispose();
            }
        }

        public Sprite this[int index]
        {
            get { return tiles[index]; }
        }

        /// <summary>
        /// Creates a Tileset, removing all repeated tiles from the source Sprite.
        /// </summary>
        public static void Create(Sprite source, bool allowFlipping, out Tileset tileset, out Tilemap tilemap)
        {
            // copy tile Sprites from source Sprite
            var tiledWidth = source.Width / TileSize;
            var tiledHeight = source.Height / TileSize;
            var tiles = new Sprite[tiledWidth * tiledHeight];

            // create base tileset
            int t = 0;
            for (int y = 0; y < tiledHeight; y++)
            {
                for (int x = 0; x < tiledWidth; x++)
                {
                    tiles[t++] = new Sprite(source,
                        new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize));
                }
            }

            // init tilemap
            tilemap = new Tilemap(tiledWidth, tiledHeight);
            tilemap[0] = new Tile();

            // remove all repeated tiles
            // the first tile is always preserved
            var uniqueTiles = new List<Sprite> { tiles[0] };
            int current = 1;

            for (int i = 1; i < tiles.Length; i++)
            {
                var tile = tiles[i];

                // info for flipping
                var index = current;
                var flipX = false;
                var flipY = false;

                // compare against all unique tiles to check for repeats
                // gets slower as the tileset grows ;(
                for (int j = 0; j < uniqueTiles.Count; j++)
                {
                    var otherTile = uniqueTiles[j];

                    // compare normally
                    if (tile.Compare(otherTile))
                    {
                        index = j;
                        break;
                    }

                    // compare flipped
                    if (allowFlipping)
                    {
                        if (tile.Compare(otherTile, true, false))
                        {
                            index = j;
                            flipX = true;
                            break;
                        }
                        if (tile.Compare(otherTile, false, true))
                        {
                            index = j;
                            flipY = true;
                            break;
                        }
                        if (tile.Compare(otherTile, true, true))
                        {
                            index = j;
                            flipX = true;
                            flipY = true;
                            break;
                        }
                    }
                }

                // modify tilemap
                tilemap[i] = new Tile { TilesetIndex = index, FlipX = flipX, FlipY = flipY };

                // destroy non-unique tiles
                if (index < current)
                {
                    tiles[i].Dispose();
                    tiles[i] = null;
                }
                // remember unique tiles
                else
                {
                    uniqueTiles.Add(tile);
                    current++;
                }
            }

            tileset = new Tileset(uniqueTiles.ToArray());
        }

        /// <summary>
        /// Creates a Sprite containing the Tileset rendered with the given number of tiles per row.
        /// </summary>
        /// <param name="tilesPerRow">The number of tiles to fit in a single row within the Sprite.</param>
        /// <returns></returns>
        public Sprite Smoosh(int tilesPerRow)
        {
            var width = tilesPerRow;
            var height = (tiles.Length / tilesPerRow) + (tiles.Length % tilesPerRow > 0 ? 1 : 0);
            var tilesToSmoosh = width * height;

            var result = new Sprite(width * TileSize, height * TileSize, tiles[0].Palette);
            result.Lock();

            for (int t = 0; t < tilesToSmoosh; t++)
            {
                var x = t % tilesPerRow;
                var y = t / tilesPerRow;

                // copy tile to result Sprite
                var tile = tiles[t < tiles.Length ? t : 0];
                for (int x2 = 0; x2 < TileSize; x2++)
                {
                    for (int y2 = 0; y2 < TileSize; y2++)
                    {
                        result.SetPixel(x2 + x * TileSize, y2 + y * TileSize, tile.GetPixel(x2, y2));
                    }
                }
            }

            result.Unlock();
            return result;
        }

        /// <summary>
        /// Gets the number of tiles in this <c>Tileset</c>.
        /// </summary>
        public int Size
        {
            get { return tiles.Length; }
        }

        /// <summary>
        /// Gets an array of Sizes where the Tileset can be fit into a Sprite perfectly.
        /// </summary>
        public Size[] PerfectSizes
        {
            get
            {
                var sizes = new List<Size>();
                for (int i = 1; i <= tiles.Length; i++)
                {
                    if (tiles.Length % i == 0)
                    {
                        sizes.Add(new Size(i, tiles.Length / i));
                    }
                }
                return sizes.ToArray();
            }
        }
    }
}
