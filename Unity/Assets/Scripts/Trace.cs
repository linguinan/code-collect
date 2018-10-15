/*
 * @Author: lgn 
 * @Date: 2018-02-08 23:18:21 
 * @Last Modified by: lgn
 * @Last Modified time: 2018-02-08 23:33:37
 */
using System;
using System.Text;
using UnityEngine;


/** 日志打印 */
public class Trace
{

    private static object format(object[] values)
    {
        // if(values.Length == 1)
        //     return values[0];

        //减少字符串拼接导致的拆箱装箱
        StringBuilder sb = new StringBuilder();
        sb.Append(DateTime.Now.ToString("HH:mm:ss"));
        sb.Append(" - ");
        foreach (var s in values)
        {
            sb.Append(s);
            sb.Append(" ");
        }
        string content = sb.ToString();
        return content;
    }

    /// <summary>
    /// 信息
    /// </summary>
    public static void Info(params object[] values)
    {
        object content = format(values);

        Debug.Log(content);
    }

    /// <summary>
    /// 警告
    /// </summary>
    public static void Warning(params object[] values)
    {
        object content = format(values);

        Debug.LogWarning(content);
    }

    /// <summary>
    /// 错误
    /// </summary>
    public static void Error(params object[] values)
    {
        object content = format(values);

        Debug.LogError(content);
    }

        
}