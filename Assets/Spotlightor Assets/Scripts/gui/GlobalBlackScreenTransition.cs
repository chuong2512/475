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
/// Global Singleton BlackScreen.
/// Don't add it to scene. Call it from script directly.
/// </summary>
public class GlobalBlackScreenTransition : ColorScreenTransition
{
	private static GlobalBlackScreenTransition instance;

	public static GlobalBlackScreenTransition Instance {
		get {
			if (instance == null) {
				GameObject go = new GameObject ("Global Black Screen Transition");
				instance = go.AddComponent<GlobalBlackScreenTransition> ();
				instance.autoActivate = true;
				instance.durationIn = 0.5f;
				instance.durationOut = 0.4f;
				instance.delayOut = 0.1f;
				instance.easeIn = instance.easeOut = iTween.EaseType.linear;
				instance.color = Color.black;
				instance.ignoreTimeScale = true;
				DontDestroyOnLoad (go);
			}
			return instance;
		}
	}

	protected override void Awake ()
	{
		if (GlobalBlackScreenTransition.instance != null && GlobalBlackScreenTransition.instance != this) {
			GameObject.Destroy (this);
		} else
			base.Awake ();
	}
}
