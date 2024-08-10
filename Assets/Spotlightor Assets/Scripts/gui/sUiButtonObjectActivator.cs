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

[RequireComponent(typeof(MouseEventDispatcher))]
public class sUiButtonObjectActivator : FunctionalMonoBehaviour
{
	public GameObject normalObject;
	public GameObject overObject;
	public GameObject downObject;
	public GameObject disableObject;
	private GameObject activeObject;
	private sUiButton button;
	
	private sUiButton Button {
		get {
			if (button == null)
				button = GetComponent<sUiButton> ();
			return button;
		}
	}

	public GameObject ActiveObject {
		get { return activeObject; }
		set {
			if (normalObject != null)
				normalObject.SetActive (normalObject == value);
			if (overObject != null)
				overObject.SetActive (overObject == value);
			if (downObject != null)
				downObject.SetActive (downObject == value);
			if (disableObject != null)
				disableObject.SetActive (disableObject == value);
			activeObject = value;
		}
	}

	protected override void OnBecameFunctional (bool forTheFirstTime)
	{
		Button.StateChanged += HandleButtonStateChanged;
		ActivateGameObjectByButtonState ();
	}

	protected override void OnBecameUnFunctional ()
	{
		Button.StateChanged -= HandleButtonStateChanged;
	}
	
	void HandleButtonStateChanged (sUiButton button, sUiButton.StateTypes stateBefore, sUiButton.StateTypes stateNow)
	{
		ActivateGameObjectByButtonState ();
	}

	private void ActivateGameObjectByButtonState ()
	{
		switch (Button.StateType) {
		case sUiButton.StateTypes.Normal:
			ActiveObject = normalObject;
			break;
		case sUiButton.StateTypes.Over:
			ActiveObject = overObject;
			break;
		case sUiButton.StateTypes.Down:
			ActiveObject = downObject;
			break;
		case sUiButton.StateTypes.Disable:
			ActiveObject = disableObject;
			break;
		}
	}
}
