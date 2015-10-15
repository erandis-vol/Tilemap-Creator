using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Windows.Forms;

using TMC.Imaging;

namespace TMC
{
    public class Tileset : IDisposable
    {
        public const int TILE_SIZE = 8;

        private Pixelmap[] tiles;

        public Tileset(Pixelmap pixelmap)
        {
            tiles = ExplodeTiles(pixelmap);
        }

        public Tileset(Pixelmap[] tiles)
        {
            this.tiles = tiles;
        }

        public void Dispose()
        {
            // Destroy all the tiles
            for (int i = 0; i < tiles.Length; i++) tiles[i].Dispose();
        }

        public Pixelmap this[int index]
        {
            get { return tiles[index]; }
        }

        // Use SubPixelmap a bunch to explode a large pixelmap ^^
        public static Pixelmap[] ExplodeTiles(Pixelmap pmp)
        {
            // Ignore 8 x 8 size requirements
            // Just cut stuff
            /*if (pmp.Width % 8 > 0 || pmp.Height % 8 > 0)
            {
                MessageBox.Show("Uh-oh");
            }*/

            if (pmp.Width < 8 || pmp.Height < 8)
            {
                throw new Exception("Image must be at least 8 x 8 pixels!");
            }

            List<Pixelmap> tiles = new List<Pixelmap>();
            int tiledWidth = pmp.Width / 8;
            int tiledHeight = pmp.Height / 8;

            for (int y = 0; y < tiledHeight; y++)
            {
                for (int x = 0; x < tiledWidth; x++)
                {
                    tiles.Add(pmp.SubPixelmap(x * 8, y * 8, 8, 8));
                }
            }

            return tiles.ToArray();
        }

        public static void Create(Pixelmap pmp, out Tileset tileset, out Tilemap tilemap)
        {
            // First, create a blank tilemap
            tilemap = new Tilemap(pmp.Width / 8, pmp.Height / 8);
            
            // Create the initial tileset
            // The first tile is always unique, so remember it
            Pixelmap[] allTiles = ExplodeTiles(pmp);
            List<Pixelmap> uniqueTiles = new List<Pixelmap>();
            uniqueTiles.Add(allTiles[0]);

            // Check for unique tiles
            // And set the tilemap data
            // It's a bit convoluted
            // But *much* faster than in the past
            int i = 1; // ~~~ unique tile count
            for (int t = 1; t < allTiles.Length; t++)
            {
                //! Compare it against all known unique tiles
                //! Meaning this gets slower with each new tile... :c
                Pixelmap tile = allTiles[t];
                bool tileIsUnique = true;

                for (int t2 = 0; t2 < uniqueTiles.Count; t2++)
                {
                    // Skip this one
                    if (t == t2) continue;

                    Pixelmap tile2 = uniqueTiles[t2];

                    // Otherwise perform checks
                    if (tile.IsSameAs(tile2))
                    {
                        tilemap[t].Value = t2;

                        tileIsUnique = false;
                        break;
                    }
                        // TODO: some formats don't allow flipping, so make this optional
                    else if (tile.IsSameAsFlipped(tile2, true, false))
                    {
                        tilemap[t].Value = t2;
                        tilemap[t].FlipX = true;

                        tileIsUnique = false;
                        break;
                    }
                    else if (tile.IsSameAsFlipped(tile2, false, true))
                    {
                        tilemap[t].Value = t2;
                        tilemap[t].FlipY = true;
                        
                        tileIsUnique = false;
                        break;
                    }
                    else if (tile.IsSameAsFlipped(tile2, true, true))
                    {
                        tilemap[t].Value = t2;
                        tilemap[t].FlipX = true;
                        tilemap[t].FlipY = true;

                        tileIsUnique = false;
                        break;
                    }
                }

                // Set the tile in the tilemap
                // And remember the unique tiles
                if (tileIsUnique)
                {
                    tilemap[t].Value = i;

                    uniqueTiles.Add(allTiles[t]);
                    i++;
                }
            }

            // Finally, set tileset
            tileset = new Tileset(uniqueTiles.ToArray());
        }

        /// <summary>
        /// Combine all tiles into a single Pixelmap.
        /// </summary>
        /// <returns></returns>
        public Pixelmap SmooshTiles(int tilesPerRow)
        {
            int width = tilesPerRow;
            int height = tiles.Length / width + (tiles.Length % width > 0 ? 1 : 0);

            int tilesToSmoosh = width * height;

            Pixelmap pmap = new Pixelmap(width * TILE_SIZE, height * TILE_SIZE, tiles[0].Palette);
            for (int t = 0; t < tilesToSmoosh; t++)
            {
                int x = t % width;
                int y = t / width;

                var tile = tiles[t < tiles.Length ? t : 0];
                for (int xx = 0; xx < TILE_SIZE; xx++)
                {
                    for (int yy = 0; yy < TILE_SIZE; yy++)
                    {
                        pmap.SetPixel(xx + x * TILE_SIZE, yy + y * TILE_SIZE, tile.GetPixel(xx, yy));
                    }
                }
            }

            return pmap;
        }

        public int[] CalculatePerfectWidths()
        {
            List<int> widths = new List<int>();
            for (int i = 1; i <= tiles.Length; i++)
            {
                if (tiles.Length % i == 0) widths.Add(i);
            }
            return widths.ToArray<int>();
        }

        public Size[] CalculatePerfectSizes()
        {
            int[] widths = CalculatePerfectWidths();

            Size[] result = new Size[widths.Length];
            for (int x = 0; x < result.Length; x++)
            {
                result[x] = new Size(widths[x], tiles.Length / widths[x]);
            }
            return result;
        }

        public Bitmap Render(int tilesPerRow, int zoom)
        {
            // TODO: cache?
            int width = tilesPerRow;
            int height = tiles.Length / width;// +(tiles.Length % width > 0 ? 1 : 0);
            if (tiles.Length % width > 0) height++;

            int tilesToDraw = width * height;

            Bitmap bmp = new Bitmap(width * TILE_SIZE * zoom, height * TILE_SIZE * zoom);
            using (Graphics g = Graphics.FromImage(bmp))
                /*for (int y = 0; y < width; y++)
                {
                    for (int x = 0; x < height; x++)
                    {
                        int t = x + y * width;
                        if (t >= tiles.Length) t = 0;

                        g.DrawImage(tiles[t].Render(zoom), x * TILE_SIZE * zoom, y * TILE_SIZE * zoom);
                    }
                }*/
                    for (int t = 0; t < tilesToDraw; t++)
                    {
                        int x = t % width;
                        int y = t / width;

                        g.DrawImage(tiles[t < tiles.Length ? t : 0].Render(zoom), x * TILE_SIZE * zoom, y * TILE_SIZE * zoom);
                    }

                    return bmp;
        }

        /// <summary>
        /// Gets the number of tiles in the Tileset.
        /// </summary>
        public int Count
        {
            get { return tiles.Length; }
        }
    }
}
