using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class WebRequest : MonoBehaviour
{

    public Slider slider;
    public Text text;
    public Text progressText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetText());
        Debug.Log("start");
    }

    private IEnumerator GetText()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("https://www.jianshu.com/p/dbd0c1e53600"))
        {
            www.SendWebRequest();//开始请求
            while (!www.isDone)
            {
                slider.value = www.downloadProgress;
                progressText.text = Mathf.Floor(www.downloadProgress * 100) + "%";
                yield return 1;
            }
            if (www.isDone)
            {
                progressText.text = "100%";
                slider.value = 1;
            }
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string res = www.downloadHandler.text;
                Debug.Log(res);
                text.text = res;
            }
        }
    }
}
