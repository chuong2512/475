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

public class XboxSinglePlayerEngagerStateEventTrigger : MonoBehaviour
{
	public XboxSinglePlayerEngager.StateTypes stateType;
	public UnityEvent onEnableWith;
	public UnityEvent onBegin;
	public UnityEvent onEnd;

	void OnEnable ()
	{
		if (Application.platform == RuntimePlatform.XboxOne) {
			if (XboxSinglePlayerEngager.Instance.FiniteStateMachine.CurrentState.StateId == stateType)
				onEnableWith.Invoke ();
		
			XboxSinglePlayerEngager.Instance.FiniteStateMachine.StateBegin += HandleStateBegin;
			XboxSinglePlayerEngager.Instance.FiniteStateMachine.StateEnd += HandleStateEnd;
		}
	}

	void OnDisable ()
	{
		if (Application.platform == RuntimePlatform.XboxOne) {
			XboxSinglePlayerEngager.Instance.FiniteStateMachine.StateBegin -= HandleStateBegin;
			XboxSinglePlayerEngager.Instance.FiniteStateMachine.StateEnd -= HandleStateEnd;
		}
	}

	void HandleStateBegin (XboxSinglePlayerEngager.StateTypes stateId)
	{
		if (stateId == stateType)
			onBegin.Invoke ();
	}

	void HandleStateEnd (XboxSinglePlayerEngager.StateTypes stateId)
	{
		if (stateId == stateType)
			onEnd.Invoke ();
	}
}
