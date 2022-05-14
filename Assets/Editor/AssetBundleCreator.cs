using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class AssetBundleCreator : EditorWindow
{
    static string assetBundleName = "AssetBundle_";
    static Sprite xSprite = null;
    static Sprite oSprite = null;
    static Sprite bgSprite = null;
    static string filePath;


    [MenuItem("Window/AssetBundleCreator")]

    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(AssetBundleCreator));
    }

    void OnGUI()
    {
        filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "MoonActive");

        GUILayout.Label("Base Settings", EditorStyles.boldLabel);

        assetBundleName = EditorGUILayout.TextField("Asset Bundle Name", assetBundleName);

        GUILayout.Label("X Sprite");
        xSprite = EditorGUILayout.ObjectField(xSprite, typeof(Sprite), true) as Sprite;
        DrawOnGUISprite(xSprite, 1);

        GUILayout.Label("O Sprite");
        oSprite = EditorGUILayout.ObjectField(oSprite, typeof(Sprite), true) as Sprite;
        DrawOnGUISprite(oSprite, 2);

        GUILayout.Label("Background Sprite");
        bgSprite = EditorGUILayout.ObjectField(bgSprite, typeof(Sprite), true) as Sprite;
        DrawOnGUISprite(bgSprite, 3);

        // add a space separator between the buttons and controls
        Rect rect = GUILayoutUtility.GetRect(20, 20);

        /*v TODO - add code to build asset pack v*/
        if (GUILayout.Button("Build"))
        {
            BuildAllAssetBundles();
        }

        /*v TODO - remove after testing v*/
        if (GUILayout.Button("Load"))
        {
            LoadAssetBundle(assetBundleName, xSprite.name);
        }
    }
    void BuildAllAssetBundles()
    {
        /*v TODO - fix "No AssetBundle has been set for this build." error v*/
        // AssetBundleBuild[] buildMap = new AssetBundleBuild[] { new AssetBundleBuild() };
        // buildMap[0].assetBundleName = $"{assetBundleName}.unity3d";
        // buildMap[0].assetNames = new string[] { 
        //                                 $"Assets/Textures/MoonActive/{bgSprite.name}.png"};
        // buildMap[0].addressableNames = new string[] {
        //                                 $"{Application.streamingAssetsPath}/MoonActive/{bgSprite.name}.png"};

        // BuildPipeline.BuildAssetBundles($"Assets/StreamingAssets/AssetBundles/",
        //                                 buildMap,
        //                                 BuildAssetBundleOptions.None,
        //                                 BuildTarget.StandaloneWindows64);


        // // BuildPipeline.BuildAssetBundles("Assets/StreamingAssets", BuildAssetBundleOptions.None, BuildTarget.Android);
        // // BuildPipeline.BuildAssetBundles("Assets/StreamingAssets", BuildAssetBundleOptions.None, BuildTarget.iOS);

        ExportResource();

        // //Refresh the Project folder
        // AssetDatabase.Refresh();
    }

    /*v TODO - deprecated - find better way v*/
    static void ExportResource()
    {
        Debug.Log(filePath);
        string path = System.IO.Path.Combine(filePath, $"{assetBundleName}.unity3d");
        Debug.Log(path);
        Object[] selection = new Object[] {
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

    private void LoadAssetBundle(string assetBundleName, string objectNameToLoad)
    {
        // string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "AssetBundles");
        var path = System.IO.Path.Combine(filePath, $"{assetBundleName}.unity3d");

        //Load AssetBundle
        var assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(path);

        // yield return assetBundleCreateRequest;

        AssetBundle asseBundle = assetBundleCreateRequest.assetBundle;

        //Load the "objectNameToLoad" Asset (Use GameObject if prefab)
        AssetBundleRequest asset = asseBundle?.LoadAssetAsync<Sprite>(objectNameToLoad);

        // yield return asset;

        //Retrieve the object (Use GameObject if prefab)
        Sprite loadedAsset = asset.asset as Sprite;




// MoonActive
        // //Do something with the loaded loadedAsset  object (Load to RawImage for example) 
        // image.texture = loadedAsset;
    }

    void DrawOnGUISprite(Sprite aSprite, int heightIdx)
    {
        GUILayout.Label("    Preview");
        float spriteW = 40;
        float spriteH = 40;
        // reserve space for preview sprite
        Rect rect = GUILayoutUtility.GetRect(spriteW, spriteH);

        if (aSprite == null) { return; }

        if (Event.current.type == EventType.Repaint)
        {
            EditorGUI.DrawPreviewTexture(new Rect(100, heightIdx * 100, spriteW, spriteH), aSprite.texture);
        }

        // Rect c = aSprite.rect;
        // Debug.Log($"rect: {rect.x} | cXMax: {rect.y} | cYmin: {rect.width} | cYmax: {rect.height}");
        // if (Event.current.type == EventType.Repaint)
        // {
        //     var tex = aSprite.texture;
        //     Debug.Log($"texW: {tex.width} | texH: {tex.height}");
        //     Debug.Log($"111 cXmin: {c.xMin} | cXMax: {c.xMax} | cYmin: {c.yMin} | cYmax: {c.yMax}");
        //     c.xMin /= tex.width;
        //     c.xMax /= tex.width;
        //     c.yMin /= tex.height;
        //     c.yMax /= tex.height;
        //     Debug.Log($"222 cXmin: {c.xMin} | cXMax: {c.xMax} | cYmin: {c.yMin} | cYmax: {c.yMax}");
        //     /*v TODO - find a way to display without streching v*/
        //     GUI.DrawTexture(rect, tex);
        //     GUI.DrawTextureWithTexCoords(rect, tex, c);
        // }
    }
}