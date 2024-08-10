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
public class sUiButtonScale : FunctionalMonoBehaviour
{
	public Transform target;
	public Vector3 normalScale = Vector3.one;
	public Vector3 overScale = Vector3.one;
	public Vector3 downScale = Vector3.one;
	public Vector3 disableScale = Vector3.one;
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
		ScaleByButtonState ();
	}
	
	protected override void OnBecameUnFunctional ()
	{
		Button.StateChanged -= HandleStateChanged;
	}
	
	void HandleStateChanged (sUiButton button, sUiButton.StateTypes stateBefore, sUiButton.StateTypes stateNow)
	{
		ScaleByButtonState ();
	}
	
	private void ScaleByButtonState ()
	{
		switch (Button.StateType) {
		case sUiButton.StateTypes.Normal:
			ScaleTo (normalScale);
			break;
		case sUiButton.StateTypes.Over:
			ScaleTo (overScale);
			break;
		case sUiButton.StateTypes.Down:
			ScaleTo (downScale);
			break;
		case sUiButton.StateTypes.Disable:
			ScaleTo (disableScale);
			break;
		}
	}
	
	private void ScaleTo (Vector3 localScale)
	{
		target.localScale = localScale;
	}
	
	void Reset ()
	{
		if (target == null) {
			target = transform;
			normalScale = overScale = downScale = disableScale = target.localScale;
		}
	}
}
