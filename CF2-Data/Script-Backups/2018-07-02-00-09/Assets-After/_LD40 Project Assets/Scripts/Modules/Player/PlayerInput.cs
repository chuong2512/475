using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	public StickyWalker walker;
	public float boostSpeedScale = 2;

	void Update ()
	{
		Vector2 dirInput = new Vector2 (ControlFreak2.CF2Input.GetAxis ("Horizontal"), ControlFreak2.CF2Input.GetAxis ("Vertical"));
		if (dirInput != Vector2.zero)
			walker.Facing = dirInput.normalized;
		walker.ForwardStrength = dirInput.magnitude;

		walker.speedScale = ControlFreak2.CF2Input.GetButton ("Jump") ? boostSpeedScale : 1;
	}
}
