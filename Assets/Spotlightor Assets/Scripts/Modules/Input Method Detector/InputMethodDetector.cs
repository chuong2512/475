/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class InputMethodDetector : MonoBehaviour
{
	public delegate void InputMethodChangedEventHandler (InputMethodTypes newInputMethod, InputMethodTypes oldInputMethod);

	public delegate void ConnectedJoysticksCountChangedEventHandler (int newCount, int oldCount);

	public event InputMethodChangedEventHandler InputMethodChanged;
	public event ConnectedJoysticksCountChangedEventHandler ConnectedJoysticksCountChanged;

	public const string MessageInputMethodChanged = "input_method_changed";

	private InputMethodTypes currentInputMethod = InputMethodTypes.Unkown;
	private static InputMethodDetector instance;
	private float lastKeyboardInputTime = 0;
	private float lastJoystickInputTime = -1;
	private int connectedJoysticksCount = 0;

	public static InputMethodDetector Instance { get { return instance; } }

	public InputMethodTypes CurrentInputMethod {
		get {
			if (currentInputMethod == InputMethodTypes.Unkown)
				SetCurrentInputMethodByPlatform ();
			return currentInputMethod;
		}
		private set {
			if (value != currentInputMethod) {
				InputMethodTypes oldInputMethod = currentInputMethod;
				currentInputMethod = value;
				if (InputMethodChanged != null)
					InputMethodChanged (currentInputMethod, oldInputMethod);
				Messenger.Broadcast (MessageInputMethodChanged, oldInputMethod, MessengerMode.DONT_REQUIRE_LISTENER);
//				Debug.Log (string.Format ("Input method changed to {0} from {1}", currentInputMethod, oldInputMethod));
			}
		}
	}

	public int ConnectedJoysticksCount {
		get { return connectedJoysticksCount; }
		set {
			if (connectedJoysticksCount != value) {
				int oldCount = connectedJoysticksCount;
				connectedJoysticksCount = value;

//				this.Log ("Connected joysticks count change from {0} to {1}", oldCount, connectedJoysticksCount);

				if (ConnectedJoysticksCountChanged != null)
					ConnectedJoysticksCountChanged (connectedJoysticksCount, oldCount);
			}
		}
	}

	private static bool AnyJoystickButtonDown {
		get {
			bool result = false;

			for (int i = (int)KeyCode.JoystickButton0; i <= (int)KeyCode.JoystickButton19; i++) {
				if (ControlFreak2.CF2Input.GetKeyDown ((KeyCode)i)) {
					result = true;
//					this.Log ("Detect Joystick Button {0} Down", (KeyCode)i);

					break;
				}
			}

			return result;
		}
	}

	private void SetCurrentInputMethodByPlatform ()
	{
		if (Application.platform == RuntimePlatform.XboxOne) {
			lastJoystickInputTime = Time.realtimeSinceStartup;
			CurrentInputMethod = InputMethodTypes.JoystickXbox;
		} else if (Application.platform == RuntimePlatform.PS4) {
			lastJoystickInputTime = Time.realtimeSinceStartup;
			CurrentInputMethod = InputMethodTypes.JoystickPlayStation;
		} else {
			lastKeyboardInputTime = Time.realtimeSinceStartup;
			CurrentInputMethod = InputMethodTypes.KeyboardMouse;
		}

//		Debug.Log (string.Format ("CurrentInputMethod set to {0} by platform: {1}", currentInputMethod, Application.platform));
	}

	void Awake ()
	{
		instance = this;
		DontDestroyOnLoad (gameObject);
	}

	public void ForceChangeInputMethod (InputMethodTypes inputMethod)
	{
		if (inputMethod == InputMethodTypes.KeyboardMouse)
			lastKeyboardInputTime = lastJoystickInputTime + 1;
		else
			lastJoystickInputTime = lastKeyboardInputTime + 1;
		
		CurrentInputMethod = inputMethod;
	}

	void Update ()
	{
		if (CurrentInputMethod != InputMethodTypes.KeyboardMouse)
			UpdateKeyboardMouseTimestamp ();

		UpdateHasJoystickConnected ();
		
		if (CurrentInputMethod == InputMethodTypes.KeyboardMouse)
			UpdateJoystickTimestamp ();

		if (lastJoystickInputTime > lastKeyboardInputTime) {
			if (Application.platform == RuntimePlatform.XboxOne)
				CurrentInputMethod = InputMethodTypes.JoystickXbox;
			else if (Application.platform == RuntimePlatform.PS4)
				currentInputMethod = InputMethodTypes.JoystickPlayStation;
			else
				CurrentInputMethod = InputMethodTypes.Joystick;
		} else
			CurrentInputMethod = InputMethodTypes.KeyboardMouse;
	}

	private void UpdateKeyboardMouseTimestamp ()
	{
		bool hasKeyboardMouse = 
			Application.platform == RuntimePlatform.WindowsEditor
			|| Application.platform == RuntimePlatform.WindowsPlayer
			|| Application.platform == RuntimePlatform.OSXEditor
			|| Application.platform == RuntimePlatform.OSXPlayer
			|| Application.platform == RuntimePlatform.LinuxPlayer
			#if !UNITY_5_4_OR_NEWER
			|| Application.platform == RuntimePlatform.WindowsWebPlayer
			|| Application.platform == RuntimePlatform.OSXWebPlayer
			#endif
			|| Application.platform == RuntimePlatform.WebGLPlayer;
		
		bool keyboardMouseInputDetected = false;

		if (hasKeyboardMouse) {
			if (ControlFreak2.CF2Input.anyKeyDown) {
				if (string.IsNullOrEmpty (Input.inputString) == false
				    || AnyJoystickButtonDown == false) {

					keyboardMouseInputDetected = true;
//					this.Log ("Detect keyboard key down [{0}] isNullOrEmpty = {1}", Input.inputString, string.IsNullOrEmpty (Input.inputString));
				}
			}

			if (!keyboardMouseInputDetected) {
				if (ControlFreak2.CF2Input.GetMouseButtonDown (0) || ControlFreak2.CF2Input.GetMouseButtonDown (1)) {
					keyboardMouseInputDetected = true;
//				this.Log ("Detect mouse button down");
				}

			}

			if (!keyboardMouseInputDetected) {
				float mouseMovement = new Vector2 (ControlFreak2.CF2Input.GetAxis ("Mouse X"), ControlFreak2.CF2Input.GetAxis ("Mouse Y")).magnitude;
				if (mouseMovement > 0.5f) {
					keyboardMouseInputDetected = true;
//				this.Log ("Detect mouse movement ({0})", mouseMovement);
				}
			}
		}

		if (keyboardMouseInputDetected)
			lastKeyboardInputTime = Time.realtimeSinceStartup;
	}

	private void UpdateHasJoystickConnected ()
	{
		int count = 0;
		string[] joystickNames = Input.GetJoystickNames ();
		foreach (string joystickName in joystickNames) {
			if (string.IsNullOrEmpty (joystickName) == false)
				count++;
		}

		this.ConnectedJoysticksCount = count;
	}

	private void UpdateJoystickTimestamp ()
	{
		bool joystickInputDetected = false;

		joystickInputDetected = AnyJoystickButtonDown;

		if (!joystickInputDetected) {
			float joystickDirMovement = new Vector2 (ControlFreak2.CF2Input.GetAxis ("Joystick Horizontal"), ControlFreak2.CF2Input.GetAxis ("Joystick Vertical")).magnitude;
			if (joystickDirMovement > 0.5f) {
				joystickInputDetected = true;
//				this.Log ("Detect Joystick Dir Movement {0:0.00}", joystickDirMovement);
			}
		}

		if (joystickInputDetected)
			lastJoystickInputTime = Time.realtimeSinceStartup;
	}

}
