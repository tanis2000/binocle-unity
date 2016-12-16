using System;

namespace Binocle.Importers
{
    [Serializable]
    public class TexturePackerRegion
    {
        public string filename;

        public TexturePackerRectangle frame;

        public bool rotated;

        public bool trimmed;

        public TexturePackerRectangle spriteSourceSize;

        public TexturePackerSize sourceSize;

        public TexturePackerPoint pivot;


        public override string ToString()
        {
            return string.Format("{0} {1}", filename, frame);
        }

    }
}

