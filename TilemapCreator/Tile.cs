using System;
using System.Runtime.InteropServices;

namespace TilemapCreator
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Tile : IEquatable<Tile>
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct Row : IEquatable<Row>
        {
            private byte p0, p1, p2, p3, p4, p5, p6, p7;

            public byte this[int index]
            {
                get
                {
                    switch (index)
                    {
                        case 0: return p0;
                        case 1: return p1;
                        case 2: return p2;
                        case 3: return p3;
                        case 4: return p4;
                        case 5: return p5;
                        case 6: return p6;
                        case 7: return p7;
                        default:
                            throw new IndexOutOfRangeException();
                    }
                }
                set
                {
                    switch (index)
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
                            throw new IndexOutOfRangeException();
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

            public override bool Equals(object? obj) => obj is Row other && Equals(other);

            public override int GetHashCode() => HashCode.Combine(p0, p1, p2, p3, p4, p5, p6, p7);
        }

        private Row r0, r1, r2, r3, r4, r5, r6, r7;

        public byte this[int index]
        {
            get => this[index % 8, index / 8];
            set => this[index % 8, index / 8] = value;
        }

        public byte this[int x, int y]
        {
            get
            {
                switch (y)
                {
                    case 0: return r0[x];
                    case 1: return r1[x];
                    case 2: return r2[x];
                    case 3: return r3[x];
                    case 4: return r4[x];
                    case 5: return r5[x];
                    case 6: return r6[x];
                    case 7: return r7[x];
                    default:
                        throw new IndexOutOfRangeException();
                }
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
                        throw new IndexOutOfRangeException();
                }
            }
        }

        public bool Equals(Tile other)
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

        public bool Equals(Tile other, TileFlip flip)
        {
            if (flip != TileFlip.None)
            {
                var flipX = flip.HasFlag(TileFlip.X);
                var flipY = flip.HasFlag(TileFlip.Y);

                for (int sy = 0; sy < 8; sy++)
                {
                    for (int sx = 0; sx < 8; sx++)
                    {
                        var dx = flipX ? (7 - sx) : sx;
                        var dy = flipY ? (7 - sy) : sy;

                        if (this[sx, sy] != other[dx, dy])
                            return false;
                    }
                }

                return true;
            }
            else
            {
                return Equals(other);
            }
        }

        public override bool Equals(object? obj) => obj is Tile other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(r0, r1, r2, r3, r4, r5, r6, r7);
    }
}
