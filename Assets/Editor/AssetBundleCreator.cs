using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

[ExecuteInEditMode]
public class AssetBundleCreator : EditorWindow
{
    string assetBundleName = "AssetBundle_";
    Texture2D xSprite = null;
    Texture2D oSprite = null;
    Texture2D bgSprite = null;
    string filePath;
    AssetBundle assetBundle;

    [MenuItem("Window/AssetBundleCreator")]

    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(AssetBundleCreator));
    }

    void OnGUI()
    {
        filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "AssetBundles");

        GUILayout.Label("Base Settings", EditorStyles.boldLabel);

        assetBundleName = EditorGUILayout.TextField("Asset Bundle Name", assetBundleName);

        GUILayout.Label("X Sprite");
        xSprite = EditorGUILayout.ObjectField(xSprite, typeof(Texture2D), true) as Texture2D;
        DrawOnGUISprite(xSprite, 1);

        GUILayout.Label("O Sprite");
        oSprite = EditorGUILayout.ObjectField(oSprite, typeof(Texture2D), true) as Texture2D;
        DrawOnGUISprite(oSprite, 2);

        GUILayout.Label("Background Sprite");
        bgSprite = EditorGUILayout.ObjectField(bgSprite, typeof(Texture2D), true) as Texture2D;
        DrawOnGUISprite(bgSprite, 3);

        // add a space separator between the buttons and controls
        Rect rect = GUILayoutUtility.GetRect(20, 20);

        if (GUILayout.Button("Build"))
        {
            BuildAllAssetBundles();
        }

        // load asset bundle and activate both in editorWindow and editor
        if (GUILayout.Button("Load"))
        {
            LoadAssetBundle(assetBundleName);
        }
    }
    void BuildAllAssetBundles()
    {
        ExportResource();

        // /*v TODO - fix "No AssetBundle has been set for this build." error v*/
        // AssetBundleBuild[] buildMap = new AssetBundleBuild[] { new AssetBundleBuild() };
        // buildMap[0].assetBundleName = assetBundleName; //$"{assetBundleName}.unity3d";
        // buildMap[0].assetNames = new string[] { 
        //                                 $"Assets/Textures/MoonActive/{bgSprite.name}.png"};
        // // buildMap[0].addressableNames = new string[] {
        // //                                 $"{Application.streamingAssetsPath}/MoonActive/{bgSprite.name}.png"};

        // BuildPipeline.BuildAssetBundles($"Assets/StreamingAssets/AssetBundles/",
        //                                 buildMap,
        //                                 BuildAssetBundleOptions.None,
        //                                 BuildTarget.StandaloneWindows64);


        // // BuildPipeline.BuildAssetBundles("Assets/StreamingAssets", BuildAssetBundleOptions.None, BuildTarget.Android);
        // // BuildPipeline.BuildAssetBundles("Assets/StreamingAssets", BuildAssetBundleOptions.None, BuildTarget.iOS);

        //Refresh the Project folder
        AssetDatabase.Refresh();

        // load asset as active
        LoadAssetBundle(assetBundleName);
    }

    /*v TODO - deprecated - find better way v*/
    void ExportResource()
    {
        string path = System.IO.Path.Combine(filePath, $"{assetBundleName}.unity3d");

        UnityEngine.Object[] selection = new UnityEngine.Object[] {
                                    xSprite,
                                    oSprite,
                                    bgSprite };
        BuildPipeline.BuildAssetBundle(selection[0], selection, path,
                                       BuildAssetBundleOptions.CollectDependencies
                                     | BuildAssetBundleOptions.CompleteAssets,
                                        BuildTarget.StandaloneWindows64);

        //Refresh the Project folder
        AssetDatabase.Refresh();
    }

    private void LoadAssetBundle(string assetBundleName)
    {
        var path = System.IO.Path.Combine(filePath, $"{assetBundleName}.unity3d");

        //Load AssetBundle
        assetBundle = AssetBundle.LoadFromFile(path);
        if (assetBundle == null) { return; }

        Texture2D[] loadedAssets = assetBundle.LoadAllAssets<Texture2D>();

        // do something with the sprites ->> random order when loading
        for (int i = 0; i < loadedAssets.Length; i += 1)
        {
            if (loadedAssets[i].name == "ExTarget")
            {
                xSprite = loadedAssets[i];
            }
            else if (loadedAssets[i].name == "CircleTarget")
            {
                oSprite = loadedAssets[i];
            }
            else if (loadedAssets[i].name == "EmptyBG")
            {
                bgSprite = loadedAssets[i];
            }
        }
        assetBundle.Unload(false);

        /*v TODO - remove try catch after fixing the compressed images error given by EncodeToPNG() v*/
        try
        {
            SaveSpriteToEditorPath(bgSprite, Application.streamingAssetsPath + "/EmptyBG.png");
        }
        catch { }
        try
        {
            SaveSpriteToEditorPath(xSprite, Application.streamingAssetsPath + "/PlayerIcon1.png");
        }
        catch { }
        try
        {
            SaveSpriteToEditorPath(oSprite, Application.streamingAssetsPath + "/PlayerIcon2.png");
        }
        catch { }
    }

    /*v TODO - find better way v*/
    void SaveSpriteToEditorPath(Texture2D sp, string path)
    {
        if (sp == null) { return; }
        string dir = Path.GetDirectoryName(path);
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        // remove existing file
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        // write new sprite
        File.WriteAllBytes(path, sp.EncodeToPNG());
    }
    void DrawOnGUISprite(Texture2D aSprite, int heightIdx)
    {
        GUILayout.Label("    Preview");
        float spriteW = 40;
        float spriteH = 40;
        // reserve space for preview sprite
        Rect rect = GUILayoutUtility.GetRect(spriteW, spriteH);

        if (aSprite == null) { return; }

        if (Event.current.type == EventType.Repaint)
        {
            EditorGUI.DrawPreviewTexture(new Rect(100, heightIdx * 100, spriteW, spriteH), aSprite);
        }
    }
}