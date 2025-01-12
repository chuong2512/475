/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using System.Collections;
using System;

[ExecuteInEditMode ()]
public abstract class EasyDebugGui : MonoBehaviour
{
	private class AxisButton
	{
		private const float clickInterval = 0.5f;
		public string axisName = "Horizontal";
		public float downValue = 1;
		private float lastDownTime = -100;

		public bool IsClicked {
			get {
				bool axisReachDownValue = false;
				if (axisName != "") {
					float axisValue = ControlFreak2.CF2Input.GetAxis (axisName);
					axisReachDownValue = downValue > 0 ? axisValue >= downValue : axisValue <= downValue;

					if (axisValue == 0)
						lastDownTime = -100;
				}
				bool coolDownComplete = Time.time - lastDownTime >= clickInterval;
				bool clicked = axisReachDownValue && coolDownComplete;
				if (clicked)
					lastDownTime = Time.time;
				return clicked;
			}
		}

		public AxisButton (string axis, float downValue)
		{
			this.axisName = axis;
			this.downValue = downValue;
		}
	}

	private const string focusControlName = "easy_debug_gui_bt";
	private const string unfocusControlName = "easy_debug_gui_bt_un";

	[Header ("Appearance")]
	public bool drawInEditorMode = false;
	public GUISkin skin;
	public Vector2 topLeft;
	public float verticleOffset = 10;
	public float horizontalOffset = 10;
	public float labelWidth = 100;
	public float buttonWidth = 100;
	public float lineHeight = 30;

	[Header ("Behaviors")]
	public bool drawCloseButton = false;

	[Header ("Joystick Control Settings")]
	public string gainFocusButton = "Fire";
	public string loseFocusButton = "Jump";
	public string horizontalAxis = "Horizontal";
	public string verticalAxis = "Vertical";
	public string submitButton = "Submit";

	private float top = 0;
	private float left = 0;
	private int controlX = 0;
	private int controlY = 0;
	private float lineHeightScale = 1;
	private int focusButtonX = 0;
	private int focusButtonY = 0;
	private bool isFocusing = false;
	private bool submitButtonDown = false;
	private AxisButton leftButton;
	private AxisButton rightButton;
	private AxisButton upButton;
	private AxisButton downButton;

	private float ScaledLineHeight{ get { return LineHeightScale * lineHeight; } }

	private AxisButton LeftButton {
		get {
			if (leftButton == null)
				leftButton = new AxisButton (horizontalAxis, -1);
			return leftButton;
		}
	}

	private AxisButton RightButton {
		get {
			if (rightButton == null)
				rightButton = new AxisButton (horizontalAxis, 1);
			return rightButton;
		}
	}

	private AxisButton UpButton {
		get {
			if (upButton == null)
				upButton = new AxisButton (verticalAxis, 1);
			return upButton;
		}
	}

	private AxisButton DownButton {
		get {
			if (downButton == null)
				downButton = new AxisButton (verticalAxis, -1);
			return downButton;
		}
	}

	protected float LineHeightScale {
		get { return lineHeightScale; }
		set { lineHeightScale = Mathf.Max (1, value); }
	}

	protected virtual void Update ()
	{
		if (!Application.isPlaying)
			return;

		if (!isFocusing && gainFocusButton != "" && ControlFreak2.CF2Input.GetButtonDown (gainFocusButton))
			isFocusing = true;
		else if (isFocusing && loseFocusButton != "" && ControlFreak2.CF2Input.GetButtonDown (loseFocusButton))
			isFocusing = false;

		if (isFocusing) {
			if (RightButton.IsClicked)
				focusButtonX++;
			if (LeftButton.IsClicked)
				focusButtonX = Mathf.Max (0, focusButtonX - 1);
			if (UpButton.IsClicked)
				focusButtonY = Mathf.Max (0, focusButtonY - 1);
			else if (DownButton.IsClicked)
				focusButtonY++;

			submitButtonDown = ControlFreak2.CF2Input.GetButtonDown (submitButton);
		}
	}

