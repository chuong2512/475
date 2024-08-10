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

public class CurvePlayer : MonoBehaviour
{
	public AnimationCurve curve = new AnimationCurve (new Keyframe (0, 0), new Keyframe (1, 1));

	public bool playOnAwake = true;
	public float time = 1;
	public bool ignoreTimeScale = false;

	public float Value{ get; private set; }

	void OnEnable ()
	{
		if (playOnAwake)
			Play ();
	}

	public void Play ()
	{
		StopCoroutine ("DoPlay");
		StartCoroutine ("DoPlay");
	}

	private IEnumerator DoPlay ()
	{
		float timeElapsed = 0;
		while (timeElapsed < time) {
			yield return null;
			timeElapsed += ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
			Value = curve.Evaluate (Mathf.Clamp01 (timeElapsed / time));
		}
	}
}
