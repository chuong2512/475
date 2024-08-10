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

[RequireComponent(typeof(sGUISkinableButton))]
public class sGUIMultiStateButton : MonoBehaviour
{
	public sGUIButtonSkin[] additionalSkins;
	private int state = 0;
	private sGUISkinableButton button;
	private sGUIButtonSkin defaultSkin;

	public int State {
		get { return state; }
		set {
			state = value;
			ChangeApperanceByCurrentState ();
		}
	}
	
	protected virtual void ChangeApperanceByCurrentState ()
	{
		if (State <= additionalSkins.Length) {
			if (defaultSkin == null)
				defaultSkin = Button.buttonSkin;
			if (State == 0)
				Button.ChangeButtonSkin (defaultSkin);
			else
				Button.ChangeButtonSkin (additionalSkins [State - 1]);
		}
	}

	public sGUISkinableButton Button {
		get {
			if (button == null)
				button = gameObject.GetComponent<sGUISkinableButton> ();
			return button;
		}
	}
	
	private void Reset ()
	{
		if (Application.isPlaying)
			return;
		additionalSkins = new sGUIButtonSkin[1];
	}
}
