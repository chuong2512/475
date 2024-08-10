using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinputButtonByInput : MonoBehaviour
{
	public string buttonName = "Jump";

	void Update ()
	{
		Vinput.SetButton (buttonName, Input.GetButton (buttonName));
	}
}
