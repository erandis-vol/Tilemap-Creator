using System;
using System.Diagnostics;

namespace TilemapCreator
{
    /// <summary>
    /// Represents a tile reference in a <see cref="Tilemap"/>.
    /// </summary>
    [DebuggerDisplay("Tile = {Tile}, Palette = {Palette}, FlipX = {FlipX}, FlipY = {FlipY}")]
    public struct TilemapTile
    {
        /// <summary>
        /// The tile index.
        /// </summary>
        public int Tile;

        /// <summary>
        /// The palette index.
        /// </summary>
        public byte Palette;

        /// <summary>
        /// Determines whether the tile is flipped horizontally.
        /// </summary>
        public bool FlipX;

        /// <summary>
        /// Determines whether the tile is flipped vertically.
        /// </summary>
        public bool FlipY;
    }
}
