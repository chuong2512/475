using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinputAxisByInput : MonoBehaviour
{
	public string axisName = "Horizontal";

	void Update ()
	{
		Vinput.SetAxis (axisName, Input.GetAxis (axisName));
	}
}
