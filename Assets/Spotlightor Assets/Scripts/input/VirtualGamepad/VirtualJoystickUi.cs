/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualJoystickUi : MonoBehaviour
{
	public VirtualJoystick joystick;
	public bool autoHide = false;
	public bool sizeByJoystickSize = true;
	[HideByBooleanProperty ("sizeByJoystickSize", false)]
	public float sizeOffsetByJoystickSize = 100;
	public RectTransform line;
	private Canvas canvas;

	private List<Graphic> graphics = new List<Graphic> ();

	private bool Visible {
		set {
			for (int i = 0; i < graphics.Count; i++)
				graphics [i].enabled = value;
		}
	}

	void Start ()
	{
		canvas = GetComponentInParent<Canvas> ();
		if (canvas == null)
			enabled = false;

		graphics = new List<Graphic> (GetComponentsInChildren<Graphic> ());
	}

	void Update ()
	{
		if (joystick.gameObject.activeInHierarchy && joystick.enabled && (joystick.IsPressed || !autoHide)) {
			Visible = true;

			if (sizeByJoystickSize)
				UpdateSizeByJoystickSize ();

			UpdateStrengthDisplay ();
		} else
			Visible = false;
	}

	private void UpdateSizeByJoystickSize ()
	{
		Vector2 canvasSize = canvas.GetComponent<RectTransform> ().sizeDelta;
		float size = VirtualJoystick.ScreenInchesToPixels (joystick.inchesForMaxStrength);
		size = canvasSize.x * size / (float)Screen.width;
		size *= 2;
		size += sizeOffsetByJoystickSize;
		GetComponent<RectTransform> ().sizeDelta = Vector2.one * size;
	}

	private void UpdateStrengthDisplay ()
	{
		Vector2 normalizedCenter = joystick.CenterPosition;
		normalizedCenter.x /= Screen.width;
		normalizedCenter.y /= Screen.height;

		Vector2 canvasSize = canvas.GetComponent<RectTransform> ().sizeDelta;

		GetComponent<RectTransform> ().anchoredPosition = new Vector2 (normalizedCenter.x * canvasSize.x, normalizedCenter.y * canvasSize.y);

		Vector2 normalizedFingerOffset = joystick.FingerPosition - joystick.CenterPosition;
		normalizedFingerOffset.x /= Screen.width;
		normalizedFingerOffset.y /= Screen.height;

		Vector2 lineDirPosition = new Vector2 (normalizedFingerOffset.x * canvasSize.x, normalizedFingerOffset.y * canvasSize.y);
		float lineLength = lineDirPosition.magnitude;
		float maxStrengthLineLength = VirtualJoystick.ScreenInchesToPixels (joystick.inchesForMaxStrength);
		maxStrengthLineLength = canvasSize.x * maxStrengthLineLength / Screen.width;
		lineLength = Mathf.Min (lineLength, maxStrengthLineLength);
		line.sizeDelta = new Vector2 (lineLength, line.sizeDelta.y);

		line.SetLocalEulerAngleZ (Mathf.Atan2 (lineDirPosition.y, lineDirPosition.x) * Mathf.Rad2Deg);
	}
}
