using UnityEngine;
using System.Collections;

public class HideCursorByInputMethod : InputMethodResponder
{
	protected override void HandleInputMethodChanged (InputMethodTypes newInputMethod, InputMethodTypes oldInputMethod)
	{
		ControlFreak2.CFCursor.visible = newInputMethod == InputMethodTypes.KeyboardMouse;
	}
}
