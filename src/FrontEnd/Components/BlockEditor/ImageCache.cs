using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace SeniorDesign.FrontEnd.BlockEditor
{

    /// <summary>
    ///     A cache used for loading and retreiving images used
    ///     multiple times in various textures or sprites
    /// </summary>
    public static class ImageCache
    {

        /// <summary>
        ///     All bitmaps that are being used somewhere
        /// </summary>
        private static Dictionary<string, Bitmap> _cachedBitmaps = new Dictionary<string, Bitmap>();

        /// <summary>
        ///     The reversed cache to allow Bitmap -> string lookup
        /// </summary>
        private static Dictionary<Bitmap, string> _reversedCachedBitmaps = new Dictionary<Bitmap, string>();

        /// <summary>
        ///     All bitmaps being used linked to the objects that are requesting them
        /// </summary>
        private static Dictionary<string, List<object>> _objectsNeedingBitmaps = new Dictionary<string, List<object>>();

        /// <summary>
        ///     Gets a bitmap from a specific file for an object.
        ///     If the path is "Gen:{Params}", one will be generated
        ///     W# = Width of #
        ///     H# = Height of #
        ///     C# = Color of hex value #
        ///     Each is separated by a comma
        /// </summary>
        /// <param name="requester">The object requesting the bitmap</param>
        /// <param name="name">The name of the bitmap to request</param>
        /// <returns>A bitmap of the specified file name</returns>
        public static Bitmap LoadBitmap(object requester, string name)
        {

            // Check if not already loaded
            if (!_cachedBitmaps.ContainsKey(name))
            {
                Bitmap b;
                if (File.Exists(name))
                {
                    b = new Bitmap(name);
                }
                else if (name.ToLower().StartsWith("gen:"))
                {
                    // Generate a bitmap from the string
                    b = GenerateBitmap(name.Substring(4));
                }
                else
                {
                    b = new Bitmap(32, 32);
                }
                _cachedBitmaps.Add(name, b);
                _reversedCachedBitmaps.Add(b, name);
                _objectsNeedingBitmaps.Add(name, new List<object>());
            }
            // Remember that this object requested the bitmap
            _objectsNeedingBitmaps[name].Add(requester);
            return _cachedBitmaps[name];
        }

        /// <summary>
        ///     Checks to see if a given bitmap exists
        /// </summary>
        /// <param name="filename">The name of the bitmap</param>
        /// <returns>True if the bitmap exists</returns>
        public static bool ImageExists(string filename)
        {
            return (_cachedBitmaps.ContainsKey(filename) || File.Exists(filename));
        }

        /// <summary>
        ///     Unloads a bitmap, checking if it should be cleared from the cache.
        /// </summary>
        /// <param name="requester">The object unloading the bitmap</param>
        /// <param name="bitmap">The bitmap being unloaded</param>
        public static void UnloadBitmap(object requester, Bitmap bitmap)
        {

            // Double-check that the bitmap actually exists
            if (!_reversedCachedBitmaps.ContainsKey(bitmap)) return;

            // Remove the requester from the cache
            var s = _reversedCachedBitmaps[bitmap];
            _objectsNeedingBitmaps[s].Remove(requester);

            // Check if empty to unload bitmap
            if (_objectsNeedingBitmaps[s].Count <= 0)
            {
                _objectsNeedingBitmaps.Remove(s);
                _reversedCachedBitmaps.Remove(bitmap);
                _cachedBitmaps.Remove(s);
                bitmap.Dispose();
            }
        }

        /// <summary>
        ///     Parses out an instruction to generate a new bitmap
        /// </summary>
        /// <param name="parameters">The parameters to use</param>
        /// <returns>A new bitmap with the specified parameters</returns>
        private static Bitmap GenerateBitmap(string parameters)
        {
            // Split by commas
            int width = 32, height = 32;
            Color color = Color.Empty;

            var pieces = parameters.Split(',');
            foreach (var piece in pieces)
            {
                switch (char.ToLower(piece[0]))
                {
                    case 'w':
                        width = int.Parse(piece.Substring(1));
                        break;
                    case 'h':
                        height = int.Parse(piece.Substring(1));
                        break;
                    case 'f':
                        color = Color.FromArgb(int.Parse(piece.Substring(1), System.Globalization.NumberStyles.HexNumber));
                        break;
                }
            }

            var b = new Bitmap(width, height);

            // Fill with color if defined
            if (color != Color.Empty)
            {
                using (System.Drawing.Graphics gfx = System.Drawing.Graphics.FromImage(b))
                using (SolidBrush brush = new SolidBrush(color))
                {
                    gfx.FillRectangle(brush, 0, 0, width, height);
                }
            }

            return b;
        }
    }
}
