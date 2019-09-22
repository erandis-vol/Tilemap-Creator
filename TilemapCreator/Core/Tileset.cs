using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TilemapCreator.Core
{
    /// <summary>
    /// Represents an ordered collection of 8x8 tiles.
    /// </summary>
    public class Tileset
    {
        #region Field

        private Tile[] tiles;

        public Tileset(IEnumerable<Tile> collection)
        {
            tiles = collection.ToArray();
        }

        #endregion

        #region Methods

        public ref Tile this[int index] => ref tiles[index];

        #endregion

        #region Properties

        public int Length => tiles.Length;

        #endregion
    }
}
