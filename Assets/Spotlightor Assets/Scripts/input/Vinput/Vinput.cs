/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Vinput
{
	private static Dictionary<string, VinputAxis> axisDictionary = new Dictionary<string, VinputAxis> ();
	private static Dictionary<string, VinputButton> buttonDictionary = new Dictionary<string, VinputButton> ();

	static Vinput ()
	{
		SceneManager.sceneLoaded += HandleSceneLoaded;
	}

	static void HandleSceneLoaded (Scene scene, LoadSceneMode loadSceneMode)
	{
		if (loadSceneMode == LoadSceneMode.Single)
			ResetAllInputValues ();
	}

	private static void ResetAllInputValues ()
	{
		foreach (KeyValuePair<string, VinputAxis> pair in axisDictionary)
			pair.Value.AxisValue = 0;

		foreach (KeyValuePair<string, VinputButton> pair in buttonDictionary)
			pair.Value.Button = false;
	}

	public static float GetAxis (string axisName)
	{
		return GetVinputAxis (axisName).AxisValue;
	}

	public static bool GetButton (string buttonName)
	{
		return GetVinputButton (buttonName).Button;
	}

	public static bool GetButtonDown (string buttonName)
	{
		return GetVinputButton (buttonName).ButtonDown;
	}

	public static bool GetButtonUp (string buttonName)
	{
		return GetVinputButton (buttonName).ButtonUp;
	}

	public static void SetAxis (string axisName, float axisValue)
	{
		GetVinputAxis (axisName).AxisValue = axisValue;
	}

	public static void SetButton (string buttonName, bool button)
	{
		GetVinputButton (buttonName).Button = button;
	}

	private static VinputAxis GetVinputAxis (string axisName)
	{
		VinputAxis axis = null;
		if (axisDictionary.TryGetValue (axisName, out axis) == false) {
			axis = new VinputAxis ();
			axisDictionary [axisName] = axis;
		}
		return axis;
	}

	private static VinputButton GetVinputButton (string buttonName)
	{
		VinputButton button = null;
		if (buttonDictionary.TryGetValue (buttonName, out button) == false) {
			button = new VinputButton ();
			buttonDictionary [buttonName] = button;
		}
		return button;
	}
}
