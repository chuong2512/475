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
public class TransitionableMonoBehavior : MonoBehaviour, ITransition
{
	public event TransitionManager.TransitionBegunEventHandler TransitionInTriggered {
		add{ Transition.TransitionInTriggered += value;}
		remove{ Transition.TransitionInTriggered -= value;}
	}

	public event TransitionManager.TransitionBegunEventHandler TransitionInStarted {
		add{ Transition.TransitionInStarted += value;}
		remove{ Transition.TransitionInStarted -= value;}
	}

	public event TransitionManager.GenericEventHandler TransitionInCompleted {
		add{ Transition.TransitionInCompleted += value;}
		remove{ Transition.TransitionInCompleted -= value;}
	}

	public event TransitionManager.TransitionBegunEventHandler TransitionOutTriggered {
		add{ Transition.TransitionOutTriggered += value;}
		remove{ Transition.TransitionOutTriggered -= value;}
	}

	public event TransitionManager.TransitionBegunEventHandler TransitionOutStarted {
		add{ Transition.TransitionOutStarted += value;}
		remove{ Transition.TransitionOutStarted -= value;}
	}

	public event TransitionManager.GenericEventHandler TransitionOutCompleted {
		add{ Transition.TransitionOutCompleted += value;}
		remove{ Transition.TransitionOutCompleted -= value;}
	}

	private TransitionManager transition;

	public TransitionManager Transition {
		get {
			if (transition == null) {
				transition = GetComponent<TransitionManager> ();
				if (transition == null)
					Debug.LogError ("No TransitionManager in " + name);
			}
			return transition; 
		}
	}

	public void TransitionIn ()
	{
		Transition.TransitionIn ();
	}

	public void TransitionIn (bool instant)
	{
		Transition.TransitionIn (instant);
	}

	public void TransitionOut ()
	{
		Transition.TransitionOut ();
	}

	public void TransitionOut (bool instant)
	{
		Transition.TransitionOut (instant);
	}
}
