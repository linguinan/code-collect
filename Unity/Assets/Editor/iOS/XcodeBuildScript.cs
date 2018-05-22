using System.IO;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Callbacks;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif

public class XcodeBuildScript
{

#if UNITY_IOS

    [PostProcessBuild, UsedImplicitly]
    public static void BuildIOS(BuildTarget buildTarget, string pathToBuiltProject)
    {
        ChangesToXcode(buildTarget, pathToBuiltProject);
    }

    public static void ChangesToXcode(BuildTarget buildTarget, string pathToBuiltProject)
    {
        // Get project PBX file
        var projPath = PBXProject.GetPBXProjectPath(pathToBuiltProject);
        var proj = new PBXProject();
        proj.ReadFromString(File.ReadAllText(projPath));
        var target = proj.TargetGuidByName("Unity-iPhone");


        //add for #import <AssetsLibrary/AssetsLibrary.h>
        proj.AddFrameworkToProject(target, "AssetsLibrary.framework", true);
        File.WriteAllText(projPath, proj.WriteToString());


        
    }

#endif

}