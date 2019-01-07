namespace TMC.Core
{
    /// <summary>
    /// Represents the format of a <see cref="Tilemap"/>.
    /// </summary>
    public enum TilemapFormat
    {
        /// <summary>Text mode, 4 bits per pixel</summary>
        Text4 = 0x40,

        /// <summary>Text mode, 8 bits per pixel</summary>
        Text8 = 0x80,

        /// <summary>Rotation/scaling mode</summary>
        RotationScaling,
    }
}
