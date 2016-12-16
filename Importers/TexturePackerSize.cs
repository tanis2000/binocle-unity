using System;

namespace Binocle.Importers
{
    [Serializable]
    public class TexturePackerSize
    {
        public int w;

        public int h;


        public override string ToString()
        {
            return string.Format("{0} {1}", w, h);
        }
    }
}

