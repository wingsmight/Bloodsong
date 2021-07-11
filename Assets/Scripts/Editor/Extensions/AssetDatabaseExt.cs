using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class AssetDatabaseExt
{
    public static void CreateOrReplaceAsset<T>(T asset, string path) where T : Object
    {
        T existingAsset = AssetDatabase.LoadAssetAtPath<T>(path);

        if (existingAsset == null)
        {
            AssetDatabase.CreateAsset(asset, path);
        }
        else
        {
            string tempFilePath = path.Replace(".asset", "") + " (temp)" + ".asset";
            string assetName = Path.GetFileNameWithoutExtension(path);
            string metaFileText = File.ReadAllText(path + ".meta");

            AssetDatabase.DeleteAsset(path);
            AssetDatabase.DeleteAsset(path + ".meta");

            AssetDatabase.CreateAsset(asset, tempFilePath);
            File.WriteAllText(tempFilePath + ".meta", metaFileText);

            AssetDatabase.RenameAsset(tempFilePath + ".meta", assetName + ".meta");
            AssetDatabase.RenameAsset(tempFilePath, assetName);

            string assetFileText = File.ReadAllText(path);
            File.WriteAllText(path, assetFileText.Replace(" (temp)", ""));

            T finalAsset = AssetDatabase.LoadAssetAtPath<T>(path);
            finalAsset.name = assetName;

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}