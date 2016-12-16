using System;

namespace Binocle.Importers
{
    [Serializable]
    public class TexturePackerMeta
    {
        public string app;

        public string version;

        public string image;

        public string format;

        public TexturePackerSize size;

        public float scale;

        public string smartupdate;


        public override string ToString()
        {
            return image;
        }

    }
}

