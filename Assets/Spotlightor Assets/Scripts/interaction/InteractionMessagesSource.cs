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
using System.Collections.Generic;

[RequireComponent(typeof(InteractionMessagesSender))]
public abstract class InteractionMessagesSource : MonoBehaviour
{
	public List<InteractionMessagesSender.MessageCameraSetting> messageCameraSettings;
	private InteractionMessagesSender sender;
	
	public InteractionMessagesSender Sender {
		get {
			if (sender == null)
				sender = GetComponent<InteractionMessagesSender> ();
			return sender;
		}
	}

	void Awake ()
	{
		messageCameraSettings.ForEach (cameraSetting => cameraSetting.camera.eventMask = 0);
	}

	void Update ()
	{
		List<InteractionMessagesSender.InteractionPointerData> uiCursorDatas = GetUiCursorDatas ();
		foreach (InteractionMessagesSender.InteractionPointerData data in uiCursorDatas) {
			foreach (InteractionMessagesSender.MessageCameraSetting cameraSetting in messageCameraSettings) {
				bool hit = Sender.UpdateUiCursor (data, cameraSetting);
				if (hit) // 1 cursor get only 1 hit in all camera settings (no penetrate)
					break;
			}
		}
	}

	protected abstract List<InteractionMessagesSender.InteractionPointerData> GetUiCursorDatas ();
}
