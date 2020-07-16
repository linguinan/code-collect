using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestDownLoad : MonoBehaviour {
    bool isDone;
    Slider slider;
    Text text;
    float progress = 0f;

    void Awake () {
        slider = GameObject.Find ("Slider").GetComponent<Slider> ();
        text = GameObject.Find ("Text").GetComponent<Text> ();
    }

    HttpDownLoad http;
    //隔天之后你需要更新
    string url = @"https://epicgames-download1.akamaized.net/Builds/UnrealEngineLauncher/Installers/Mac/EpicInstaller-10.17.0.dmg?launcherfilename=EpicInstaller-10.17.0-unrealengine-6661f98ebde842be8ac648fb9f43a262.dmg";
    string savePath;

    void Start () {
        savePath = Application.streamingAssetsPath;
        http = new HttpDownLoad ();
        http.DownLoad (url, savePath, LoadLevel);
    }

    void OnDisable () {
        print ("OnDisable");
        http.Close ();
    }

    void LoadLevel () {
        isDone = true;
    }

    void Update () {
        slider.value = http.progress;
        text.text = "资源加载中" + (slider.value * 100).ToString ("0.00") + "%";
        if (isDone) {
            print("isDone -> " + isDone);
            isDone = false;
            // string url = @"file://" + Application.streamingAssetsPath + "/test";
            // StartCoroutine (LoadScene (url));
        }
    }

    // IEnumerator LoadScene (string url) {
    //     WWW www = new WWW (url);
    //     yield return www;
    //     AssetBundle ab = www.assetBundle;
    //     SceneManager.LoadScene ("Demo2_towers");
    // }

}