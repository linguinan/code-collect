using System;
using System.IO;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Callbacks;
// #if UNITY_IOS
using UnityEditor.iOS.Xcode;
using UnityEngine;
// #endif

public class XcodeBuildScript : ScriptableObject
{

    public DefaultAsset entitlementsFile;

#if UNITY_IOS

    [PostProcessBuild, UsedImplicitly]
    public static void BuildIOS(BuildTarget buildTarget, string pathToBuiltProject)
    {
        changesToXcode(buildTarget, pathToBuiltProject);
    }

#endif

    private static void changesToXcode(BuildTarget buildTarget, string pathToBuiltProject)
    {
        // Get project PBX file
        var projPath = PBXProject.GetPBXProjectPath(pathToBuiltProject);
        var proj = new PBXProject();
        proj.ReadFromString(File.ReadAllText(projPath));
        var target = proj.TargetGuidByName("Unity-iPhone");


        //add for #import <AssetsLibrary/AssetsLibrary.h>
        proj.AddFrameworkToProject(target, "AssetsLibrary.framework", true);
        File.WriteAllText(projPath, proj.WriteToString());

        // 部分第三方sdk不能bitcode时，需禁用
        // proj.SetBuildProperty(target, "ENABLE_BITCODE", "NO");

        // 简单设置
        var dummy = CreateInstance<XcodeBuildScript>();
        var entitlementsFile = dummy.entitlementsFile;
        DestroyImmediate(dummy);
        var entitlementPath = AssetDatabase.GetAssetPath(entitlementsFile);
        proj.AddCapability(target, PBXCapabilityType.InAppPurchase, entitlementPath, false);
        proj.AddCapability(target, PBXCapabilityType.PushNotifications, entitlementPath);

        // 复杂设置用：
        // https://docs.unity3d.com/ScriptReference/iOS.Xcode.ProjectCapabilityManager.html
        // addCapability(pathToBuiltProject, projPath, proj, target);


        updateInfoSettings(pathToBuiltProject);
    }

    private static void addCapability(string pathToBuiltProject, string projPath, PBXProject proj, string target)
    {
        var dummy = CreateInstance<XcodeBuildScript>();
        var entitlementsFile = dummy.entitlementsFile;
        DestroyImmediate(dummy);

        // Copy the entitlement file to the xcode project
        var entitlementPath = AssetDatabase.GetAssetPath(entitlementsFile);
        var entitlementFileName = Path.GetFileName(entitlementPath);
        var unityTarget = PBXProject.GetUnityTargetName();
        var relativeDestination = unityTarget + "/" + entitlementFileName;
        var desPath = pathToBuiltProject + "/" + relativeDestination;
        if (File.Exists(desPath))
            File.Delete(desPath);
        FileUtil.CopyFileOrDirectory(entitlementPath, desPath);
        // Add the pbx configs to include the entitlements files on the project
        proj.AddFile(relativeDestination, entitlementFileName);
        proj.AddBuildProperty(target, "CODE_SIGN_ENTITLEMENTS", relativeDestination);

        // Add Capability
        var capManager = new ProjectCapabilityManager(projPath, entitlementFileName, unityTarget);
        capManager.AddPushNotifications(true);
        capManager.AddInAppPurchase();
        capManager.WriteToFile();
    }

    private static void updateInfoSettings(string pathToBuiltProject)
    {
         // Editing Info.plist
        var plistPath = Path.Combine(pathToBuiltProject, "Info.plist");
        var plist = new PlistDocument ();
        plist.ReadFromFile (plistPath);

        // 设定构建版本未使用加密 <key>ITSAppUsesNonExemptEncryption</key><false/>
        plist.root.SetBoolean("ITSAppUsesNonExemptEncryption", false);

        // 列为白名单，才可正常检查其他应用是否安装
        // PlistElementArray schemes = plist.root.CreateArray("LSApplicationQueriesSchemes");
        // schemes.AddString("fbauth");
        // schemes.AddString("fb");

        /* iOS9所有的app对外http协议默认要求改成https */
        // PlistElementDict dict = plist.root.CreateDict("NSAppTransportSecurity");
        // dict.SetBoolean("NSAllowsArbitraryLoads", true);

        // 可以通过编辑器直接设置了
        // PlayerSettings.iOS.allowHTTPDownload = true;


        // 部分许可使用http
        // PlistElementDict dict = plist.root.CreateDict("NSAppTransportSecurity");
        // PlistElementDict dict2 = dict.CreateDict("NSExceptionDomains");
        // PlistElementDict dict3 = dict2.CreateDict("xxx.com");
        // dict3.SetBoolean("NSIncludesSubdomains", true);
        // dict3.SetBoolean("NSTemporaryExceptionAllowsInsecureHTTPLoads", true);
        // dict3.SetString("NSTemporaryExceptionMinimumTLSVersion", "TLSv1.1");

        // Apply editing settings to Info.plist
        plist.WriteToFile (plistPath);
    }

// #endif

}