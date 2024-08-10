using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class ButtonCombo : MonoBehaviour
{
	public List<KeyCode> buttonSequence;
	public UnityEvent onComboComplete;
	private int currentInputIndex = 0;

	void Update ()
	{
		if (buttonSequence.Count > 0) {
			if (ControlFreak2.CF2Input.anyKeyDown) {
				if (ControlFreak2.CF2Input.GetKeyDown (buttonSequence [currentInputIndex])) {
					currentInputIndex++;
					if (currentInputIndex >= buttonSequence.Count) {
						onComboComplete.Invoke ();
						currentInputIndex = 0;
					}
				} else
					currentInputIndex = 0;
			} 
		}
	}
}
