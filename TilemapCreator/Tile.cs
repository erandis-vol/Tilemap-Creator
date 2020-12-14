using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace TilemapCreator
{
    /// <summary>
    /// Represents an 8x8 array of pixel data.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [Obsolete]
    public struct Tile
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct Row
        {
            private int p0;
            private int p1;
            private int p2;
            private int p3;
            private int p4;
            private int p5;
            private int p6;
            private int p7;

            public Row(ReadOnlySpan<int> pixels)
            {
                Debug.Assert(pixels.Length == 8, "Passed invalid span to row.");
                p0 = pixels[0];
                p1 = pixels[1];
                p2 = pixels[2];
                p3 = pixels[3];
                p4 = pixels[4];
                p5 = pixels[5];
                p6 = pixels[6];
                p7 = pixels[7];
            }

            public int this[int x]
            {
                get
                {
                    return x switch
                    {
                        0 => p0,
                        1 => p1,
                        2 => p2,
                        3 => p3,
                        4 => p4,
                        5 => p5,
                        6 => p6,
                        7 => p7,
                        _ => throw new ArgumentOutOfRangeException(nameof(x))
                    };
                }
                set
                {
                    switch (x)
                    {
                        case 0: p0 = value; break;
                        case 1: p1 = value; break;
                        case 2: p2 = value; break;
                        case 3: p3 = value; break;
                        case 4: p4 = value; break;
                        case 5: p5 = value; break;
                        case 6: p6 = value; break;
                        case 7: p7 = value; break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(x));
                    }
                }
            }

            public bool Equals(Row other)
            {
                return p0 == other.p0
                    && p1 == other.p1
                    && p2 == other.p2
                    && p3 == other.p3
                    && p4 == other.p4
                    && p5 == other.p5
                    && p6 == other.p6
                    && p7 == other.p7;
            }

            //public static bool operator ==(Row r1, Row r2)
            //{
            //    return r1.Equals(r2);
            //}
            //
            //public static bool operator !=(Row r1, Row r2)
            //{
            //    return !(r1 == r2);
            //}
        }

        private Row r0;
        private Row r1;
        private Row r2;
        private Row r3;
        private Row r4;
        private Row r5;
        private Row r6;
        private Row r7;

        public Tile(ReadOnlySpan<int> pixels)
        {
            if (pixels.Length != 64)
                throw new ArgumentException("Pixels does not contain 64 entries.", nameof(pixels));
            r0 = new Row(pixels.Slice( 0, 8));
            r1 = new Row(pixels.Slice( 8, 8));
            r2 = new Row(pixels.Slice(16, 8));
            r3 = new Row(pixels.Slice(24, 8));
            r4 = new Row(pixels.Slice(32, 8));
            r5 = new Row(pixels.Slice(40, 8));
            r6 = new Row(pixels.Slice(48, 8));
            r7 = new Row(pixels.Slice(56, 8));
        }

        public int this[int index]
        {
            get
            {
                if (index < 0 || index >= 64)
                    throw new ArgumentOutOfRangeException(nameof(index));
                return this[index % 8, index / 8];
            }
            set
            {
                if (index < 0 || index >= 64)
                    throw new ArgumentOutOfRangeException(nameof(index));
                this[index % 8, index / 8] = value;
            }
        }

        public int this[int x, int y]
        {
            get
            {
                return y switch
                {
                    0 => r0[x],
                    1 => r1[x],
                    2 => r2[x],
                    3 => r3[x],
                    4 => r4[x],
                    5 => r5[x],
                    6 => r6[x],
                    7 => r7[x],
                    _ => throw new ArgumentOutOfRangeException(nameof(y))
                };
            }
            set
            {
                switch (y)
                {
                    case 0: r0[x] = value; break;
                    case 1: r1[x] = value; break;
                    case 2: r2[x] = value; break;
                    case 3: r3[x] = value; break;
                    case 4: r4[x] = value; break;
                    case 5: r5[x] = value; break;
                    case 6: r6[x] = value; break;
                    case 7: r7[x] = value; break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(y));
                }
            }
        }

        public unsafe bool Matches(ref Tile other, bool flipX, bool flipY)
        {
            if (flipX || flipY)
            {
                for (int srcY = 0; srcY < 8; srcY++)
                {
                    for (int srcX = 0; srcX < 8; srcX++)
                    {
                        var dstX = flipX ? (7 - srcX) : srcX;
                        var dstY = flipY ? (7 - srcY) : srcY;

                        if (this[srcX, srcY] != other[dstX, dstY])
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
            else
            {
                return r0.Equals(other.r0)
                    && r1.Equals(other.r1)
                    && r2.Equals(other.r2)
                    && r3.Equals(other.r3)
                    && r4.Equals(other.r4)
                    && r5.Equals(other.r5)
                    && r6.Equals(other.r6)
                    && r7.Equals(other.r7);
            }
        }
    }
}
