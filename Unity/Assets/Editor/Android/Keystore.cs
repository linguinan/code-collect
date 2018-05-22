/*
 * @Author: lgn 
 * @Date: 2018-05-22 15:07:13 
 * @Last Modified by:   lgn 
 * @Last Modified time: 2018-05-22 15:07:13 
 */
using UnityEditor;

[InitializeOnLoad]
public class PreloadKeystoreSetting {

	//处理每次重启后都要重新输入密码才能发布的情况
	static PreloadKeystoreSetting () {
		//TODO
		PlayerSettings.Android.keystorePass = "111";//密码
		PlayerSettings.Android.keyaliasPass = "111";//密码
	}
	
}
