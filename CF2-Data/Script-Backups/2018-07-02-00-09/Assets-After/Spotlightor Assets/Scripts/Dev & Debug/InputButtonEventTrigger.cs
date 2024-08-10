using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class InputButtonEventTrigger : MonoBehaviour
{
	public string buttonName = "Jump";
	public UnityEvent onButtonDown;
	public UnityEvent onButtonUp;

	void Update ()
	{
		if (ControlFreak2.CF2Input.GetButtonDown (buttonName))
			onButtonDown.Invoke ();
		if (ControlFreak2.CF2Input.GetButtonUp (buttonName))
			onButtonUp.Invoke ();
	}
}
