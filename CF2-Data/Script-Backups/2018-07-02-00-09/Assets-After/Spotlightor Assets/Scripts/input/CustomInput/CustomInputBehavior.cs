using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomInputBehavior : MonoBehaviour
{
	void Update ()
	{
		foreach (KeyValuePair<string, CustomInputAxis> pair in CustomInput.InputAxisDictionary) {
			CustomInputAxis inputAxis = pair.Value;

			if (inputAxis.PossitiveKey != KeyCode.None && ControlFreak2.CF2Input.GetKey (inputAxis.PossitiveKey))
				inputAxis.RawValue = 1;
			if (inputAxis.NegativeKey != KeyCode.None && ControlFreak2.CF2Input.GetKey (inputAxis.NegativeKey))
				inputAxis.RawValue = -1;

			if (inputAxis.RawValueUpdated == false && inputAxis.Setting.altInputAxisName != "")
				inputAxis.RawValue = ControlFreak2.CF2Input.GetAxisRaw (inputAxis.Setting.altInputAxisName);

			inputAxis.UpdateSmoothedValue ();
		}
	}
}
