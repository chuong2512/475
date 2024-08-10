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

[RequireComponent(typeof(sUiButton))]
public class sUiButtonOffset : FunctionalMonoBehaviour
{
	public Transform target;
	public Vector3 normalLocalPos = Vector3.zero;
	public Vector3 overLocalPos = Vector3.zero;
	public Vector3 downLocalPos = Vector3.zero;
	public Vector3 disableLocalPos = Vector3.zero;
	private sUiButton button;

	private sUiButton Button {
		get {
			if (button == null)
				button = GetComponent<sUiButton> ();
			return button;
		}
	}

	protected override void OnBecameFunctional (bool forTheFirstTime)
	{
		Button.StateChanged += HandleStateChanged;
		SetLocalPositionByButtonState ();
	}

	protected override void OnBecameUnFunctional ()
	{
		Button.StateChanged -= HandleStateChanged;
	}
	
	void HandleStateChanged (sUiButton button, sUiButton.StateTypes stateBefore, sUiButton.StateTypes stateNow)
	{
		SetLocalPositionByButtonState ();
	}

	private void SetLocalPositionByButtonState ()
	{
		switch (Button.StateType) {
		case sUiButton.StateTypes.Normal:
			target.localPosition = normalLocalPos;
			break;
		case sUiButton.StateTypes.Over:
			target.localPosition = overLocalPos;
			break;
		case sUiButton.StateTypes.Down:
			target.localPosition = downLocalPos;
			break;
		case sUiButton.StateTypes.Disable:
			target.localPosition = disableLocalPos;
			break;
		}
	}
}
