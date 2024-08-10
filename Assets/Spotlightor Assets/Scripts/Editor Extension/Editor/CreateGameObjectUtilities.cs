/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEditor;
using UnityEngine;
using System.Collections;

class CreateGameObjectUtilities : ScriptableObject
{
	[MenuItem("GameObject/Create Empty At Selected %#&n")]
	static void CreateAtSamePlace ()
	{
		GameObject[] objs = Selection.gameObjects;
		GameObject go = new GameObject ();
		if (objs.Length > 0) {
			GameObject target = objs [0];
            
			if (target.GetComponent<RectTransform> () == null) {
				go.transform.position = target.transform.position;
				go.transform.rotation = target.transform.rotation;
				go.transform.parent = target.transform.parent;
			} else {
				RectTransform rectTransform = go.AddComponent<RectTransform> ();
				rectTransform.SetParent (target.transform.parent, false);
				rectTransform.anchoredPosition = target.GetComponent<RectTransform> ().anchoredPosition;
				rectTransform.sizeDelta = Vector2.zero;
			}
			go.layer = target.layer;
		}
		
		Selection.activeObject = go;
	}
}
