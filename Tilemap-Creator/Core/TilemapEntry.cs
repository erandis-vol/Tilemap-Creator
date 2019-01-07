using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace TMC.Core
{
    /// <summary>
    /// Represents an entry in a <see cref="Tilemap"/>.
    /// </summary>
    [DebuggerDisplay("Tileset = {Tileset}, Palette = {Palette}, FlipX = {FlipX}, FlipY = {FlipY}")]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TilemapEntry
    {
        /// <summary>The tile index.</summary>
        public short Index;

        /// <summary>The palette index.</summary>
        public byte Palette;

        /// <summary>Determines wether the tile is flipped horizontally.</summary>
        public bool FlipX;

        /// <summary>Determines whether the tile is flipped vertically.</summary>
        public bool FlipY;

        /// <summary>
        /// Initializes a new isntance of the <see cref="TilemapEntry"/> struct with the specified index.
        /// </summary>
        /// <param name="index">The tile index.</param>
        public TilemapEntry(short index)
            : this(index, 0, false, false)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TilemapEntry"/> struct with the specified index and flipping.
        /// </summary>
        /// <param name="index">The tile index.</param>
        /// <param name="flipX">Determines wether the tile is flipped horizontally.</param>
        /// <param name="flipY">Determines whether the tile is flipped vertically.</param>
        public TilemapEntry(short index, bool flipX, bool flipY)
            : this(index, 0, flipX, flipY)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TilemapEntry"/> struct with the specified index, palette, and flipping.
        /// </summary>
        /// <param name="index">The tile index.</param>
        /// <param name="palette">The palette index.</param>
        /// <param name="flipX">Determines wether the tile is flipped horizontally.</param>
        /// <param name="flipY">Determines whether the tile is flipped vertically.</param>
        public TilemapEntry(short index, byte palette, bool flipX, bool flipY)
        {
            Index = index;
            Palette = palette;
            FlipX = flipX;
            FlipY = flipY;
        }
    }
}
