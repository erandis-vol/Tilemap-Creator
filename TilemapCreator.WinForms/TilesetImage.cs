using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace TilemapCreator
{
    // caches the tileset image for faster rendering in the editor
    public class TilesetImage : IDisposable
    {
        private const int Columns = 32; // the image is stored as 32 tile rows
        private Tileset _tileset;
        private Palette _palette;
        private Bitmap _image;
        private bool _isDisposed;

        /// <summary>
        /// Releases all resources used by this <see cref="TilesetImage"/>.
        /// </summary>
        public void Dispose()
        {
            if (!_isDisposed)
            {
                _image?.Dispose();
                _image = null;
                _isDisposed = true;
            }
        }

        /// <summary>
        /// Sets the tileset.
        /// </summary>
        /// <param name="tileset"></param>
        public void SetTileset(Tileset tileset)
        {
            _tileset = tileset;
            Invalidate();
        }

        /// <summary>
        /// Sets the tileset and palette.
        /// </summary>
        /// <param name="tileset"></param>
        /// <param name="palette"></param>
        public void SetTileset(Tileset tileset, Palette palette)
        {
            _tileset = tileset;
            _palette = palette;
            Invalidate();
        }

        /// <summary>
        /// Sets the palette.
        /// </summary>
        /// <param name="palette"></param>
        public void SetPalette(Palette palette)
        {
            _palette = palette;
            Invalidate();
        }

        /// <summary>
        /// Invalidates the image and forces it to be redrawn.
        /// </summary>
        public void Invalidate()
        {
            if (_tileset is null || _palette is null)
            {
                _image?.Dispose();
                _image = null;
                return;
            }

            var length = _tileset.Length;
            var rows = Math.Max(1, length / Columns);
            if (rows * Columns < length)
                rows++;

            var image = new Bitmap(Columns * 8, rows * 8);
            using (var fb = image.FastLock())
                _tileset.Draw(fb, Columns, _palette);

            _image?.Dispose();
            _image = image;
        }

        /// <summary>
        /// Returns the cached image.
        /// </summary>
        /// <returns></returns>
        public Image GetImage()
        {
            Debug.Assert(!_isDisposed, "image has been disposed");
            return _image;
        }
    }
}
