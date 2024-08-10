using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchInteractionMessagesSource : InteractionMessagesSource
{
	private bool hasAnyTouchAppeared = false;
	[SingleLineLabel()]
	public bool
		disableMouseSourceIfTouchAppeared = true;

	void Start ()
	{
		ControlFreak2.CF2Input.simulateMouseWithTouches = false;
	}

	protected override List<InteractionMessagesSender.InteractionPointerData> GetUiCursorDatas ()
	{
		List<InteractionMessagesSender.InteractionPointerData> datas = new List<InteractionMessagesSender.InteractionPointerData> ();

		/**
		 * In OSX editor, multiToucheEnabled = true, but you won't get any touches without
		 * an iOS device with "Unity Remote". In order to test with mouse, we check if there
		 * is any touch appeared.
		 **/
		if (Input.multiTouchEnabled && !hasAnyTouchAppeared && ControlFreak2.CF2Input.touchCount > 0) {
			hasAnyTouchAppeared = true;
			if (disableMouseSourceIfTouchAppeared) {
				MouseInteractionMessagesSource mouseSource = GetComponent<MouseInteractionMessagesSource> ();
				if (mouseSource != null)
					mouseSource.enabled = false;
			}
		}
		
		if (Input.multiTouchEnabled && hasAnyTouchAppeared) {
			for (int i = 0; i < ControlFreak2.CF2Input.touchCount; i++) {
				ControlFreak2.InputRig.Touch touch = ControlFreak2.CF2Input.GetTouch (i);
				bool isExisted = touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled;
			
				if (!isExisted)
					datas.Add (new InteractionMessagesSender.InteractionPointerData (touch.fingerId, true, false, touch.position));// simulate a cursor leave message
			
				datas.Add (new InteractionMessagesSender.InteractionPointerData (touch.fingerId, isExisted, true, touch.position));
			}
		}

		return datas;
	}
}
