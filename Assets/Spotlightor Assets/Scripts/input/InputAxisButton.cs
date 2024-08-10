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
using UnityEngine.Events;
using System.Collections;

public class InputAxisButton : MonoBehaviour
{
	public delegate void GenericEventHandler (InputAxisButton button);

	public event GenericEventHandler Clicked;

	public string axisName = "Horizontal";
	public bool invert = false;
	public float repeatDelay = 0.5f;
	public float repeatInterval = 0.2f;
	public bool activeByInputModule = true;
	public UnityEvent onClick;
	public bool logInfo = false;
	private bool isPressed = false;
	private float timeSinceLastClick = 0;
	private int repeatTimes = 0;

	public bool IsPressed {
		get { return isPressed; }
		set {
			if (isPressed != value) {
				isPressed = value;
				if (isPressed)
					OnClicked ();
			}
		}
	}

	public void Update ()
	{
		if (!activeByInputModule || EventSystem.current == null || EventSystem.current.currentInputModule.enabled) {
			if (!invert)
				IsPressed = ControlFreak2.CF2Input.GetAxis (axisName) >= 1;
			else
				IsPressed = ControlFreak2.CF2Input.GetAxis (axisName) <= -1;
		} else
			IsPressed = false;

		if (IsPressed) {
			timeSinceLastClick += Time.deltaTime;
			float delayTimeForNextClick = repeatTimes == 0 ? repeatDelay : repeatInterval;
			if (timeSinceLastClick >= delayTimeForNextClick) {
				OnClicked ();

				timeSinceLastClick = 0;
				repeatTimes++;
			}
		} else {
			timeSinceLastClick = 0;
			repeatTimes = 0;
		}

		if (logInfo)
			Debug.LogFormat ("Input {0} value = {1}, rawValue = {2}", axisName, ControlFreak2.CF2Input.GetAxis (axisName), ControlFreak2.CF2Input.GetAxisRaw (axisName));
	}

	private void OnClicked ()
	{
		if (Clicked != null)
			Clicked (this);
		
		onClick.Invoke ();
	}
}
