using UnityEngine;
using System.Collections;

// It's not a dragging or long holding - it's a real click!
// Unity message OnMouseClick will be sent when click
public class InputMouseRealClickListener : MonoBehaviour
{
	public static float maxDurationForClick = 0.8f;
	public static float maxMouseMovementForClick = 10;
	private float lastTimeMouseDown = 0;
	private Vector3 mousePosWhenDown = Vector3.zero;
	
	protected virtual void Update ()
	{
		if (ControlFreak2.CF2Input.GetMouseButtonDown (0)) {
			lastTimeMouseDown = Time.time;
			mousePosWhenDown = ControlFreak2.CF2Input.mousePosition;
		} else if (ControlFreak2.CF2Input.GetMouseButtonUp (0)) {
			float duration = Time.time - lastTimeMouseDown;
			float movement = Vector3.Distance (ControlFreak2.CF2Input.mousePosition, mousePosWhenDown);
		
			if (duration < maxDurationForClick && movement < maxMouseMovementForClick)
				SendMessage ("OnMouseClick");
		}
	}
}
