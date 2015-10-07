using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace TMC.Imaging
{
    // Used for storing a cached thingy
    public struct PixelmapCache : IDisposable
    {
        public int Zoom;
        public Bitmap Image;

        public void Dispose()
        {
            if (Image != null)
                Image.Dispose();
        }
    }

    // Saving formats
    public enum PixelmapFormat
    {
        Bitmap,
        BitmapIndexed,
        NCGR //? Nitro Character GRaphic
    }

    public class Pixelmap : IDisposable
    {
        private int _width, _height;
        private byte[] pixels;
        private Palette palette;

        private bool disposed = false;

        // cached image
        // empty by default :3
        private PixelmapCache cache = new PixelmapCache()
        {
            Zoom = -1,
            Image = null
        };

        /// <summary>
        /// Create a Pixelmap from an indexed System.Drawing.Bitmap.
        /// </summary>
        /// <param name="bmp"></param>
        public Pixelmap(Bitmap bmp)
        {
            pixels = new byte[bmp.Width * bmp.Height];
            _width = bmp.Width;
            _height = bmp.Height;

            if (bmp.PixelFormat == PixelFormat.Format4bppIndexed ||
                bmp.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                palette = new Palette(bmp);
            }
            else
            {
                throw new Exception("Image is not indexed!");
            }

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    pixels[y * bmp.Width + x] = (byte)palette[bmp.GetPixel(x, y)];
                }
            }
        }

        /// <summary>
        /// Create a Pixelmap from a System.Drawing.Bitmap and generate a Palette.
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="paletteSize"></param>
        public Pixelmap(Bitmap bmp, int paletteSize)
        {
            pixels = new byte[bmp.Width * bmp.Height];
            _width = bmp.Width;
            _height = bmp.Height;

            palette = bmp.CreatePalette2(paletteSize);//.ToGreyscale();

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    pixels[y * bmp.Width + x] = (byte)palette[bmp.GetPixel(x, y)];
                }
            }
        }

        /// <summary>
        /// Create a blank Pixelmap with a blank Palette.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="paletteSize"></param>
        public Pixelmap(int width, int height, int paletteSize = 16)
        {
            _width = width;
            _height = height;

            pixels = new byte[width * height];
            palette = new Palette(paletteSize);

            // Fill image data with empty stuff
            //for (int i = 0; i < width * height; i++) pixels[i] = 0;
        }

        /// <summary>
        /// Create a blank Pixelmap with the specified Palette.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="palette"></param>
        public Pixelmap(int width, int height, Palette palette)
        {
            _width = width;
            _height = height;

            pixels = new byte[width * height];
            this.palette = palette;
        }

        /// <summary>
        /// Creates a Pixelmap from a file with the specified PixelmapFormat.
        /// ** Only supports NCGR files right now. **
        /// </summary>
        /// <param name="file">The file to load from.</param>
        /// <param name="format">The format of the file.</param>
        public Pixelmap(string file, PixelmapFormat format)
        {
            if (format == PixelmapFormat.NCGR)
            {
                using (BinaryReader br = new BinaryReader(File.OpenRead(file)))
                {

                    // header
                    if (br.ReadUInt32() != 0x4E434752) throw new Exception("This is an NCGR!");
                    br.BaseStream.Position += 10L;
                    ushort sections = br.ReadUInt16();

                    // char section
                    // header
                    if (br.ReadUInt32() != 0x43484152) throw new Exception("Bad NCGR format!");
                    br.ReadUInt32(); // section size
                    ushort tiledHeight = br.ReadUInt16();
                    ushort tiledWidth = br.ReadUInt16();

                    if (tiledHeight == 0xFFFF || tiledWidth == 0xFFFF) throw new Exception("Unsupported NCGR format!");

                    //ColorMode mode = ColorMode.Color16;
                    //if (br.ReadUInt32() == 0x3) mode = ColorMode.Color16;
                    //else mode = ColorMode.Color256;
                    int bpp = br.ReadUInt32() == 3 ? 4 : 8;

                    // initialize the data
                    _width = tiledWidth * 8;
                    _height = tiledHeight * 8;
                    pixels = new byte[_width * _height];
                    palette = new Palette(bpp * bpp, true);

                    br.ReadUInt16(); // unknown
                    br.ReadUInt16(); // unknown
                    if (br.ReadUInt32() != 0) throw new Exception("Unsupported NCGR format!"); // flags
                    uint size = br.ReadUInt32();
                    br.ReadUInt32(); // 0x18

                    // read tile data
                    for (int y = 0; y < tiledHeight; y++)
                    {
                        for (int x = 0; x < tiledWidth; x++)
                        {
                            for (int yy = 0; yy < 8; yy++)
                            {
                                if (bpp == 4)
                                {
                                    for (int xx = 0; xx < 8; xx += 2)
                                    {
                                        byte b = br.ReadByte();
                                        int l = b & 15;
                                        int r = (b >> 4) & 15;
                                        SetPixel(xx + x * 8, yy + y * 8, (byte)l);
                                        SetPixel(xx + 1 + x * 8, yy + y * 8, (byte)r);
                                    }
                                }
                                else
                                {
                                    for (int xx = 0; xx < 8; xx++)
                                    {
                                        SetPixel(xx + x * 8, yy + y * 8, br.ReadByte());
                                    }
                                }
                            }
                        }
                    }

                    // not sure on this one
                    if (sections > 1)
                    {
                        // pass
                    }
                }
            }
            else
            {
                throw new Exception("Unsupported initialization format (for now)!\nUse other initializers instead.");
            }
        }

        // A secret initializer
        // For SubPixelmap and other low-level-y stuff only
        private Pixelmap(int width, int height, byte[] pixels, Palette palette)
        {
            _width = width; _height = height;
            this.pixels = pixels;
            this.palette = palette;
        }

        ~Pixelmap()
        {
            if (!disposed) Dispose();
        }

        public void Dispose()
        {
            cache.Dispose();
            disposed = true;
        }

        #region Saving

        public void Save(string file, PixelmapFormat format)
        {
            if (format == PixelmapFormat.Bitmap)
            {
                SaveBitmap(file);
            }
            else if (format == PixelmapFormat.BitmapIndexed)
            {
                SaveBitmapIndexed(file);
            }
            else if (format == PixelmapFormat.NCGR) // Nintendo DS format
            {
                SaveNCGR(file);
            }
        }

        private void SaveBitmap(string file)
        {
            using (Bitmap bmp = new Bitmap(_width, _height))
            {
                //? use FastPixel here?

                // a real quick unzoomed render
                for (int y = 0; y < _height; y++)
                    for (int x = 0; x < _width; x++)
                        bmp.SetPixel(x, y, palette[GetPixel(x, y)]);

                // and save
                bmp.Save(file, ImageFormat.Bmp);
            }
        }

        private void SaveBitmapIndexed(string file)
        {
            if (palette.Length == 16)
            {
                int pixelArraySize = (_width / 2) * _height;
                byte[] data = new byte[118 + pixelArraySize];

                // Write header
                data[0xA] = 0x76;

                data[0x1C] = 4;
                byte[] ilength = BitConverter.GetBytes(pixelArraySize);

                data[0] = (byte)'B';
                data[1] = (byte)'M';

                byte[] flength = BitConverter.GetBytes(data.Length);
                flength.CopyTo(data, 2);

                data[6] = (byte)'T';
                data[7] = (byte)'M';
                data[8] = (byte)'C';
                data[9] = (byte)'4';

                data[0xE] = 40;

                byte[] w = BitConverter.GetBytes(_width);
                w.CopyTo(data, 0x12);

                byte[] h = BitConverter.GetBytes(_height);
                h.CopyTo(data, 0x16);

                data[0x1A] = 1;

                ilength.CopyTo(data, 0x22);

                // Write palette table
                for (int i = 0; i < palette.Length; i++)
                {
                    data[0x36 + i * 4] = palette[i].B;
                    data[0x37 + i * 4] = palette[i].G;
                    data[0x38 + i * 4] = palette[i].R;
                }

                // Write pixels
                for (int y = 0; y < _height; y++)
                {
                    for (int x = 0; x < _width; x += 2)
                    {
                        //int l = Helper.GetClosestColorFromPalette(b.GetPixel(x, y), palette);
                        //int r = Helper.GetClosestColorFromPalette(b.GetPixel(x + 1, y), palette);
                        int l = GetPixel(x, y);
                        int r = GetPixel(x + 1, y);

                        data[118 + (x / 2) + ((_height - y - 1) * (_width / 2))] = (byte)((byte)(l << 4) + (byte)r);
                    }
                }

                // Save to file
                BinaryWriter bw = new BinaryWriter(File.Create(file));
                bw.Write(data);

                bw.Close();
                bw.Dispose();
            }
            else if (palette.Length == 256)
            {
                throw new NotImplementedException("Cannot save 256 color Bitmaps yet!");
            }
            else
            {
                // Indexing is not cooperating, save generically instead
                SaveBitmap(file);
            }
        }

        private void SaveNCGR(string file)
        {
            // Fail-safe, really
            if (palette.Length != 16 && palette.Length != 256)
            {
                SaveBitmap(file);
                return;
            }

            int bpp = palette.Length == 16 ? 4 : 8;
            using (BinaryWriter bw = new BinaryWriter(File.Create(file)))
            {
                // Generic header
                bw.Write((uint)0x4E434752); // NCGR
                bw.Write((uint)0x0001FEFF); // format info.
                bw.Write(0x0); // file size -- write at end
                bw.Write((ushort)0x10); // header size
                bw.Write((ushort)0x2); // section count

                // CHAR section
                // Responsible for holding the image data
                bw.Write((uint)0x43484152);

                ushort tiledHeight = (ushort)(_height / 8);
                ushort tiledWidth = (ushort)(_width / 8);
                // section size
                if (bpp == 4) bw.Write((uint)(tiledHeight * tiledWidth * 32) + 0x20u);
                else bw.Write((uint)(tiledHeight * tiledWidth * 64) + 0x20u);
                // dimension
                bw.Write(tiledHeight);
                bw.Write(tiledWidth);

                // Bit Depth (3 = 4 bpp, 4 = 8 bpp)
                if (bpp == 4) bw.Write((uint)3);
                else bw.Write((uint)4);

                bw.Write((ushort)0); // unknown
                bw.Write((ushort)0); // unknown
                bw.Write((uint)0); // flags
                // section size
                if (bpp == 4) bw.Write((uint)(tiledHeight * tiledWidth * 32));
                else bw.Write((uint)(tiledWidth * tiledHeight * 64));
                bw.Write((uint)0x18); // section offset (relative) -- always 0x18

                // Write tile data (this is a monstrosity)
                // I write this in "horizontal tile mode"
                // If I was writing in "no tile mode" I would just write
                // The pixels straight out
                for (int y = 0; y < tiledHeight; y++)
                {
                    for (int x = 0; x < tiledWidth; x++)
                    {
                        // write a tile
                        for (int yy = 0; yy < 8; yy++)
                        {
                            if (bpp == 4)
                            {
                                for (int xx = 0; xx < 8; xx += 2)
                                {
                                    int l = GetPixel(xx + x * 8, yy + y * 8);
                                    int r = GetPixel(xx + 1 + x * 8, yy + y * 8);
                                    bw.Write((byte)(((r & 15) << 4) | (l & 15)));
                                }
                            }
                            else
                            {
                                for (int xx = 0; xx < 8; xx++)
                                {
                                    int p = GetPixel(xx + x * 8, yy + y * 8);
                                    bw.Write((byte)p);
                                }
                            }
                        }
                    }
                }

                // SOPC section
                // I don't really know what this does...
                bw.Write((uint)0x43504F53);
                bw.Write((uint)0x10); // ???
                bw.Write((uint)0x0); // ???
                bw.Write((ushort)tiledWidth); // width in tiles
                bw.Write((ushort)tiledHeight); // height in tiels

                // Write file size
                bw.BaseStream.Seek(8L, SeekOrigin.Begin);
                bw.Write((uint)bw.BaseStream.Length);
            }
        }

        #endregion

        public byte GetPixel(int x, int y)
        {
            int index = x + y * _width;
            if (index < pixels.Length) return pixels[index];
            else return 0;
        }

        public void SetPixel(int x, int y, byte value)
        {
            int index = x + y * _width;
            if (index < pixels.Length) pixels[index] = value;
        }

        /*
        public static bool operator ==(Pixelmap p1, Pixelmap p2)
        {
            // First, check size equality
            if (p1._width != p2._width || p1._height != p2._height) return false;

            // Now, check pixel by pixel equivalence
            //! Palette does not matter here
            for (int b = 0; b < p1.pixels.Length; b++)
            {
                if (p1.pixels[b] != p2.pixels[b]) return false;
            }

            // Looks good! ^_^
            return true;
        }

        public static bool operator !=(Pixelmap p1, Pixelmap p2)
        {
            return !(p1 == p2);
        }
        */

        public bool IsSameAs(Pixelmap p2)
        {
            // First, check size equality
            if (_width != p2._width || _height != p2._height) return false;

            // Now, check pixel by pixel equivalence
            //! Palette does not matter here
            for (int b = 0; b < pixels.Length; b++)
            {
                if (pixels[b] != p2.pixels[b]) return false;
            }

            // Looks good! ^_^
            return true;
        }

        /// <summary>
        /// Draws the Pixelmap on a System.Drawing.Bitmap with the specified zoom.
        /// </summary>
        /// <param name="zoom"></param>
        /// <returns></returns>
        public Bitmap Render(int zoom = 2)
        {
            // Return cached image if it fits
            // Caching like this improves speed a TON
            if (cache.Zoom == zoom && cache.Image != null)
            {
                return cache.Image;
            }
            else
            {
                // Initialize
                Bitmap bmp = new Bitmap(_width * zoom, _height * zoom);

                // This is faster than setting individual pixels
                // TODO: test against FastPixel
                using (Graphics g = Graphics.FromImage(bmp))
                    for (int y = 0; y < _height; y++)
                        for (int x = 0; x < _width; x++)
                        {
                            // A small safety check
                            int index = y * _width + x;
                            if (index > pixels.Length) continue;

                            // And paint
                            using (var brush = new SolidBrush(palette[pixels[index]]))
                                g.FillRectangle(brush, x * zoom, y * zoom, zoom, zoom);
                        }
                

                /*FastPixel fp = new FastPixel(bmp);
                fp.Lock();

                for (int y = 0; y < _height; y++)
                    for (int x = 0; x < _width; x++)
                        for (int yy = 0; yy < zoom; yy++)
                            for (int xx = 0; xx < zoom; xx++)
                            {
                                fp.SetPixel(xx + x * zoom, yy + y * zoom, palette[pixels[x + y * _width]]);
                            }

                fp.Unlock(true);*/

                // Save in cache
                if (cache.Image != null) cache.Dispose();
                cache.Zoom = zoom;
                cache.Image = bmp;

                // Return
                return bmp;
            }
        }

        /// <summary>
        /// Draws the Pixelmap on a System.Drawing.Bitmap with the specified zoom and flipping.
        /// </summary>
        /// <param name="flipX"></param>
        /// <param name="flipY"></param>
        /// <param name="zoom"></param>
        /// <returns></returns>
        public Bitmap Render(bool flipX, bool flipY, int zoom = 2)
        {
            //! You have to make a new Bitmap
            //! Or the cached one will be flipped too
            /*Bitmap bmp = new Bitmap(Render(zoom));

            // TODO: looping and shit

            if (flipX && flipY) bmp.RotateFlip(RotateFlipType.RotateNoneFlipXY);
            else if (flipY) bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            else if (flipX) bmp.RotateFlip(RotateFlipType.RotateNoneFlipX);

            return bmp;*/

            // Initialize
            Bitmap bmp = new Bitmap(_width * zoom, _height * zoom);

            // This is faster than setting individual pixels
            // TODO: test against FastPixel
            using (Graphics g = Graphics.FromImage(bmp))
                for (int y = 0; y < _height; y++)
                    for (int x = 0; x < _width; x++)
                    {
                        // A small safety check
                        int index = y * _width + x;
                        if (index > pixels.Length) continue;

                        // apply flipping stuff
                        int aX = x, aY = y;
                        if (flipX) aX = _width - x - 1;
                        if (flipY) aY = _height - y - 1;

                        // And paint
                        using (var brush = new SolidBrush(palette[pixels[index]]))
                            g.FillRectangle(brush, aX * zoom, aY * zoom, zoom, zoom);
                    }

            // Save in cache
            //if (cache.Image != null) cache.Dispose();
            //cache.Zoom = zoom;
            //cache.Image = bmp;

            // Return
            return bmp;
        }
        

        /// <summary>
        /// Create a sub-Pixelmap of this Pixelmap.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public Pixelmap SubPixelmap(int x, int y, int width, int height)
        {
            //byte[] buffer = new byte[width * height];

            Pixelmap sub = new Pixelmap(width, height, palette);

            // Copy the necessary data
            for (int Y = 0; Y < height; Y++)
                for (int X = 0; X < width; X++)
                {
                    /*int subIndex = X + Y * width;
                    int mainIndex = (x + X) + (y + Y) * _width;

                    if (mainIndex >= pixels.Length || subIndex >= buffer.Length) continue;

                    buffer[subIndex] = pixels[mainIndex];*/
                    sub.SetPixel(X, Y, GetPixel(X + x, Y + y));
                }

            //return new Pixelmap(width, height, buffer, palette);
            return sub;
        }

        /// <summary>
        /// Create a sub-Pixelmap of this Pixelmap.
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public Pixelmap SubPixelmap(Rectangle rect)
        {
            return SubPixelmap(rect.X, rect.Y, rect.Width, rect.Height);
        }

        /// <summary>
        /// Creates a clone of this Pixelmap.
        /// </summary>
        /// <returns>The clone.</returns>
        public Pixelmap Clone()
        {
            return new Pixelmap(_width, _height, pixels, palette);
        }

        #region Properties

        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }
        }

        public Palette Palette
        {
            get { return palette; }
            set { palette = value; }
        }

        public int BitsPerPixel
        {
            get
            {
                if (palette.Length <= 16) return 4;
                else if (palette.Length <= 256) return 8;
                else return -1; // fail :(
            }
        }

        #endregion
    }
}
