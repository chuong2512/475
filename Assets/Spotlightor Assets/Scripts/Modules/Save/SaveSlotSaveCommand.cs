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

public class SaveSlotSaveCommand : CoroutineCommandBehavior
{
	public bool autoInstallSaveEngine = true;

	protected override IEnumerator CoroutineCommand ()
	{
		if (HybridSaveEngine.Instance.State != SaveEngine.StateTypes.Installed && autoInstallSaveEngine) {
			this.Log ("Save while notInstalled, autoInstall save engine");
			GameObject loaderGo = new GameObject ("Load Save Data");
			SaveSlotLoadCommand loader = loaderGo.AddComponent<SaveSlotLoadCommand> ();
			yield return StartCoroutine (loader.ExecuteCoroutine ());
		}

		this.Log ("Save game");

		if (HybridSaveEngine.Instance.State == SaveEngine.StateTypes.Installed) {
			SaveSlotsStorage.ActiveSaveSlot.Save ();
			yield return null;
		}
	}
}
