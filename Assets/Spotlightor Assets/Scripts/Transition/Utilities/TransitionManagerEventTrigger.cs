/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class TransitionManagerEventTrigger : MonoBehaviour
{
	public TransitionManager transitionManager;
	public UnityEvent transitionInStarted;
	public UnityEvent transitionInCompleted;
	public UnityEvent transitionOutStarted;
	public UnityEvent transitionOutCompleted;

	private bool ShouldInvokeEvents {
		get {
			bool should = false;
			if (this.enabled && gameObject.activeInHierarchy)
				should = true;
			else
				should = transitionManager.gameObject == this.gameObject;
			return should;
		}
	}

	void Awake ()
	{
		if (transitionManager == null)
			transitionManager = GetComponent<TransitionManager> ();

		if (transitionManager != null) {
			transitionManager.TransitionInStarted += HandleTransitionInStarted;
			transitionManager.TransitionInCompleted += HandleTransitionInCompleted;
			transitionManager.TransitionOutStarted += HandleTransitionOutStarted;
			transitionManager.TransitionOutCompleted += HandleTransitionOutCompleted;
		}
	}

	void HandleTransitionInStarted (TransitionManager source, bool isInstant, TransitionManager.StateTypes prevStateType)
	{
		if (ShouldInvokeEvents)
			transitionInStarted.Invoke ();
	}

	void HandleTransitionInCompleted (TransitionManager source)
	{
		if (ShouldInvokeEvents)
			transitionInCompleted.Invoke ();
	}

	void HandleTransitionOutStarted (TransitionManager source, bool isInstant, TransitionManager.StateTypes prevStateType)
	{
		if (ShouldInvokeEvents)
			transitionOutStarted.Invoke ();
	}

	void HandleTransitionOutCompleted (TransitionManager source)
	{
		if (ShouldInvokeEvents)
			transitionOutCompleted.Invoke ();
	}

	void Reset ()
	{
		if (transitionManager == null) {
			transitionManager = GetComponent<TransitionManager> ();
			if (transitionManager == null)
				transitionManager = GetComponentInChildren<TransitionManager> ();
		}
	}
}
