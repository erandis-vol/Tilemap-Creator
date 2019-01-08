using System.Diagnostics;

namespace TMC.Core
{
    [DebuggerDisplay("FileName = {FileName}, Format = {Format}")]
    public class TilesetFileOptions
    {
        /// <summary>
        /// Gets or sets the full name and path of the file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the format.
        /// </summary>
        public TilesetFormat Format { get; set; }

        /// <summary>
        /// Gets or sets the number of columns.
        /// </summary>
        public int Columns { get; set; }
    }
}
