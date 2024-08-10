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

public abstract class InputMethodResponder : MonoBehaviour
{
	void OnEnable ()
	{
		if (InputMethodDetector.Instance != null)
			HandleInputMethodChanged (InputMethodDetector.Instance.CurrentInputMethod, InputMethodTypes.Unkown);
		Messenger.AddListener (InputMethodDetector.MessageInputMethodChanged, OnInputMethodChanged);
	}

	private void OnInputMethodChanged (object data)
	{
		InputMethodTypes oldInputMedhod = (InputMethodTypes)data;
		HandleInputMethodChanged (InputMethodDetector.Instance.CurrentInputMethod, oldInputMedhod);
	}

	protected abstract void HandleInputMethodChanged (InputMethodTypes newInputMethod, InputMethodTypes oldInputMethod);
}
