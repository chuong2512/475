/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SaveSlotLoadCommand : CoroutineCommandBehavior
{
	public bool autoInstallSaveEngine = true;
	public UnityEvent onSuccess;
	public UnityEvent onFail;
	private bool hasCompleted = false;
	private bool success = false;

	protected override IEnumerator CoroutineCommand ()
	{
		success = hasCompleted = false;

		if (autoInstallSaveEngine && HybridSaveEngine.Instance.State == SaveEngine.StateTypes.NotInstalled) {
			this.Log ("Load while save engine not installed. Auto install");
			HybridSaveEngine.Instance.Install ();
		}

		if (HybridSaveEngine.Instance.State == SaveEngine.StateTypes.Installing) {
			this.Log ("Before load, wait save engine installing...");
			while (HybridSaveEngine.Instance.State == SaveEngine.StateTypes.Installing)
				yield return null;
		}

		if (HybridSaveEngine.Instance.State == SaveEngine.StateTypes.Installed) {
			this.Log ("Loading start");
			SaveSlotsStorage.ActiveSaveSlot.LoadCompleted += HandleSaveSlotLoadCompleted;
			SaveSlotsStorage.ActiveSaveSlot.Load ();

			while (!hasCompleted)
				yield return null;
		} else
			this.Log ("Cannot load save slot because save engine is not installed.");

		this.Log ("Loading command completed. Success = {0}", success);

		if (success) {
			if (onSuccess != null)
				onSuccess.Invoke ();
		} else {
			if (onFail != null)
				onFail.Invoke ();
		}
	}

	void HandleSaveSlotLoadCompleted (SaveSlot saveSlot, bool success, string message)
	{
		this.hasCompleted = true;
		this.success = success;
	}
}
