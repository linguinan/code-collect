/*
 * @Author: lgn 
 * @Date: 2018-05-22 15:06:01 
 * @Last Modified by:   lgn 
 * @Last Modified time: 2018-05-22 15:06:01 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class BuildPack : MonoBehaviour {

	
	[MenuItem("Tools/发布/iOS", false, 405)]
	public static void BuildReleaseIOS()
	{
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.iOS, BuildTarget.iOS);

		// PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, "");
		// AssetDatabase.Refresh();

		var scenes = getScenes();//new [] {"Assets/Scenes/Main.unity"};
		string bundleVersion = PlayerSettings.iOS.buildNumber;
		string today = DateTime.Now.ToString("yyyyMMdd");
		string locationPathName = PlayerSettings.productName + "_" + today + "_v" + PlayerSettings.bundleVersion + "_build_" + bundleVersion;
		BuildPipeline.BuildPlayer(scenes, PlayerSettings.productName , BuildTarget.iOS, BuildOptions.None);

		// PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, "Debug");
		// AssetDatabase.Refresh();
	}

	[MenuItem("Tools/发布/Android", false, 406)]
	public static void BuildReleaseAndroid()
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);

        // PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "");
        // AssetDatabase.Refresh();

        var scenes = getScenes();//new[] { "Assets/Scenes/Main.unity" };
        int bundleVersion = PlayerSettings.Android.bundleVersionCode;
        string today = DateTime.Now.ToString("yyyyMMdd");
        string locationPathName = PlayerSettings.productName + today + " v" + PlayerSettings.bundleVersion + " build " + bundleVersion + ".apk";
        BuildPipeline.BuildPlayer(scenes, locationPathName, BuildTarget.Android, BuildOptions.None);

        // PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "Debug");
        // AssetDatabase.Refresh();
    }

    private static string[] getScenes()
    {
		List<string> list = new List<string>();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.IsValid())
            {
				list.Add(scene.path);
                Debug.Log(scene.path);
            }
        }
		return list.ToArray();
    }
}
