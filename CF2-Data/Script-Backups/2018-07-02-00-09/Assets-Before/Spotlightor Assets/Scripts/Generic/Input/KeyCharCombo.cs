using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class KeyCharCombo : MonoBehaviour
{
	public string comboString = "combo";
	public UnityEvent onComboComplete;
	private int currentInputIndex = 0;

	void Update ()
	{
		if (!string.IsNullOrEmpty (comboString)) {
			if (Input.anyKeyDown) {
				if (Input.inputString == comboString [currentInputIndex].ToString ()) {
					currentInputIndex++;
					if (currentInputIndex >= comboString.Length) {
						onComboComplete.Invoke ();
						currentInputIndex = 0;
					}
				} else
					currentInputIndex = 0;
			} 
		}
	}
}
