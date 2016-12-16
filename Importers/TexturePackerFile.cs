using System.Collections.Generic;
using System;

namespace Binocle.Importers
{
    [Serializable]
    public class TexturePackerFile
    {
        public List<TexturePackerRegion> frames;
        public TexturePackerMeta meta;

    }
}

