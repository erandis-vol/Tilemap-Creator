namespace TMC.Core
{
    /// <summary>
    /// Provides method for encoding and decoding bit depth.
    /// </summary>
    public static class BitDepth
    {
        #region Encode

        private static void EncodeTile4(ref Tile tile, ref byte[] bytes, int index)
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    bytes[index + x + y * 4] = (byte)((tile[x * 2, y] & 0xF) | ((tile[x * 2 + 1, y] & 0xF) << 4));
                }
            }
        }

        public static byte[] Encode4(Tile[] tiles)
        {
            var bytes = new byte[tiles.Length << 5]; // * 32

            for (int i = 0; i < tiles.Length; i++)
            {
                EncodeTile4(ref tiles[i], ref bytes, i << 5);
            }

            return bytes;
        }

        private static void EncodeTile8(ref Tile tile, ref byte[] bytes, int index)
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    bytes[index + x + y * 8] = (byte)tile[x, y];
                }
            }
        }

        public static byte[] Encode8(Tile[] tiles)
        {
            var bytes = new byte[tiles.Length << 6]; // * 64

            for (int i = 0; i < tiles.Length; i++)
            {
                EncodeTile8(ref tiles[i], ref bytes, i << 6);
            }

            return bytes;
        }

        #endregion

        #region Decode

        public static Tile[] DecodeTiles4(byte[] bytes)
        {
            var tiles = new Tile[bytes.Length >> 5]; // / (8 * 8 / 2)

            for (int i = 0; i < tiles.Length; i++)
            {
                DecodeTile4(ref bytes, i << 5, ref tiles[i]); // * (8 * 8 / 2)
            }

            return tiles;
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

        private static void DecodeTile4(ref byte[] bytes, int index, ref Tile tile)
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    tile[x * 2, y]     = (bytes[index + x + y * 4] & 0xF);
                    tile[x * 2 + 1, y] = (bytes[index + x + y * 4] >> 4 & 0xF);
                }
            }
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

        #endregion
    }
}
