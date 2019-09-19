using System;

namespace TilemapCreator
{
    [Flags]
    public enum TileFlip
    {
        None = 0x000,
        X    = 0x400,
        Y    = 0x800,
        XY   = X | Y
    }
}
