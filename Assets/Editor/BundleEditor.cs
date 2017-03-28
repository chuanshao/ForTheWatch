using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BundleEditor : Editor {
    [MenuItem("BundleTool/BuildAndroidLZ4Bundle")]
    static void BuildAndroidBundle()
    {
        BuildPipeline.BuildAssetBundles("Assets/MyAssetsBundle/Android" , BuildAssetBundleOptions.ChunkBasedCompression , BuildTarget.Android);
    }
    [MenuItem("BundleTool/BuildAndroidLZMABundle")]
    static void BuildAndroidLZMABundle()
    {
        BuildPipeline.BuildAssetBundles("Assets/MyAssetsBundle/AndroidLZMA", BuildAssetBundleOptions.None , BuildTarget.Android);
    }
}
