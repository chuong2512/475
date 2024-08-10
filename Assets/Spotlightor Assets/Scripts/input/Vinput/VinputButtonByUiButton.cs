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
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent (typeof(Button))]
public class VinputButtonByUiButton : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
{
	public string buttonName = "Jump";

	void IPointerDownHandler.OnPointerDown (PointerEventData eventData)
	{
		Vinput.SetButton (buttonName, true);
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		Vinput.SetButton (buttonName, false);
	}

	public void OnPointerExit (PointerEventData eventData)
	{
		Vinput.SetButton (buttonName, false);
	}

}
