using System;
using System.Diagnostics;

namespace TilemapCreator
{
    // minimal color structure compatible with consoles
    [DebuggerDisplay("{R}, {G}, {B}")]
    public readonly struct Bgr555 : IEquatable<Bgr555>
    {
        private readonly ushort _value;

        public Bgr555(int r, int g, int b)
        {
            if (r < 0 || r > 0x1F)
                throw new ArgumentOutOfRangeException(nameof(r));
            if (g < 0 || g > 0x1F)
                throw new ArgumentOutOfRangeException(nameof(g));
            if (b < 0 || b > 0x1F)
                throw new ArgumentOutOfRangeException(nameof(b));
            _value = (ushort)((r & 0x1F) | ((g & 0x1F) << 5) | ((b & 0x1F) << 10));
        }

        public Bgr555(bool a, int r, int g, int b)
        {
            if (r < 0 || r > 0x1F)
                throw new ArgumentOutOfRangeException(nameof(r));
            if (g < 0 || g > 0x1F)
                throw new ArgumentOutOfRangeException(nameof(g));
            if (b < 0 || b > 0x1F)
                throw new ArgumentOutOfRangeException(nameof(b));
            _value = (ushort)((r & 0x1F) | ((g & 0x1F) << 5) | ((b & 0x1F) << 10));
            if (a)
                _value |= 0x8000;
        }

        public Bgr555(ushort bgr)
        {
            _value = bgr;
        }

        public ushort ToUInt16() => _value;

        public bool Equals(Bgr555 other) => _value == other._value;

        public override bool Equals(object? obj) => obj is Bgr555 other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(_value);

        public static bool operator ==(Bgr555 a, Bgr555 b) => a.Equals(b);

        public static bool operator !=(Bgr555 a, Bgr555 b) => !(a == b);

        public byte R => (byte)(_value & 0x1F);

        public byte G => (byte)((_value >> 5) & 0x1F);

        public byte B => (byte)((_value >> 10) & 0x1F);

        public bool A => _value >= 0x8000; // extra alpha bit -- occasionally on NDS+

        public static Bgr555 Black => new Bgr555(0x0000);

        public static Bgr555 White => new Bgr555(0x7FFF);
    }
}
