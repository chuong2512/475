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

public static class GameObjectExtensionMethods
{
	public static T GetComponentOfType<T> (this GameObject obj) where T:class
	{
		MonoBehaviour[] allMonoBehaviours = obj.GetComponents<MonoBehaviour> ();
		foreach (MonoBehaviour monoBehaviour in allMonoBehaviours) {
			if (monoBehaviour is T)
				return monoBehaviour as T;
		}
		return null;
	}
	
	public static T[] GetComponentsOfType<T> (this GameObject obj) where T:class
	{
		MonoBehaviour[] allMonoBehaviours = obj.GetComponents<MonoBehaviour> ();
		List<T> result = new List<T> ();
		foreach (MonoBehaviour monoBehaviour in allMonoBehaviours) {
			if (monoBehaviour is T) 
				result.Add (monoBehaviour as T);
		}
		return result.ToArray ();
	}
}
