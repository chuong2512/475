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

public abstract class ShakeVisualizer : MonoBehaviour
{
	public Shake shake;

	void Start ()
	{
		if (shake != null) {
			shake.Started += HandleShakeStarted;
			shake.Ended += HandleShakeEnded;

			if (shake.IsShaking)
				HandleShakeStarted (shake);
		}
	}

	void HandleShakeStarted (Shake shake)
	{
		StartCoroutine ("VisualizeShakeLoop");
	}
	
	void HandleShakeEnded (Shake shake)
	{
		StopCoroutine ("VisualizeShakeLoop");
		VisualizeShake (Vector3.zero);
	}

	private IEnumerator VisualizeShakeLoop ()
	{
		while (true) {
			VisualizeShake (shake.Intensity);

			yield return null;
		}
	}

	protected virtual void Reset ()
	{
		if (shake == null)
			shake = GetComponent<Shake> ();
	}

	protected abstract void VisualizeShake (Vector3 intensity);
}
