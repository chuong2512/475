/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(Button))]
public class ButtonExtended : GenericButton, IPointerClickHandler,
IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, 
ISubmitHandler, ISelectHandler, IDeselectHandler,
IMoveHandler
{
	public delegate void BaseEventHandler (BaseEventData EventData);
	
	public delegate void PointerEventHandler (PointerEventData pointerEventData);
	
	public delegate void AxisEventHandler (AxisEventData EventData);

	public event PointerEventHandler PointerClicked;
	public event BaseEventHandler Submitted;
	public event BaseEventHandler Selected;
	public event BaseEventHandler Deselected;
	public event PointerEventHandler PointerDown;
	public event PointerEventHandler PointerEntered;
	public event PointerEventHandler PointerExited;
	public event PointerEventHandler PointerUp;
	public event AxisEventHandler Moved;

	public override bool Interactable {
		get { return UiButton.interactable; }
		set { UiButton.interactable = value; }
	}

	public Button UiButton{ get { return GetComponent<Button> (); } }

	public void OnPointerClick (PointerEventData eventData)
	{
		if (Interactable) {
			if (PointerClicked != null)
				PointerClicked (eventData);
			OnClicked ();
		}
	}

	public Selectable UiSelectable{ get { return GetComponent<Selectable> (); } }
	
	public void OnSubmit (BaseEventData eventData)
	{
		if (Interactable) {
			if (Submitted != null)
				Submitted (eventData);

			OnClicked ();
		}
	}
	
	public void OnDeselect (BaseEventData eventData)
	{
		if (Interactable) {
			if (Deselected != null)
				Deselected (eventData);
		}
	}
	
	public void OnMove (AxisEventData  axisEventData)
	{
		if (Interactable) {
			if (Moved != null)
				Moved (axisEventData);
		}
	}
	
	public void OnPointerDown (PointerEventData  pointerEventData)
	{
		if (Interactable) {
			if (PointerDown != null)
				PointerDown (pointerEventData);

			OnPressed ();
		}
	}
	
	public void OnPointerEnter (PointerEventData  pointerEventData)
	{
		if (Interactable) {
			if (PointerEntered != null)
				PointerEntered (pointerEventData);
		}
	}
	
	public void OnPointerExit (PointerEventData  pointerEventData)
	{
		if (Interactable) {
			if (PointerExited != null)
				PointerExited (pointerEventData);
		}
	}
	
	public void OnPointerUp (PointerEventData  pointerEventData)
	{
		if (Interactable) {
			if (PointerUp != null)
				PointerUp (pointerEventData);
		}
	}
	
	public void OnSelect (BaseEventData eventData)
	{
		if (Interactable) {
			if (Selected != null)
				Selected (eventData);
		}
	}
}
