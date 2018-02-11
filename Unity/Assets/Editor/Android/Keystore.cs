using UnityEditor;

[InitializeOnLoad]
public class PreloadKeystoreSetting {

	//处理每次重启后都要重新输入密码才能发布的情况
	static PreloadKeystoreSetting () {
		PlayerSettings.Android.keystorePass = "111";//密码
		PlayerSettings.Android.keyaliasPass = "111";//密码
	}
	
}
