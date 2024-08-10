/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent (typeof(GenericButton))]
public class GenericButtonClickByInput : MonoBehaviour
{
	public bool triggerOnce = true;
	public string inputName = "Submit";
	public bool activeByUiInput = true;
	private int clickTimes = 0;

	void OnEnable ()
	{
		clickTimes = 0;
	}

	void Update ()
	{
		if (!triggerOnce || clickTimes < 1) {
			bool inputActive = true;
			if (activeByUiInput)
				inputActive = EventSystem.current != null && EventSystem.current.currentInputModule != null && EventSystem.current.currentInputModule.isActiveAndEnabled;
			
			if (inputActive) {
				if (ControlFreak2.CF2Input.GetButtonDown (inputName)) {
					SendMessage ("OnClicked");
					clickTimes++;
				}
			}
		}
	}
}
