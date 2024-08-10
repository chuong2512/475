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

[RequireComponent (typeof(TransitionManager))]
public class TransitionOutWhenInCompleted : MonoBehaviour
{
	public float delay = 0;
	public bool ignoreTimeScale = false;
	private IEnumerator timeDelayRoutine = null;

	void Awake ()
	{
		GetComponent<TransitionManager> ().TransitionInCompleted += HandleTransitionInCompleted;
		GetComponent<TransitionManager> ().TransitionOutTriggered += HandleTransitionOutTriggered;
	}

	void HandleTransitionOutTriggered (TransitionManager source, bool isInstant, TransitionManager.StateTypes prevStateType)
	{
		if (timeDelayRoutine != null)
			StopCoroutine (timeDelayRoutine);
	}

	void HandleTransitionInCompleted (TransitionManager source)
	{
		if (delay <= 0)
			GetComponent<TransitionManager> ().TransitionOut ();
		else {
			if (timeDelayRoutine != null)
				StopCoroutine (timeDelayRoutine);

			timeDelayRoutine = DelayAndTransitionOut ();
			StartCoroutine (timeDelayRoutine);
		}
	}

	private IEnumerator DelayAndTransitionOut ()
	{
		float timeElapsed = 0;
		while (timeElapsed < delay) {
			yield return null;
			float deltaTime = ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
			timeElapsed += deltaTime;
		}

		GetComponent<TransitionManager> ().TransitionOut ();
	}
}
