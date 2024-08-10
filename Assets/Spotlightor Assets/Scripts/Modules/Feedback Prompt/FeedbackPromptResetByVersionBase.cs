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

public abstract class FeedbackPromptResetByVersionBase : MonoBehaviour
{
	private const string LastVersionSaveKey = "fdbk_last_version";
	public List<FeedbackPrompt> prompts;

	protected abstract string CurrentVersion{ get; }

	void Start ()
	{
		string lastVersion = BasicDataTypeStorage.GetString (LastVersionSaveKey);
		if (lastVersion != CurrentVersion) {
			prompts.ForEach (p => p.Reset ());
			this.Log ("FeedbackPrompt reset because current version [{0}] != last version [{1}]", CurrentVersion, lastVersion);
			BasicDataTypeStorage.SetString (LastVersionSaveKey, CurrentVersion);
		}
	}
}
