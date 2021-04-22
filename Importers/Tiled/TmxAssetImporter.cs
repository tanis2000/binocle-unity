using System.IO;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace Binocle.Importers.Tiled
{
    [ScriptedImporter( 1, "tmx" )]
    public class TmxAssetImporter: ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            TextAsset subAsset = new TextAsset(File.ReadAllText(ctx.assetPath));
            ctx.AddObjectToAsset("text", subAsset);
            ctx.SetMainObject(subAsset);
        }
    }
}