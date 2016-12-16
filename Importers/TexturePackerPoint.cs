using System;

namespace Binocle.Importers
{
    [Serializable]
    public class TexturePackerPoint
    {
        public double x;

        public double y;


        public override string ToString()
        {
            return string.Format("{0} {1}", x, y);
        }

    }
}

