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

public class EasyConfigureFloatKeyboardSetter : MonoBehaviour
{
	public const float GuiHintDuration = 3;
	public string label = "Float Configure";
	public string key = "save key";
	public float modifyStep = 0.1f;
	public KeyCode increaseKey = KeyCode.UpArrow;
	public KeyCode decreaseKey = KeyCode.DownArrow;
	public GUISkin guiSkin;
	private float lastChangeTime = float.MinValue;

	void Update ()
	{
		float mod = 0;
		if (ControlFreak2.CF2Input.GetKeyDown (increaseKey))
			mod = modifyStep;
		else if (ControlFreak2.CF2Input.GetKeyDown (decreaseKey))
			mod = -modifyStep;
		if (mod != 0) {
			float value = EasyConfigure.GetFloat (key);
			value += mod;
			EasyConfigure.SetFloat (key, value);
			EasyConfigure.SaveFloat (key);

			lastChangeTime = Time.time;
		}
	}

	void OnGUI ()
	{
		if (Time.time - lastChangeTime < GuiHintDuration) {
			if (guiSkin != null)
				GUI.skin = guiSkin;
			GUI.Box (new Rect (Screen.width * 0.5f - 100, 10, 200, 30), string.Format ("{0} = {1}", label, EasyConfigure.GetFloat (key)));
		}
	}
}
