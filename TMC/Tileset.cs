// Copyright (c) 2014 itari
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at:
// 
//  http://www.apache.org/licenses/LICENSE-2.0
//  
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

using TMC.Imaging;

namespace TMC
{
    public class Tileset
    {
        private Pixelmap baseImg;
        private Pixelmap[] tiles;

        // cache
        private Bitmap cachedImg;
        private int cachedImgWidth, cachedZoom;

        public Tileset(Pixelmap pixelmap)
        {
            this.baseImg = pixelmap;
            this.tiles = pixelmap.Tile();

            this.cachedImg = null;
            this.cachedImgWidth = -1;
            this.cachedZoom = 0;
        }

        ~Tileset()
        {
            if (cachedImg != null)
                cachedImg.Dispose();
            // nothing? Pixelmap's destructor handles stuff like that
        }

        public Pixelmap this[int index]
        {
            get { return tiles[(index >= tiles.Length ? 0 : index)]; }
            set { tiles[index] = value; }
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

        public Bitmap Draw(int width, int zoom)
        {
            // Check if what we want is already good.
            if (width == cachedImgWidth && cachedImg != null && zoom == cachedZoom)
            {
                return cachedImg;
            }
            else
            {
                int height = tiles.Length / width;
                if (tiles.Length % width > 0) height++;

                Bitmap bmp = new Bitmap(width * 8 * zoom, height * 8 * zoom);
                Graphics g = Graphics.FromImage(bmp);
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        g.DrawImage(this[x + y * width].Draw(zoom), x * 8 * zoom, y * 8 * zoom, 8 * zoom, 8 * zoom);
                    }
                }
                g.Dispose();

                cachedImg = bmp;
                cachedImgWidth = width;
                cachedZoom = zoom;
                return bmp;
            }
        }

        /// <summary>
        /// Combine all tiles into a single Pixelmap.
        /// </summary>
        /// <param name="tilesPerRow">Number of tiles per row.</param>
        /// <returns></returns>
        public Pixelmap Smoosh(int tilesPerRow)
        {
            int width = tilesPerRow;
            int height = tiles.Length / width;
            if (tiles.Length % width > 0) height++; // over flow

            // Draw
            Pixelmap pm = new Pixelmap(width * 8, height * 8, tiles[0].Palette);
            for (int t = 0; t < tiles.Length; t++)
            {
                int x = t % width;
                int y = t / width;

                Pixelmap tile = tiles[t];
                for (int xx = 0; xx < 8; xx++)
                {
                    for (int yy = 0; yy < 8; yy++)
                    {
                        pm.SetPixel(xx + x * 8, yy + y * 8, tile.GetPixel(xx, yy));
                    }
                }
            }
            return pm;
        }

        public int Length
        {
            get { return tiles.Length; }
        }

        public string GetBitDepthDescription()
        {
            if (tiles[0].Palette.Length <= 16) return "4 BPP (16 Colors)";
            else return "8 BPP (256 Colors)";
        }
    }
}
