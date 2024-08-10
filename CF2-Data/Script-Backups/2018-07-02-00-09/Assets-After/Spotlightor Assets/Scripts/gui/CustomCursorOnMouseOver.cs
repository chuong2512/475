using UnityEngine;
using System.Collections;

public class CustomCursorOnMouseOver : MonoBehaviour
{
	public bool hideMouseCursor;
	public Vector2 offset;
	public Texture cursorTexture;
	void OnMouseEnter ()
	{
		if (hideMouseCursor)
		#if UNITY_5_3_OR_NEWER
		ControlFreak2.CFCursor.visible = false;
		#else
		ControlFreak2.CFScreen.showCursor = false;
		#endif
		enabled = true;
	}

	void OnMouseExit ()
	{
		if (hideMouseCursor)
		#if UNITY_5_3_OR_NEWER
		ControlFreak2.CFCursor.visible = true;
		#else
		ControlFreak2.CFScreen.showCursor = true;
		#endif
		enabled = false;
	}

	void OnDisable ()
	{
		if (hideMouseCursor)
		#if UNITY_5_3_OR_NEWER
		ControlFreak2.CFCursor.visible = true;
		#else
		ControlFreak2.CFScreen.showCursor = true;
		#endif
	}

	void Start ()
	{
		enabled = false;
	}

	void OnGUI ()
	{
		if (cursorTexture) {
			Vector3 mousePos = ControlFreak2.CF2Input.mousePosition;
			Rect drawRect = new Rect (mousePos.x + offset.x, Screen.height - mousePos.y + offset.y, cursorTexture.width, cursorTexture.height);
			GUI.DrawTexture (drawRect, cursorTexture);
		}
	}
}
