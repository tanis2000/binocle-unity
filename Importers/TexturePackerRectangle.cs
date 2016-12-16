using System;

namespace Binocle.Importers
{
    [Serializable]
    public class TexturePackerRectangle
    {
        public int x;

        public int y;

        public int w;

        public int h;


        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", x, y, w, h);
        }


    }
}

