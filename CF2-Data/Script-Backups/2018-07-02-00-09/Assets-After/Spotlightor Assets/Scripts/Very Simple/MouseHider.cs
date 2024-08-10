using UnityEngine;
using System.Collections;

public class MouseHider : MonoBehaviour
{
	public KeyCode showMouseKey = KeyCode.M;

	void Start ()
	{
		if (Application.isEditor == false)
#if UNITY_5_3_OR_NEWER
			ControlFreak2.CFCursor.visible = false;
#else
			ControlFreak2.CFScreen.showCursor = false;
#endif
	}

	void Update ()
	{
		if (ControlFreak2.CF2Input.GetKeyDown (showMouseKey))
		#if UNITY_5_3_OR_NEWER
			ControlFreak2.CFCursor.visible = true;
		#else
			ControlFreak2.CFScreen.showCursor = true;
		#endif
	}
}
