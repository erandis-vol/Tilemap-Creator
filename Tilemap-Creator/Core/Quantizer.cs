using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TMC.Core
{
    // Written by Smart KB for "A Simple - Yet Quite Powerful - Palette Quantizer in C#"
    // http://www.codeproject.com/Articles/66341/A-Simple-Yet-Quite-Powerful-Palette-Quantizer-in-C

    /// <summary>
    /// Provides a mechanism for quantizing palettes.
    /// </summary>
    public interface IQuantizer
    {
        void AddColor(Color color);
        void AddColors(IEnumerable<Color> colors);
        IList<Color> GetPalette(int colorCount);
        int GetPaletteIndex(Color color);
    }

    /// <summary>
    /// Implements an octree palette quantizer.
    /// </summary>
    public class OctreeQuantizer : IQuantizer
    {
        private Node root;
        private int lastColorCount;
        private List<Node>[] levels;

        /// <summary>
        /// Initializes a new instance of the <see cref="OctreeQuantizer"/> class.
        /// </summary>
        public OctreeQuantizer()
        {
            // Initializes the octree level lists
            levels = new List<Node>[7];

            // Creates the octree level lists
            for (int level = 0; level < 7; level++)
            {
                levels[level] = new List<Node>();
            }

            // Creates a root node
            root = new Node(0, this);
        }

        /// <summary>
        /// Adds a node at the specified level.
        /// </summary>
        /// <param name="level">The level to add to.</param>
        /// <param name="node">The node to be added.</param>
        private void AddLevelNode(int level, Node node)
        {
            levels[level].Add(node);
        }

        /// <summary>
        /// Adds the specified color to the quantizer.
        /// </summary>
        /// <param name="color">The color to add.</param>
        public void AddColor(Color color)
        {
            root.AddColor(color, 0, this);
        }

        /// <summary>
        /// Adds the specified colors to the quantizer.
        /// </summary>
        /// <param name="colors">The colors to add.</param>
        public void AddColors(IEnumerable<Color> colors)
        {
            foreach (var color in colors) AddColor(color);
        }

        /// <summary>
        /// Returns a palette with the specified color count.
        /// </summary>
        /// <param name="colorCount">The maximum number of colors in the new palette.</param>
        /// <returns></returns>
        public IList<Color> GetPalette(int colorCount)
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
                    IEnumerable<Node> sortedNodeList = levels[level].OrderBy(node => node.ActiveNodesPixelCount);

                    // removes the nodes unless the count of the leaves is lower or equal than our requested color count
                    foreach (Node node in sortedNodeList)
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
            foreach (Node node in Leaves.OrderByDescending(node => node.ActiveNodesPixelCount))
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

        /// <summary>
        /// Returns the index of the specified color in the palette.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public int GetPaletteIndex(Color color) => root.GetPaletteIndex(color, 0);

        /// <summary>
        /// Gets an enumeration of all active leaves.
        /// </summary>
        private IEnumerable<Node> Leaves
        {
            get { return root.ActiveNodes.Where(node => node.IsLeaf); }
        }

        private class Node
        {
            private static readonly byte[] Mask = new byte[] { 0x80, 0x40, 0x20, 0x10, 0x08, 0x04, 0x02, 0x01 };

            private int red;
            private int green;
            private int blue;

            private int pixelCount;
            private int paletteIndex;

            private readonly Node[] nodes;

            public Node(int level, OctreeQuantizer parent)
            {
                nodes = new Node[8];

                if (level < 7)
                {
                    parent.AddLevelNode(level, this);
                }
            }

            public bool IsLeaf
            {
                get { return pixelCount > 0; }
            }

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

            public int ActiveNodesPixelCount
            {
                get
                {
                    int result = pixelCount;

                    // sums up all the pixel presence for all the active nodes
                    for (int index = 0; index < 8; index++)
                    {
                        Node node = nodes[index];

                        if (node != null)
                        {
                            result += node.pixelCount;
                        }
                    }

                    return result;
                }
            }

            public IEnumerable<Node> ActiveNodes
            {
                get
                {
                    List<Node> result = new List<Node>();

                    // adds all the active sub-nodes to a list
                    for (int index = 0; index < 8; index++)
                    {
                        Node node = nodes[index];

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
                        nodes[index] = new Node(level, parent);
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
                    Node node = nodes[index];

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

}
