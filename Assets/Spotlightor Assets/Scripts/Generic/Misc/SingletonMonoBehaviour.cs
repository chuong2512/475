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

/// <summary>
/// Singleton is NOT a good design pattern. Use it wisely.
/// In short, it's just a lazy initialized(out of resource control) global variable(bad).
/// </summary>
public class SingletonMonoBehaviour<T> : MonoBehaviour where T:MonoBehaviour
{
	private static T instance;

	public static T Instance {
		get {
			if (instance == null) {
				instance = GameObject.FindObjectOfType (typeof(T)) as T;
				if (instance == null) {
					GameObject go = new GameObject (string.Format ("Singleton [{0}]", typeof(T).Name));
					instance = go.AddComponent<T> ();
				}
			}
			return instance;
		}
	}
	
	protected virtual void Awake ()
	{
		if (instance != null) {
			if (instance != this)
				Destroy (gameObject);
		} else
			instance = this as T;
	}
	
	protected virtual void OnDestroy ()
	{
		instance = null;
	}
	
	protected virtual void OnApplicationQuit ()
	{
		instance = null;
	}
}
