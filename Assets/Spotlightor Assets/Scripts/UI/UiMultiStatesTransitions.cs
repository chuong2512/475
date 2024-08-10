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

[RequireComponent(typeof(UiMultiStates))]
public class UiMultiStatesTransitions : MonoBehaviour
{
	public List<int> transitionInStateIds = new List<int>{1};
	public List<TransitionManager> transitions;

	void Start ()
	{
		UpdateTransitions ();
		GetComponent<UiMultiStates> ().StateChanged += HandleStateChanged;
	}

	void HandleStateChanged (UiMultiStates uiMultiStates)
	{
		UpdateTransitions ();
	}

	private void UpdateTransitions ()
	{
		bool transitionIn = transitionInStateIds.Contains (GetComponent<UiMultiStates> ().StateId);
		if (transitionIn)
			transitions.ForEach (t => t.TransitionIn ());
		else
			transitions.ForEach (t => t.TransitionOut ());
	}
}
