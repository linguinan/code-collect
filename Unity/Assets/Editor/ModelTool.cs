﻿/*
 * @Author: collect from web  
 * @Date: 2018-05-22 15:10:38 
 * @Last Modified by: lgn
 * @Last Modified time: 2018-05-22 15:12:29
 */
using UnityEditor;
using UnityEngine;

public class ModelTool {

	[MenuItem ("Tools/Center Model")]
	static void CenterModel () 
	{
		Transform parent = 	Selection.activeGameObject.transform;
		Vector3 postion = parent.position;
		Quaternion rotation = parent.rotation;
		Vector3 scale = parent.localScale;
		parent.position = Vector3.zero;
		parent.rotation = Quaternion.Euler(Vector3.zero);
		parent.localScale = Vector3.one;
 
 
		Vector3 center = Vector3.zero;
		Renderer[] renders = parent.GetComponentsInChildren<Renderer>();
		foreach (Renderer child in renders){
			center += child.bounds.center;   
		}
		center /= parent.GetComponentsInChildren<Transform>().Length; 
		Bounds bounds = new Bounds(center,Vector3.zero);
		foreach (Renderer child in renders){
			bounds.Encapsulate(child.bounds);   
		}
	
		parent.position = postion;
		parent.rotation = rotation;
		parent.localScale = scale;
 
		foreach(Transform t in parent){
			t.position = t.position -  bounds.center;
		}
		//查找并设置模型的中心点
		parent.transform.position = bounds.center + parent.position;
	}
 
}
