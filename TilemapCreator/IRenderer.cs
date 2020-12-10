using System;
using System.Collections.Generic;
using System.Text;

namespace TilemapCreator
{
    /// <summary>
    /// Provides a mechanism for rendering pixels.
    /// </summary>
    public interface IRenderer
    {
        public void SetPixel(int x, int y, Bgr555 color);
    }
}
