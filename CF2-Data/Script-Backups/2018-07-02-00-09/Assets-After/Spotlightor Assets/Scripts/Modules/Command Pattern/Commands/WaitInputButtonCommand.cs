using UnityEngine;
using System.Collections;

public class WaitInputButtonCommand : CoroutineCommandBehavior
{
	[SerializeField]
	private string buttonName = "Submit";

	public string ButtonName {
		get { return buttonName; }
		set { buttonName = value; }
	}

	protected override IEnumerator CoroutineCommand ()
	{
		while (ControlFreak2.CF2Input.GetButtonDown (buttonName) == false)
			yield return null;
	}
}
