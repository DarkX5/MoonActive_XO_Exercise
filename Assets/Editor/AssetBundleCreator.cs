using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;

public class AssetBundleCreator : EditorWindow
{
    string assetBundleName = "AssetBundle_";
    Object xSprite = null;
    Object oSprite = null;
    Object bgSprite = null;


    [MenuItem("Window/AssetBundleCreator")]

    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(AssetBundleCreator));
    }

    void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);

        assetBundleName = EditorGUILayout.TextField("Asset Bundle Name", assetBundleName);

        GUILayout.Label("X Sprite");
        xSprite = EditorGUILayout.ObjectField(xSprite, typeof(Sprite), true);

        GUILayout.Label("O Sprite");
        oSprite = EditorGUILayout.ObjectField(oSprite, typeof(Sprite), true);

        GUILayout.Label("Background Sprite");
        bgSprite = EditorGUILayout.ObjectField(bgSprite, typeof(Sprite), true);

        /*v TODO - add code to build asset pack v*/
        if (GUILayout.Button("Build"))
        {
            BuildAllAssetBundles();
        }
    }
    void BuildAllAssetBundles()
    {
        BuildPipeline.BuildAssetBundles("Assets/StreamingAssets", BuildAssetBundleOptions.None, BuildTarget.Android);
        // BuildPipeline.BuildAssetBundles("Assets/StreamingAssets", BuildAssetBundleOptions.None, BuildTarget.iOS);
    }
}