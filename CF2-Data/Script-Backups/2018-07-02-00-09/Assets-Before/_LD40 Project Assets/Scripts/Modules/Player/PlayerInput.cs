using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	public StickyWalker walker;
	public float boostSpeedScale = 2;

	void Update ()
	{
		Vector2 dirInput = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		if (dirInput != Vector2.zero)
			walker.Facing = dirInput.normalized;
		walker.ForwardStrength = dirInput.magnitude;

		walker.speedScale = Input.GetButton ("Jump") ? boostSpeedScale : 1;
	}
}
