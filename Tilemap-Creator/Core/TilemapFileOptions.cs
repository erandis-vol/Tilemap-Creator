using System.Diagnostics;

namespace TMC.Core
{
    [DebuggerDisplay("FileName = {FileName}, Format = {Format}, Padding = {Padding}")]
    public class TilemapFileOptions
    {
        /// <summary>
        /// Gets or sets the full name and path of the file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the format.
        /// </summary>
        public TilemapFormat Format { get; set; }

        /// <summary>
        /// Gets or sets the amount of padding.
        /// </summary>
        public int Padding { get; set; }
    }
}
