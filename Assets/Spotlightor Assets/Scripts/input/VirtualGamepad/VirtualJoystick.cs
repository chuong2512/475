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

public class VirtualJoystick : VirtualJoystickBase
{
	public delegate void PressStateChangedEventHandler (bool pressed);

	public event PressStateChangedEventHandler PressStateChange;

	public float inchesForMaxStrength = 0.3f;
	public float noiseThresholdInches = 0.05f;
	public float maxStrengthSpeed = 999;
	public float borderSafeInches = 0.1f;
	public bool resetStrengthOnTouch = true;
	public bool centerFollowMaxStrength = false;
	public bool alignCenterToFixedDir = false;
	[SingleLineLabel ()]
	public bool backToPreferredPosOnRelease = false;
	[SingleLineLabel ()]
	public Vector2 preferredPosBoundOffsetInInches = new Vector2 (0.3f, 0.3f);

	public bool logInfo = false;

	private bool isPressed = false;
	private int trackingFingerId;
	private Vector2 centerPosition;
	private Vector2 fingerPosition;
	private Vector2 strength = Vector2.zero;
	private Vector2 rawStrength = Vector2.zero;

	public bool IsPressed {
		get {
			return isPressed;
		}
		private set {
			if (isPressed != value) {
				isPressed = value;

				if (!isPressed && resetStrengthOnTouch)
					strength = rawStrength = Vector2.zero;

				if (!isPressed && backToPreferredPosOnRelease)
					SetupJoystickAtPreferredPos ();
				
				if (PressStateChange != null)
					PressStateChange (value);
			}
		}
	}

	public bool HaveBeenUsed {
		get { return centerPosition.x != 0 || centerPosition.y != 0; }
	}

	public Vector2 CenterPosition { get { return this.centerPosition; } }

	public Vector2 FingerPosition { get { return this.fingerPosition; } }

	public override Vector2 Strength { get { return strength; } }

	public Vector2 RawStrength { get { return rawStrength; } }


	void Start ()
	{
		if (backToPreferredPosOnRelease)
			SetupJoystickAtPreferredPos ();
	}

	void Update ()
	{
		if (SystemInfo.deviceType == DeviceType.Handheld || ControlFreak2.CF2Input.touchCount > 0)
			UpdateInHandheldDevice ();
		else
			UpdateInDesktop ();

		if (logInfo)
			Debug.LogFormat ("ValidDpi = {0}", VirtualJoystick.ValidScreenDpi);
	}

	private void UpdateInHandheldDevice ()
	{
		if (!IsPressed) {
			for (int i = 0; i < ControlFreak2.CF2Input.touchCount; i++) {
				ControlFreak2.InputRig.Touch touch = ControlFreak2.CF2Input.GetTouch (i);
				if (touch.phase == TouchPhase.Began && IsInJoystickScreenArea (touch.position)) {
					trackingFingerId = touch.fingerId;
					IsPressed = true;
					SetupJoystickAt (touch.position);
				}
			}
		} else {
			bool newIsPressed = false;
			for (int i = 0; i < ControlFreak2.CF2Input.touchCount; i++) {
				ControlFreak2.InputRig.Touch touch = ControlFreak2.CF2Input.GetTouch (i);
				if (touch.fingerId == trackingFingerId
				    && touch.phase != TouchPhase.Ended
				    && touch.phase != TouchPhase.Canceled) {
					newIsPressed = true;
					fingerPosition = touch.position;
					UpdateStrength ();

					if (centerFollowMaxStrength && rawStrength != Vector2.zero) {
						centerPosition = -rawStrength * inchesForMaxStrength;
						centerPosition.x = ScreenInchesToPixels (centerPosition.x);
						centerPosition.y = ScreenInchesToPixels (centerPosition.y);
						centerPosition += fingerPosition;
					}

					if (fixedDirectionsCount > 0 && alignCenterToFixedDir
					    && touch.phase == TouchPhase.Stationary && rawStrength != Vector2.zero) {

						Vector2 fingerToCenterDir = -StrengthDirFixed.normalized;
						centerPosition = fingerToCenterDir * rawStrength.magnitude * inchesForMaxStrength;
						centerPosition.x = ScreenInchesToPixels (centerPosition.x);
						centerPosition.y = ScreenInchesToPixels (centerPosition.y);
						centerPosition += fingerPosition;
					}
				}
			}
			IsPressed = newIsPressed;
		}
	}

	private bool IsInJoystickScreenArea (Vector2 position)
	{
		Vector2 viewportPos = new Vector2 (position.x / Screen.width, position.y / Screen.height);
		return inputViewportArea.Contains (viewportPos);
	}

