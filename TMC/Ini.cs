using System;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace TMC
{
    public class IniFile
    {
        private string file;

        // For writing to the .ini
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);
        // For reading from the .ini
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
            string key, string def, StringBuilder retVal,
            int size, string filePath);

        public IniFile(string path)
        {
            // Make sure we can actually use it!
            if (!File.Exists(path))
                throw new FileNotFoundException(path + " could not be found!");
            file = path;
        }

        public string this[string Section, string Key]
        {
            get
            {
                return ReadValue(Section, Key);
            }
            set
            {
                WriteValue(Section, Key, value);
            }
        }

        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <param name="Section">Section Name</param>
        /// <param name="Key">Kay Name</param>
        /// <param name="Value">Value Name</param>
        public void WriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.file);
        }

        /// <summary>
        /// Read Data Value From the INI File
        /// </summary>
        /// <param name="Section">Section Name</param>
        /// <param name="Key">Key Name</param>
        /// <returns>The data value.</returns>
        public string ReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp,
                255, this.file);
            return temp.ToString();
        }

        /// <summary>
        /// Read Data Value From the INI File
        /// </summary>
        /// <param name="Section">Section Name</param>
        /// <param name="Key">Key Name</param>
        /// <param name="FromBase">The base of the number to return.</param>
        /// <returns>The data value.</returns>
        public int ReadInteger(string Section, string Key, int FromBase = 10)
        {
            string temp = ReadValue(Section, Key);
            return Convert.ToInt32(temp, FromBase);
        }

        /// <summary>
        /// Read Boolean Value From the INI File
        /// </summary>
        /// <param name="Section">Section Name</param>
        /// <param name="Key">Key Name</param>
        /// <returns>The boolean value.</returns>
        public bool ReadBoolean(string Section, string Key)
        {
            return Convert.ToBoolean(ReadValue(Section, Key));
        }
    }
}
