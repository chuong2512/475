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

public class HangOnGame : MonoBehaviour
{
	public bool zeroTimeScale = true;
	public bool disableUiInput = true;
	private float timeScaleBefore = 1;
	private GameObject selectedGoBefore = null;

	void OnEnable ()
	{
		if (zeroTimeScale) {
			timeScaleBefore = Time.timeScale;
			Time.timeScale = 0;
		}
		
		if (disableUiInput) {
			if (EventSystem.current != null && EventSystem.current.currentInputModule != null) {
				selectedGoBefore = EventSystem.current.currentSelectedGameObject;
				EventSystem.current.currentInputModule.DeactivateModule ();
				EventSystem.current.currentInputModule.enabled = false;
			}
		}

	}

	void OnDisable ()
	{
		if (zeroTimeScale)
			Time.timeScale = timeScaleBefore;
		
		if (disableUiInput) {
			if (EventSystem.current != null && EventSystem.current.currentInputModule != null) {
				EventSystem.current.currentInputModule.enabled = true;
				EventSystem.current.currentInputModule.ActivateModule ();
				EventSystem.current.SetSelectedGameObject (selectedGoBefore);
			}
		}
	}
}
