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

public class Flasher : MonoBehaviour
{
	public Transform target;
	public float visibleTime = 0.1f;
	public float invisibleTime = 0.03f;
	private List<Renderer> flashRenderers;

	private bool FlashRenderersEnabled {
		set { flashRenderers.ForEach (rd => rd.enabled = value);}
	}

	void Start ()
	{
		flashRenderers = new List<Renderer> ();

		Renderer[] childRenderers = target.GetComponentsInChildren<Renderer> ();
		foreach (Renderer rd in childRenderers) {
			if (!(rd is ParticleSystemRenderer))
				flashRenderers.Add (rd);
		}
	}

	public void Flash ()
	{
		StartCoroutine ("FlashLoop");
	}

	private IEnumerator FlashLoop ()
	{
		while (true) {
			FlashRenderersEnabled = false;

			yield return new WaitForSeconds (invisibleTime);

			FlashRenderersEnabled = true;

			yield return new WaitForSeconds (visibleTime);
		}
	}

	public void Stop ()
	{
		StopCoroutine ("FlashLoop");
		FlashRenderersEnabled = true;
	}
}
