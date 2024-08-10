/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

public class AlignUtilities
{

	[MenuItem("GameObject/Align World X", false, 120)]
	public static void AlignWorldX ()
	{
		AlignWorldPosition (new Vector3 (1, 0, 0));
	}

	[MenuItem("GameObject/Align World Y", false, 121)]
	public static void AlignWorldY ()
	{
		AlignWorldPosition (new Vector3 (0, 1, 0));
	}

	[MenuItem("GameObject/Align World Z", false, 122)]
	public static void AlignWorldZ ()
	{
		AlignWorldPosition (new Vector3 (0, 0, 1));
	}
	
	private static void AlignWorldPosition (Vector3 axis)
	{
		GameObject[] selectedGos = Selection.gameObjects;
		if (selectedGos.Length < 2) {
			Debug.LogWarning ("Select at least 2 GameObjects in the scene!");
			return;
		}
		
		Vector3 averagePosition = Vector3.zero;
		foreach (GameObject go in selectedGos) {
			averagePosition += go.transform.position;
		}
		averagePosition /= selectedGos.Length;
		
		Undo.RecordObjects (selectedGos, "Align World Position");
		foreach (GameObject go in selectedGos) {
			Vector3 newPos = go.transform.position;
			if (axis.x != 0)
				newPos.x = averagePosition.x * axis.x;
			if (axis.y != 0)
				newPos.y = averagePosition.y * axis.y;
			if (axis.z != 0)
				newPos.z = averagePosition.z * axis.z;
			
			go.transform.position = newPos;
		}
	}

	[MenuItem("GameObject/Distribute World X", false, 123)]
	public static void DistributeInWorldX ()
	{
		GameObject[] selectedGos = Selection.gameObjects;
		if (selectedGos.Length < 2) {
			Debug.LogWarning ("Select at least 2 GameObjects in the scene!");
			return;
		}
		
		Array.Sort (selectedGos, (x, y) => x.transform.position.x.CompareTo (y.transform.position.x));
		float start = selectedGos [0].transform.position.x;
		float end = selectedGos [selectedGos.Length - 1].transform.position.x;
		float step = (end - start) / (selectedGos.Length - 1);
		for (int i = 1; i < selectedGos.Length-1; i++) {
			selectedGos [i].transform.SetPositionX ((float)i * step + start);
		}
	}

	[MenuItem("GameObject/Distribute World Y", false, 124)]
	public static void DistributeInWorldY ()
	{
		GameObject[] selectedGos = Selection.gameObjects;
		if (selectedGos.Length < 2) {
			Debug.LogWarning ("Select at least 2 GameObjects in the scene!");
			return;
		}
		
		Array.Sort (selectedGos, (x, y) => x.transform.position.y.CompareTo (y.transform.position.y));
		float start = selectedGos [0].transform.position.y;
		float end = selectedGos [selectedGos.Length - 1].transform.position.y;
		float step = (end - start) / (selectedGos.Length - 1);
		for (int i = 1; i < selectedGos.Length-1; i++) {
			selectedGos [i].transform.SetPositionY ((float)i * step + start);
		}
	}

	[MenuItem("GameObject/Distribute World Z", false, 125)]
	public static void DistributeInWorldZ ()
	{
		GameObject[] selectedGos = Selection.gameObjects;
		if (selectedGos.Length < 2) {
			Debug.LogWarning ("Select at least 2 GameObjects in the scene!");
			return;
		}
		
		Array.Sort (selectedGos, (x, y) => x.transform.position.z.CompareTo (y.transform.position.z));
		float start = selectedGos [0].transform.position.z;
		float end = selectedGos [selectedGos.Length - 1].transform.position.z;
		float step = (end - start) / (selectedGos.Length - 1);
		for (int i = 1; i < selectedGos.Length-1; i++) {
			selectedGos [i].transform.SetPositionZ ((float)i * step + start);
		}
	}

	[MenuItem("GameObject/Distribute Local X", false, 126)]
	public static void DistributeInLocalX ()
	{
		GameObject[] selectedGos = Selection.gameObjects;
		if (selectedGos.Length < 2) {
			Debug.LogWarning ("Select at least 2 GameObjects in the scene!");
			return;
		}
		
		Array.Sort (selectedGos, (x, y) => x.transform.localPosition.x.CompareTo (y.transform.localPosition.x));
		float start = selectedGos [0].transform.localPosition.x;
		float end = selectedGos [selectedGos.Length - 1].transform.localPosition.x;
		float step = (end - start) / (selectedGos.Length - 1);
		for (int i = 1; i < selectedGos.Length-1; i++) {
			selectedGos [i].transform.SetLocalPositionX ((float)i * step + start);
		}
	}
	
}
