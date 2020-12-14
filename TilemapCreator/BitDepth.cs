using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TilemapCreator
{
    // All methods expect tile data.

    /// <summary>
    /// Provides method for encoding and decoding bit depth.
    /// </summary>
    public static class BitDepth
    {
        public static byte[] Encode4(Tileset tileset)
        {
            // assume tiles are well-formed and suited for 4bpp encoding
            var tiles = new byte[tileset.Length * 32];
            var index = 0; // using an indexer is slightly more efficient
            for (var tile = 0; tile < tileset.Length; tile++)
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 4; x++)
                    {
                        // l | (r << 4)
                        var l = tileset.GetPixel(tile, x * 2, y);
                        var r = tileset.GetPixel(tile, x * 2 + 1, y);
                        Debug.Assert(l >= 0 && l <= 0xF, "tileset is not 4bpp");
                        Debug.Assert(r >= 0 && r <= 0xF, "tileset is not 4bpp");
                        tiles[index++] = (byte)((l & 0xF) | ((r & 0xF) << 4));
                    }
                }
            }
            return tiles;
        }

        public static byte[] Encode8(Tileset tileset)
        {
            var tiles = new byte[tileset.Length * 64];
            var index = 0;
            for (int i = 0; i < tileset.Length; i++)
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        var pixel = tileset.GetPixel(i, x, y);
                        Debug.Assert(pixel >= 0 && pixel <= 0xFF, "tileset is not 8bpp");
                        tiles[index++] = (byte)pixel;
                    }
                }
            }
            return tiles;
        }

        /*
        public static byte[] Encode4(Tile[] tiles)
        {
            var bytes = new byte[tiles.Length << 5]; // * 32

            for (int i = 0; i < tiles.Length; i++)
            {
                EncodeTile4(in tiles[i], in bytes, i << 5);
            }

            return bytes;
        }

        private static void EncodeTile4(in Tile tile, in byte[] bytes, int index)
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    bytes[index + x + y * 4] = (byte)((tile[x * 2, y] & 0xF) | ((tile[x * 2 + 1, y] & 0xF) << 4));
                }
            }
        }

        public static byte[] Encode8(Tile[] tiles)
        {
            var bytes = new byte[tiles.Length << 6]; // * 64

            for (int i = 0; i < tiles.Length; i++)
            {
                EncodeTile8(in tiles[i], in bytes, i << 6);
            }

            return bytes;
        }

        private static void EncodeTile8(in Tile tile, in byte[] bytes, int index)
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    bytes[index + x + y * 8] = (byte)tile[x, y];
                }
            }
        }

        public static Tile[] DecodeTiles4(byte[] bytes)
        {
            var tiles = new Tile[bytes.Length >> 5]; // / (8 * 8 / 2)

            for (int i = 0; i < tiles.Length; i++)
            {
                DecodeTile4(bytes, i << 5, ref tiles[i]); // * (8 * 8 / 2)
            }

            return tiles;
        }

        private static void DecodeTile4(byte[] bytes, int index, ref Tile tile)
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    tile[x * 2, y] = (bytes[index + x + y * 4] & 0xF);
                    tile[x * 2 + 1, y] = (bytes[index + x + y * 4] >> 4 & 0xF);
                }
            }
        }

        public static Tile[] Decode8(byte[] bytes)
        {
            var tiles = new Tile[bytes.Length >> 6]; // / (8 * 8)

            for (int i = 0; i < tiles.Length; i++)
            {
                DecodeTile8(ref bytes, i << 6, ref tiles[i]); // * (8 * 8)
            }

            return tiles;
        }

        private static void DecodeTile8(ref byte[] bytes, int index, ref Tile tile)
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    tile[x, y] = bytes[index + x + y * 8];
                }
            }
        }
        */
    }
}
