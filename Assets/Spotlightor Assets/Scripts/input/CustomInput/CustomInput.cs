/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class CustomInput
{
	private static Dictionary<string, CustomInputAxis> inputAxisDictionary;
	private static CustomInputBehavior inputBehavior;

	public static Dictionary<string, CustomInputAxis> InputAxisDictionary { get { return inputAxisDictionary; } }

	public static CustomInputBehavior InputBehavior { get { return inputBehavior; } }

	static CustomInput ()
	{
		inputAxisDictionary = new Dictionary<string, CustomInputAxis> ();
		CustomInputSettingsAsset settingsAsset = Resources.Load (CustomInputSettingsAsset.ResourceAssetPath, typeof(CustomInputSettingsAsset)) as CustomInputSettingsAsset;
		if (settingsAsset != null) {
			foreach (CustomInputAxisSetting axisSetting in settingsAsset.inputAxisSettings) {
				inputAxisDictionary.Add (axisSetting.axisName, new CustomInputAxis (axisSetting));
			}
		} else 
			Debug.LogError (string.Format ("Cannot load CustomInputSettingsAsset at resource path: {0}", CustomInputSettingsAsset.ResourceAssetPath));

		GameObject inputBehaviorGo = new GameObject ("[CustomInputBehavior]");
		inputBehavior = inputBehaviorGo.AddComponent<CustomInputBehavior> ();
		GameObject.DontDestroyOnLoad (inputBehaviorGo);
		GameObject.DontDestroyOnLoad (inputBehavior);
	}

	public static float GetAxis (string axisName)
	{
		CustomInputAxis inputAxis = GetInputAxisByAxisName (axisName);
		if (inputAxis != null)
			return inputAxis.SmoothedValue;
		else
			return 0;
	}
	
	public static void SetAxisRawValue (string axisName, float rawValue)
	{
		CustomInputAxis inputAxis = GetInputAxisByAxisName (axisName);
		if (inputAxis != null)
			inputAxis.RawValue = rawValue;
	}
	
	public static CustomInputAxis GetInputAxisByAxisName (string axisName)
	{
		CustomInputAxis inputAxis;
		if (inputAxisDictionary.TryGetValue (axisName, out inputAxis) == false) {
			Debug.LogWarning ("Cannot find Custom Input Axis with name: " + axisName);
			inputAxis = null;
		}
		return inputAxis;
	}

	public static void UpdateAllAxisSmoothedValues ()
	{
		foreach (KeyValuePair<string, CustomInputAxis> inputSetting in inputAxisDictionary)
			inputSetting.Value.UpdateSmoothedValue ();
	}
}
