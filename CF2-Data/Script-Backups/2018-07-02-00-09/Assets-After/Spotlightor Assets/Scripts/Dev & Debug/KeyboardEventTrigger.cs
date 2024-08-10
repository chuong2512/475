using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class KeyboardEventTrigger : MonoBehaviour
{
	public KeyCode key = KeyCode.Space;
	public UnityEvent pressedEvent;
	public UnityEvent onPressDown;
	public UnityEvent onPressUp;

	void Update ()
	{
		if (ControlFreak2.CF2Input.GetKeyDown (key))
			pressedEvent.Invoke ();

		if (ControlFreak2.CF2Input.GetKeyDown (key))
			onPressDown.Invoke ();
		if (ControlFreak2.CF2Input.GetKeyUp (key))
			onPressUp.Invoke ();
	}
}
