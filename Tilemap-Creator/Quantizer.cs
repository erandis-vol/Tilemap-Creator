using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TMC
{
    // originally from:
    // http://www.codeproject.com/Articles/66341/A-Simple-Yet-Quite-Powerful-Palette-Quantizer-in-C
    class OctreeQuantizer
    {
        private OctreeNode root;
        private int lastColorCount;
        private List<OctreeNode>[] levels;

        public OctreeQuantizer()
        {
            // initializes the octree level lists
            levels = new List<OctreeNode>[7];

            // creates the octree level lists
            for (int level = 0; level < 7; level++)
            {
                levels[level] = new List<OctreeNode>();
            }

            // creates a root node
            root = new OctreeNode(0, this);
        }

        internal IEnumerable<OctreeNode> Leaves
        {
            get { return root.ActiveNodes.Where(node => node.IsLeaf); }
        }

        internal void AddLevelNode(int level, OctreeNode octreeNode)
        {
            levels[level].Add(octreeNode);
        }

        public void AddColor(Color color)
        {
            root.AddColor(color, 0, this);
        }

        public List<Color> GetPalette(int colorCount)
        {
            var result = new List<Color>();
            int leafCount = Leaves.Count();
            lastColorCount = leafCount;
            int paletteIndex = 0;

            // goes thru all the levels starting at the deepest, and goes upto a root level
            for (int level = 6; level >= 0; level--)
            {
                // if level contains any node
                if (levels[level].Count > 0)
                {
                    // orders the level node list by pixel presence (those with least pixels are at the top)
                    IEnumerable<OctreeNode> sortedNodeList = levels[level].OrderBy(node => node.ActiveNodesPixelCount);

                    // removes the nodes unless the count of the leaves is lower or equal than our requested color count
                    foreach (OctreeNode node in sortedNodeList)
                    {
                        // removes a node
                        leafCount -= node.RemoveLeaves(level, leafCount, colorCount, this);

                        // if the count of leaves is lower then our requested count terminate the loop
                        if (leafCount <= colorCount) break;
                    }

                    // if the count of leaves is lower then our requested count terminate the level loop as well
                    if (leafCount <= colorCount) break;

                    // otherwise clear whole level, as it is not needed anymore
                    levels[level].Clear();
                }
            }

            // goes through all the leaves that are left in the tree (there should now be less or equal than requested)
            foreach (OctreeNode node in Leaves.OrderByDescending(node => node.ActiveNodesPixelCount))
            {
                if (paletteIndex >= colorCount) break;

                // adds the leaf color to a palette
                if (node.IsLeaf)
                {
                    result.Add(node.Color);
                }

                // and marks the node with a palette index
                node.SetPaletteIndex(paletteIndex++);
            }

            // we're unable to reduce the Octree with enough precision, and the leaf count is zero
            if (result.Count == 0)
            {
                throw new NotSupportedException("The Octree contains after the reduction 0 colors, it may happen for 1-16 colors because it reduces by 1-8 nodes at time. Should be used on 8 or above to ensure the correct functioning.");
            }

            // returns the palette
            return result;
        }

        public int GetPaletteIndex(Color color)
        {
            // retrieves a palette index
            return root.GetPaletteIndex(color, 0);
        }
    }

    class OctreeNode
    {
        private static readonly byte[] Mask = new byte[] { 0x80, 0x40, 0x20, 0x10, 0x08, 0x04, 0x02, 0x01 };

        private int red;
        private int green;
        private int blue;

        private int pixelCount;
        private int paletteIndex;

        private readonly OctreeNode[] nodes;
        /// <summary>
        /// Initializes a new instance of the <see cref="OctreeNode"/> class.
        /// </summary>
        public OctreeNode(int level, OctreeQuantizer parent)
        {
            nodes = new OctreeNode[8];

            if (level < 7)
            {
                parent.AddLevelNode(level, this);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this node is a leaf.
        /// </summary>
        /// <value><c>true</c> if this node is a leaf; otherwise, <c>false</c>.</value>
        public bool IsLeaf
        {
            get { return pixelCount > 0; }
        }

        /// <summary>
        /// Gets the averaged leaf color.
        /// </summary>
        /// <value>The leaf color.</value>
        public Color Color
        {
            get
            {
                // determines a color of the leaf
                if (IsLeaf)
                {
                    return pixelCount == 1 ?
                        Color.FromArgb(255, red, green, blue) :
                        Color.FromArgb(255, red / pixelCount, green / pixelCount, blue / pixelCount);
                }
                else
                {
                    throw new InvalidOperationException("Cannot retrieve a color for other node than leaf.");
                }
            }
        }

        /// <summary>
        /// Gets the active nodes pixel count.
        /// </summary>
        /// <value>The active nodes pixel count.</value>
        public int ActiveNodesPixelCount
        {
            get
            {
                int result = pixelCount;

                // sums up all the pixel presence for all the active nodes
                for (int index = 0; index < 8; index++)
                {
                    OctreeNode node = nodes[index];

                    if (node != null)
                    {
                        result += node.pixelCount;
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Enumerates only the leaf nodes.
        /// </summary>
        /// <value>The enumerated leaf nodes.</value>
        public IEnumerable<OctreeNode> ActiveNodes
        {
            get
            {
                List<OctreeNode> result = new List<OctreeNode>();

                // adds all the active sub-nodes to a list
                for (int index = 0; index < 8; index++)
                {
                    OctreeNode node = nodes[index];

                    if (node != null)
                    {
                        if (node.IsLeaf)
                        {
                            result.Add(node);
                        }
                        else
                        {
                            result.AddRange(node.ActiveNodes);
                        }
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Adds the color.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="level">The level.</param>
        /// <param name="parent">The parent.</param>
        public void AddColor(Color color, int level, OctreeQuantizer parent)
        {
            // if this node is a leaf, then increase a color amount, and pixel presence
            if (level == 8)
            {
                red += color.R;
                green += color.G;
                blue += color.B;
                pixelCount++;
            }
            else if (level < 8) // otherwise goes one level deeper
            {
                // calculates an index for the next sub-branch
                int index = GetColorIndexAtLevel(color, level);

                // if that branch doesn't exist, grows it
                if (nodes[index] == null)
                {
                    nodes[index] = new OctreeNode(level, parent);
                }

                // adds a color to that branch
                nodes[index].AddColor(color, level + 1, parent);
            }
        }

        /// <summary>
        /// Gets the index of the palette.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="level">The level.</param>
        /// <returns></returns>
        public int GetPaletteIndex(Color color, int level)
        {
            // if a node is leaf, then we've found the best match already
            if (IsLeaf)
            {
                return paletteIndex;
            }
            else // otherwise continue in to the lower depths
            {
                int index = GetColorIndexAtLevel(color, level);

                return nodes[index] != null ? nodes[index].GetPaletteIndex(color, level + 1) : nodes.
                    Where(node => node != null).
                    First().
                    GetPaletteIndex(color, level + 1);
            }
        }

        /// <summary>
        /// Removes the leaves by summing all it's color components and pixel presence.
        /// </summary>
        /// <returns></returns>
        public int RemoveLeaves(int level, int activeColorCount, int targetColorCount, OctreeQuantizer parent)
        {
            int result = 0;

            // scans thru all the active nodes
            for (int index = 0; index < 8; index++)
            {
                OctreeNode node = nodes[index];

                if (node != null)
                {
                    // sums up their color components
                    red += node.red;
                    green += node.green;
                    blue += node.blue;

                    // and pixel presence
                    pixelCount += node.pixelCount;

                    // increases the count of reduced nodes
                    result++;
                }
            }

            // returns a number of reduced sub-nodes, minus one because this node becomes a leaf
            return result - 1;
        }

        /// <summary>
        /// Calculates the color component bit (level) index.
        /// </summary>
        private static int GetColorIndexAtLevel(Color color, int level)
        {
            return ((color.R & Mask[level]) == Mask[level] ? 4 : 0) |
                   ((color.G & Mask[level]) == Mask[level] ? 2 : 0) |
                   ((color.B & Mask[level]) == Mask[level] ? 1 : 0);
        }

        /// <summary>
        /// Sets a palette index to this node.
        /// </summary>
        internal void SetPaletteIndex(int index)
        {
            paletteIndex = index;
        }
    }
}
