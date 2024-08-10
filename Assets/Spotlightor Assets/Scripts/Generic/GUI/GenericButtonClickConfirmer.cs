/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(GenericButton))]
public class GenericButtonClickConfirmer : MonoBehaviour
{
	public float timeOutDelay = 1f;

	[Header("Events")]
	public UnityEvent onConfirmStart;
	public UnityEvent onConfirmStop;
	public UnityEvent onConfirmedClick;

	private float confirmTimeElapsed = 0;
	private bool isConfirming = false;

	public bool IsConfirming {
		get { return isConfirming; }
		set {
			if (isConfirming != value) {
				isConfirming = value;
				if (isConfirming)
					onConfirmStart.Invoke ();
				else
					onConfirmStop.Invoke ();
			}
			confirmTimeElapsed = 0;
		}
	}

	void OnEnable ()
	{
		GetComponent<GenericButton> ().Clicked += HandleClicked;
		IsConfirming = false;
	}

	void OnDisable ()
	{
		GetComponent<GenericButton> ().Clicked -= HandleClicked;
		IsConfirming = false;
	}

	void HandleClicked (GenericButton button)
	{
		if (IsConfirming) {
			onConfirmedClick.Invoke ();
			IsConfirming = false;
		} else
			IsConfirming = true;
	}

	void Update ()
	{
		if (IsConfirming) {
			confirmTimeElapsed += Time.unscaledDeltaTime;
			if (confirmTimeElapsed >= timeOutDelay)
				IsConfirming = false;
		}
	}
}