	private void OnGUI ()
	{
		if (!Application.isPlaying && Application.isEditor && !drawInEditorMode)
			return;
		if (skin != null)
			GUI.skin = skin;

		left = topLeft.x;
		top = topLeft.y;
		LineHeightScale = 1;

		controlX = controlY = 0;

		DrawDebugGUI ();

		if (drawCloseButton) {
			NewLine ();
			if (Button ("CLOSE"))
				enabled = false;
		}

		if (!isFocusing)
			GUI.FocusControl ("");

		focusButtonY = Mathf.Clamp (focusButtonY, 0, controlY);
		if (focusButtonY == controlY)
			focusButtonX = Mathf.Clamp (focusButtonX, 0, controlX - 1);

		submitButtonDown = false;
	}

	protected abstract void DrawDebugGUI ();

	public void DrawPrefsDataSetting (string title, Enum key, params string[] buttonLabels)
	{
		DrawPrefsDataSetting (title, BasicDataTypeStorage.ConvertEnumToStringIdentifier (key), buttonLabels);
	}

	public void DrawPrefsDataSetting (string title, string key, params string[] buttonLabels)
	{
		int keyValue = BasicDataTypeStorage.GetInt (key);
		string keyValueName = "INVALID";
		
		if (keyValue >= 0 && keyValue < buttonLabels.Length)
			keyValueName = buttonLabels [keyValue];
		
		Label (string.Format ("{0}: {1}", title, keyValueName));
		for (int i = 0; i < buttonLabels.Length; i++) {
			if (Button (buttonLabels [i]))
				BasicDataTypeStorage.SetInt (key, i);
		}
		NewLine ();
	}

	public void DrawBehaviorEnabler (string label, Behaviour behavior)
	{
		Label (label);
		if (Button (behavior.enabled ? "Disable" : "Enable"))
			behavior.enabled = !behavior.enabled;
	}

	public void DrawSavableBoolSwitch (string label, SavableBool savableBool)
	{
		Label (label);
		if (Button (savableBool.Value ? "TRUE" : "FALSE"))
			savableBool.Value = !savableBool.Value;
	}

	public int DrawIntFieldAdjuster (string label, int currentValue, int step)
	{
		DrawIntField (label, currentValue);
		if (Button ("+"))
			currentValue += step;
		if (Button ("-"))
			currentValue -= step;
		return currentValue;
	}

	public float DrawFloatFieldAdjuster (string label, float currentValue, float step)
	{
		DrawFloatField (label, currentValue);
		if (Button ("+"))
			currentValue += step;
		if (Button ("-"))
			currentValue -= step;
		return currentValue;
	}

	public void DrawIntField (string label, int currentValue)
	{
		Label (label + ": " + currentValue.ToString ());
	}

	public void DrawFloatField (string label, float currentValue)
	{
		Label (label + ": " + currentValue.ToString ());
	}

	public void Label (float width, string format, params object[] args)
	{
		Label (string.Format (format, args), width);
	}

	public void LabelFormat (string format, params object[] args)
	{
		Label (string.Format (format, args));
	}

	public void Label (string text)
	{
		Label (text, labelWidth);
	}

	public void Label (string text, float width)
	{
		GUI.Label (new Rect (left, top, width, ScaledLineHeight), text);
		left += width + horizontalOffset;
	}

	public bool Button (string label)
	{
		return Button (label, buttonWidth);
	}

	public bool Button (string label, float width)
	{
		bool isFocused = isFocusing && focusButtonX == controlX && focusButtonY == controlY;
		if (isFocused)
			GUI.SetNextControlName (focusControlName);

		bool buttonClicked = GUI.Button (new Rect (left, top, width, ScaledLineHeight), label);

		if (isFocused)
			buttonClicked |= submitButtonDown;

		if (isFocused)
			GUI.FocusControl (focusControlName);

		left += width + horizontalOffset;
		controlX++;
		return buttonClicked;
	}

	public void EmptyButtonSpace ()
	{
		left += buttonWidth + horizontalOffset;
	}

	public void NewLine ()
	{
		left = topLeft.x;
		top += verticleOffset + ScaledLineHeight;

		if (controlY == focusButtonY)
			focusButtonX = Mathf.Clamp (focusButtonX, 0, controlX - 1);

		controlX = 0;
		controlY++;
	}
	
}
