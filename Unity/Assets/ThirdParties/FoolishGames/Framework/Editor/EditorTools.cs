using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class EditorTools : Editor
{
    [MenuItem("GameObject/UI/GetRootName", false, 100)]
    static void GetRoot()
    {
        GameObject obj = Selection.activeGameObject;
        string rootPath = GetRoot(obj);
        rootPath = rootPath.Replace("GameRoot/UIRoot/", "");
        int startIndex = rootPath.IndexOf('/');
        if (startIndex > 0 && startIndex < rootPath.Length)
        {
            rootPath = rootPath.Substring(startIndex + 1);
        }
        Debug.Log(rootPath);
        Debug.LogError(rootPath);
        GUIUtility.systemCopyBuffer = rootPath;
    }

    static string GetRoot(GameObject obj)
    {
        if (obj != null)
        {
            Transform transform = obj.transform;
            string root = transform.name;
            while (transform.parent != null)
            {
                transform = transform.parent;
                root = transform.name + "/" + root;
            }

            return root;
        }

        return "";
    }

    [MenuItem("Tools/Atlas/Change Format", priority = 50)]
    public static void ChangeFormat()
    {
        var guids = AssetDatabase.FindAssets("t:Texture");
        string path = null;
        try
        {
            for (int i = 0; i < guids.Length; i++)
            {
                path = AssetDatabase.GUIDToAssetPath(guids[i]);
                if (path.EndsWith(".ttf") || path.EndsWith(".renderTexture") || path.EndsWith(".asset"))
                {
                    continue;
                }

                if (EditorUtility.DisplayCancelableProgressBar("修改图片格式", path, i * 1.0f / guids.Length))
                {
                    break;
                }

                var importer = AssetImporter.GetAtPath(path) as TextureImporter;
                if (importer.textureType == TextureImporterType.SingleChannel)
                {
                    Debug.LogError("SingleChannel: " + path);
                    continue;
                }

                var settings = importer.GetPlatformTextureSettings("iOS");
                bool reimport = !settings.overridden || settings.format != TextureImporterFormat.ASTC_4x4;
                if (reimport)
                {
                    settings.overridden = true;
                    settings.format = TextureImporterFormat.ASTC_4x4;
                    importer.SetPlatformTextureSettings(settings);
                }

                settings = importer.GetPlatformTextureSettings("Android");
                bool reimport2 = !settings.overridden || settings.format != TextureImporterFormat.ASTC_4x4;
                if (reimport2)
                {
                    settings.overridden = true;
                    settings.format = TextureImporterFormat.ASTC_4x4;
                    importer.SetPlatformTextureSettings(settings);
                }

                if (reimport || reimport2)
                {
                    AssetDatabase.ImportAsset(path);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(path);
            Debug.LogException(e);
        }

        EditorUtility.ClearProgressBar();
    }
    
    [MenuItem("Tools/Open/PersistentDataPath", false, 50)]
    static void OpenPersistentDataPath()
    {
        Process.Start("explorer.exe", Application.persistentDataPath.Replace("/", "\\"));
    }
}