using UnityEngine;
using System.Collections;

public class MouseHider : MonoBehaviour
{
	public KeyCode showMouseKey = KeyCode.M;

	void Start ()
	{
		if (Application.isEditor == false)
#if UNITY_5_3_OR_NEWER
			Cursor.visible = false;
#else
			Screen.showCursor = false;
#endif
	}

	void Update ()
	{
		if (Input.GetKeyDown (showMouseKey))
		#if UNITY_5_3_OR_NEWER
			Cursor.visible = true;
		#else
			Screen.showCursor = true;
		#endif
	}
}
