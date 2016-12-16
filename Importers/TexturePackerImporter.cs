using UnityEngine;

namespace Binocle.Importers
{
    public class TexturePackerImporter
    {
        public static TexturePackerFile LoadFromJSON(string json)
        {
            return JsonUtility.FromJson<TexturePackerFile>(json);
        }
    }

}