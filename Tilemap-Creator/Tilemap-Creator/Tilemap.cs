using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TMC
{
    //[StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Tile
    {
        public int TilesetIndex;
        public int PaletteIndex;
        public bool FlipX;
        public bool FlipY;
    }

    // a basic resizable Tilemap
    public class Tilemap
    {
        Tile[] tiles;
        int width, height;

        public Tilemap(int width, int height)
        {
            this.width = width;
            this.height = height;

            tiles = new Tile[width * height];
            for (int i = 0; i < width * height; i++)
                tiles[i] = new Tile();
        }

        public Tile this[int index]
        {
            get { return tiles[index]; }
            set { tiles[index] = value; }
        }

        public Tile this[int x, int y]
        {
            get { return tiles[x + y * width]; }
            set { tiles[x + y * width] = value; }
        }

        public void Resize(int newWidth, int newHeight)
        {
            // declare new tile array
            Tile[] newTiles = new Tile[newWidth * newHeight];

            // the data needed to be copied
            int copyWidth = Math.Min(width, newWidth);
            int copyHeight = Math.Min(height, newHeight);
            
            // copy tiles over
            for (int y = 0; y < copyHeight; y++)
            {
                for (int x = 0; x < copyWidth; x++)
                {
                    // new (x, y) = old (x, y)
                    newTiles[x + y * newWidth] = tiles[x + y * width];
                }
            }

            // all done
            tiles = newTiles;
            width = newWidth;
            height = newHeight;
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }
    }
}
