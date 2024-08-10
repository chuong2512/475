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

[RequireComponent(typeof(TransitionManager))]
public class EnableSUiButtonsByTransition : MonoBehaviour
{
	private TransitionManager transitionManager;
	private sUiButton[] buttons;
	
	void Awake ()
	{
		buttons = GetComponentsInChildren<sUiButton> (true);
		transitionManager = GetComponent<TransitionManager> ();
		
		SetAllButtonsEnabled (transitionManager.State == TransitionManager.StateTypes.In || transitionManager.State == TransitionManager.StateTypes.Unkown);
		
		transitionManager.TransitionInCompleted += HandleTransitionInCompleted;
		transitionManager.TransitionOutTriggered += HandleTransitionOutTriggered;
	}
	
	void HandleTransitionInCompleted (TransitionManager source)
	{
		SetAllButtonsEnabled (true);
	}

	void HandleTransitionOutTriggered (TransitionManager source, bool isInstant, TransitionManager.StateTypes prevStateType)
	{
		SetAllButtonsEnabled (false);
	}

	private void SetAllButtonsEnabled (bool buttonEnabled)
	{
		foreach (sUiButton button in buttons)
			button.ButtonEnabled = buttonEnabled;
	}
}