	private void UpdateInDesktop ()
	{
		if (!IsPressed) {
			if (ControlFreak2.CF2Input.GetMouseButton (0) && IsInJoystickScreenArea (ControlFreak2.CF2Input.mousePosition)) {
				IsPressed = true;
				SetupJoystickAt (new Vector2 (ControlFreak2.CF2Input.mousePosition.x, ControlFreak2.CF2Input.mousePosition.y));
			}
		} else {
			if (ControlFreak2.CF2Input.GetMouseButton (0) == false)
				IsPressed = false;
			else {
				fingerPosition = new Vector2 (ControlFreak2.CF2Input.mousePosition.x, ControlFreak2.CF2Input.mousePosition.y);
				UpdateStrength ();
			}
		}
	}

	public void ResetFingerPosition ()
	{
		fingerPosition = centerPosition;
		rawStrength = strength = Vector2.zero;
	}

	public void SetupJoystickAtPreferredPos ()
	{
		Vector2 preferredPosInInches = Vector2.one * (inchesForMaxStrength + borderSafeInches);
		preferredPosInInches += preferredPosBoundOffsetInInches;
		Vector2 preferredPos = new Vector2 (ScreenInchesToPixels (preferredPosInInches.x), ScreenInchesToPixels (preferredPosInInches.y));

		fingerPosition = preferredPos;
		strength = rawStrength = Vector2.zero;

		SetupJoystickAt (preferredPos);
	}

	private void SetupJoystickAt (Vector2 pos)
	{
		Vector2 newCenterPos = pos;
		
		float deltaXBefore = resetStrengthOnTouch ? 0 : ScreenInchesToPixels (rawStrength.x * inchesForMaxStrength);
		newCenterPos.x = pos.x - deltaXBefore;
		float deltaYBefore = resetStrengthOnTouch ? 0 : ScreenInchesToPixels (rawStrength.y * inchesForMaxStrength);
		newCenterPos.y = pos.y - deltaYBefore;
		
		float newCenterPosXinInches = ScreenPixelToInches (newCenterPos.x);
		float minCenterPosXinInches = borderSafeInches + inchesForMaxStrength;
		float maxCenterPosXinInches = ScreenPixelToInches (Screen.width * 0.5f) - minCenterPosXinInches;
		newCenterPosXinInches = Mathf.Clamp (newCenterPosXinInches, minCenterPosXinInches, maxCenterPosXinInches);
		
		float newCenterPosYinInches = ScreenPixelToInches (newCenterPos.y);
		float minCenterPosYinInches = borderSafeInches + inchesForMaxStrength;
		float maxCenterPosYinInches = ScreenPixelToInches (Screen.height * 1f) - minCenterPosYinInches;
		newCenterPosYinInches = Mathf.Clamp (newCenterPosYinInches, minCenterPosYinInches, maxCenterPosYinInches);
		
		newCenterPos.x = ScreenInchesToPixels (newCenterPosXinInches);
		newCenterPos.y = ScreenInchesToPixels (newCenterPosYinInches);
		
		centerPosition = newCenterPos;
		fingerPosition = pos;
		UpdateStrength ();
	}

	private void UpdateStrength ()
	{
		Vector2 offsetToCenter = Vector3.zero;
		offsetToCenter.x = ScreenPixelToInches (fingerPosition.x - centerPosition.x);
		offsetToCenter.y = ScreenPixelToInches (fingerPosition.y - centerPosition.y);

		if (offsetToCenter != Vector2.zero) {
			float strengthLength = Mathf.InverseLerp (0, inchesForMaxStrength, offsetToCenter.magnitude);
			rawStrength = offsetToCenter.normalized * strengthLength;
		} else
			rawStrength = Vector2.zero;

		Vector2 denoisedStrength = Vector2.zero;
		if (rawStrength != Vector2.zero) {
			float denoisedStrengthLength = Mathf.InverseLerp (noiseThresholdInches / inchesForMaxStrength, 1, rawStrength.magnitude);
			denoisedStrength = rawStrength.normalized * denoisedStrengthLength;
		}
		Vector2 deltaStrength = denoisedStrength - strength;
		float maxDeltaStrengthThisFrame = maxStrengthSpeed * Time.deltaTime;
		if (deltaStrength.magnitude > maxDeltaStrengthThisFrame)
			deltaStrength = deltaStrength.normalized * maxDeltaStrengthThisFrame;

		strength += deltaStrength;
	}

}
