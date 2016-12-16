using System.IO;
using UnityEngine;

namespace Binocle
{
    /// <summary>
    /// Generic utility class. Most of the junk ends in there.
    /// </summary>
    public static class Utils
    {
        public static string RemoveFileExtension(string name)
        {
            int extensionPosition = name.LastIndexOf(".");
            if (extensionPosition >= 0) name = name.Substring(0, extensionPosition);
            return name;
        }

        /// <summary>
        /// Creates a boxed monochromatic sprite.
        /// This is highly inefficient and should be only used for debug.
        /// It would be much better to create a shared one-pixel texture and set the color of the sprite instead
        /// </summary>
        /// <param name="width">box width</param>
        /// <param name="height">box height</param>
        /// <param name="color">the color of the sprite</param>
        /// <returns></returns>
        public static Sprite CreateBoxSprite(int width, int height, Color color)
        {
            Texture2D t = new Texture2D(width, height, UnityEngine.TextureFormat.RGBA32, false);
            Color[] colors = new Color[width * height];
            for (int i = 0; i < width * height; i++)
            {
                colors[i] = color;
            }
            t.SetPixels(0, 0, width, height, colors, 0);
            t.Apply();
            Sprite sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f), 1);
            return sprite;
        }

        public static string ReadTextFile(string sFileName)
        {
            //Debug.Log("Reading " + sFileName);

            //Check to see if the filename specified exists, if not try adding '.txt', otherwise fail
            string sFileNameFound = "";
            if (File.Exists(sFileName))
            {
                //Debug.Log("Reading '" + sFileName + "'.");
                sFileNameFound = sFileName; //file found
            }
            else if (File.Exists(sFileName + ".txt"))
            {
                sFileNameFound = sFileName + ".txt";
            }
            else
            {
                Debug.Log("Could not find file '" + sFileName + "'.");
                return null;
            }

            StreamReader sr;
            try
            {
                sr = new StreamReader(sFileNameFound);
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("Something went wrong with read.  " + e.Message);
                return null;
            }

            string fileContents = sr.ReadToEnd();
            sr.Close();

            return fileContents;
        }

        public static void WriteTextFile(string sFilePathAndName, string sTextContents)
        {
            StreamWriter sw = new StreamWriter(sFilePathAndName);
            sw.WriteLine(sTextContents);
            sw.Flush();
            sw.Close();
        }

        public static string GetVersion()
        {
            string v = string.Empty;
            string versionTextFileNameAndPath = "version.txt";
            string versionText = Utils.ReadTextFile(versionTextFileNameAndPath);
            if (versionText != null)
            {
                v = versionText;
            }
            return v;
        }

    }
}

