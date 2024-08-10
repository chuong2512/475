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
public class sUiButtonKeyBinding : MonoBehaviour
{
	public KeyCode keyCode = KeyCode.Alpha1;
	public bool activeWhenTyping = false;

	void Update ()
	{
		if (keyCode != KeyCode.None) {
			if (ControlFreak2.CF2Input.GetKeyDown (keyCode))
				SimulatePressMessages ();
			else if (ControlFreak2.CF2Input.GetKeyUp (keyCode))
				SimulateReleaseMessages ();
		}
	}

	void OnGUI ()
	{
		if (activeWhenTyping) {
			if (Event.current.keyCode == keyCode) {
				SimulatePressMessages ();
				SimulateReleaseMessages ();
			}
		}
	}

	private void SimulatePressMessages ()
	{
		gameObject.SendMessage ("OnPressDown", SendMessageOptions.DontRequireReceiver);
		gameObject.SendMessage ("OnHoverIn", SendMessageOptions.DontRequireReceiver);
	}

	private void SimulateReleaseMessages ()
	{
		gameObject.SendMessage ("OnSelect", SendMessageOptions.DontRequireReceiver);
		gameObject.SendMessage ("OnPressUp", SendMessageOptions.DontRequireReceiver);
		gameObject.SendMessage ("OnHoverOut", SendMessageOptions.DontRequireReceiver);
	}
}
