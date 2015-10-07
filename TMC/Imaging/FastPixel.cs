using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace TMC.Imaging
{
    // A big thank you to Lin, who wrote this class for JohtoMap.
    public class FastPixel
    {
        public byte[] rgbValues = new byte[4];
        private BitmapData bmpData;
        private Bitmap bmp;
        private IntPtr bmpPtr;

        private bool locked = false;

        private int width, height;
        private bool isAlpha = false;

        public FastPixel(Bitmap bitmap)
        {
            if (bitmap.PixelFormat == (bitmap.PixelFormat | PixelFormat.Indexed))
            {
                throw new Exception("Cannot lock an indexed image!");
            }

            bmp = bitmap;
            width = bitmap.Width;
            height = bitmap.Height;
            isAlpha = (bitmap.PixelFormat == (bitmap.PixelFormat | PixelFormat.Alpha));

            rgbValues = new byte[width * height * 4];
        }

        public void Lock()
        {
            if (locked)
            {
                throw new Exception("This is already locked!");
            }

            bmpData = Bitmap.LockBits(Bounds, ImageLockMode.ReadWrite, Bitmap.PixelFormat);
            bmpPtr = bmpData.Scan0;

            if (IsAlpha)
            {
                int bytes = (this.Width * this.Height) * 4;

                Marshal.Copy(bmpPtr, rgbValues, 0, rgbValues.Length);
            }
            else
            {
                int bytes = (this.Width * this.Height) * 3;

                Marshal.Copy(bmpPtr, rgbValues, 0, rgbValues.Length);
            }

            locked = true;
        }

        public void Unlock(bool setPixels)
        {
            if (!locked)
            {
                throw new Exception("Bitmap not locked!");
            }

            if (setPixels)
            {
                Marshal.Copy(rgbValues, 0, bmpPtr, rgbValues.Length);
            }

            Bitmap.UnlockBits(bmpData);
            locked = false;
        }

        #region Pixeling

        public void Clear(Color color)
        {
            if (!locked)
            {
                throw new Exception("Bitmap not locked!");
            }

            if (this.IsAlpha)
            {
                for (int index = 0; index <= rgbValues.Length - 1; index += 4)
                {
                    this.rgbValues[index] = color.B;
                    this.rgbValues[index + 1] = color.G;
                    this.rgbValues[index + 2] = color.R;
                    this.rgbValues[index + 3] = color.A;
                }
            }
            else
            {
                for (int index = 0; index <= rgbValues.Length - 1; index += 3)
                {
                    this.rgbValues[index] = color.B;
                    this.rgbValues[index + 1] = color.G;
                    this.rgbValues[index + 2] = color.R;
                }
            }
        }

        public void SetPixel(Point location, Color color)
        {
            SetPixel(location.X, location.Y, color);
        }

        public void SetPixel(int x, int y, Color color)
        {
            if (!locked)
            {
                throw new Exception("Bitmap not locked!");
            }

            if (IsAlpha)
            {
                int index = ((y * this.Width + x) * 4);
                this.rgbValues[index] = color.B;
                this.rgbValues[index + 1] = color.G;
                this.rgbValues[index + 2] = color.R;
                this.rgbValues[index + 3] = color.A;
            }
            else
            {
                int index = ((y * this.Width + x) * 3);
                this.rgbValues[index] = color.B;
                this.rgbValues[index + 1] = color.G;
                this.rgbValues[index + 2] = color.R;
            }
        }

        public Color GetPixel(Point location)
        {
            return GetPixel(location.X, location.Y);
        }

        public Color GetPixel(int x, int y)
        {
            if (!locked)
            {
                throw new Exception("Bitmap not locked.");
            }

            if (IsAlpha)
            {
                int index = ((y * this.Width + x) * 4);
                int b = this.rgbValues[index];
                int g = this.rgbValues[index + 1];
                int r = this.rgbValues[index + 2];
                int a = this.rgbValues[index + 3];
                return Color.FromArgb(a, r, g, b);
            }
            else
            {
                int index = ((y * this.Width + x) * 3);
                int b = this.rgbValues[index];
                int g = this.rgbValues[index + 1];
                int r = this.rgbValues[index + 2];
                return Color.FromArgb(r, g, b);
            }
        }

        #endregion

        #region Properties

        public Bitmap Bitmap
        {
            get { return bmp; }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle(0, 0, Width, Height); }
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public bool IsAlpha
        {
            get { return isAlpha; }
        }

        #endregion
    }
}
