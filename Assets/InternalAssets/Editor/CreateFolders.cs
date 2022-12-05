using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;


public class CreateFolders : EditorWindow
{
    private static string _internalAssets = "InternalAssets";
    private static string _externalAssets = "ExternalAssets";

    [MenuItem("Assets/Create Default Folders")]
    private static void SetUpFolders()
    {
        CreateFolders window = ScriptableObject.CreateInstance<CreateFolders>();
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 400, 150);
        window.ShowPopup();
    }

    private static void CreateInternalAssetsFolders()
    {
        List<string> folders = new List<string>()
        {
            "Animations",
            "Audio",
            "Editor",
            "Materials",
            "Meshes",
            "Prefabs",
            "Scripts",
            "Scene",
            "Settings",
            "Textures",
            "UI"
        };
        foreach (string folder in folders)
        {
            if (!Directory.Exists("Assets/" + folder))
            {
                Directory.CreateDirectory("Assets/" + _internalAssets + "/" + folder);
            }
        }


        List<string> uiFolders = new List<string>()
        {
            "Assets",
            "Fonts",
            "Icon"
        };
        foreach (var subFolder in uiFolders)
        {
            if (!Directory.Exists("Assets/" + _internalAssets + "/UI/" + subFolder))
            {
                Directory.CreateDirectory("Assets/" + _internalAssets + "/UI/" + subFolder);
            }
        }
        
        List<string> animationFolders = new List<string>()
        {
            "AnimationClips",
            "Controllers",
            "Avatars"
        };
        foreach (var subFolder in animationFolders)
        {
            if (!Directory.Exists("Assets/" + _internalAssets + "/Animations/" + subFolder))
            {
                Directory.CreateDirectory("Assets/" + _internalAssets + "/Animations/" + subFolder);
            }
        }
        
        List<string> audioFolders = new List<string>()
        {
            "AudioClips",
            "Mixers",
        };
        foreach (var subFolder in audioFolders)
        {
            if (!Directory.Exists("Assets/" + _internalAssets + "/Audio/" + subFolder))
            {
                Directory.CreateDirectory("Assets/" + _internalAssets + "/Audio/" + subFolder);
            }
        }
        
        List<string> settingsFolders = new List<string>()
        {
            "GameSettings",
            "URP",
        };
        foreach (var subFolder in settingsFolders)
        {
            if (!Directory.Exists("Assets/" + _internalAssets + "/Settings/" + subFolder))
            {
                Directory.CreateDirectory("Assets/" + _internalAssets + "/Settings/" + subFolder);
            }
        }
        
        AssetDatabase.Refresh();
    }

    private static void CreateExternalAssetsFolder()
    {
        if (!Directory.Exists("Assets/" + _externalAssets))
        {
            Directory.CreateDirectory("Assets/" + _externalAssets);
        }
        AssetDatabase.Refresh();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Folder Generator");
        this.Repaint();
        GUILayout.Space(70);
        if (GUILayout.Button("Generate!"))
        {
            CreateInternalAssetsFolders();
            CreateExternalAssetsFolder();
            this.Close();
        }
    }
}
