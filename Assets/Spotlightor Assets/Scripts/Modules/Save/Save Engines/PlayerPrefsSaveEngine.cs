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

public class PlayerPrefsSaveEngine : SaveEngine
{
	public override void Save (string slotName, string saveDataString)
	{
		OnSaveStarted ();

		// If the string size is too large, maybe we could split the string and store them in a list
		// Windows registry size limit = 1Mb. Ref: http://answers.unity3d.com/questions/693380/what-is-the-bytsize-of-string-properties-in-player.html
		PlayerPrefs.SetString (slotName, saveDataString);
		PlayerPrefs.Save ();

		SlotOperationResult result = new SlotOperationResult (slotName, true);
		OnSaveCompleted (result);
	}

	public override void Load (string slotName)
	{
		string loadedSaveDataString = "";
		SlotOperationResult result;
		if (PlayerPrefs.HasKey (slotName)) {
			loadedSaveDataString = PlayerPrefs.GetString (slotName);
			result = new SlotOperationResult (slotName, true);
		} else {
			result = new SlotOperationResult (slotName, false);
			result.message = "PlayerPrefs doesn't has key: " + slotName;
		}

		OnLoadCompleted (result, loadedSaveDataString);
	}

	public override void Delete (string slotName)
	{
		SlotOperationResult result;
		if (PlayerPrefs.HasKey (slotName)) {
			PlayerPrefs.DeleteKey (slotName);
			result = new SlotOperationResult (slotName, true);
		} else {
			result = new SlotOperationResult (slotName, false);
			result.message = "PlayerPrefs doesn't has key: " + slotName;
		}

		OnDeleteCompleted (result);
	}

	protected override void DoInstall ()
	{
		OnInstallationCompleted (true);
	}

	protected override void DoUninstall ()
	{
		OnUninstallationCompleted (true);
	}
}
