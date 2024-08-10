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

[System.Serializable]
public abstract class GameLevelInfoBase : ScriptableObject
{
	public bool active = true;
	private SavableInt completeTimes;

	public bool IsCompleted {
		get { return CompletedTimes.Value > 0; } 
		set {
			if (value)
				CompletedTimes.UpdateMax (1);
			else
				CompletedTimes.Value = 0;
		} 
	}

	public SavableInt CompletedTimes {
		get {
			if (completeTimes == null)
				completeTimes = new SavableInt (SaveKeyPrefix + "complete_times", 0);
			return completeTimes;
		}
	}

	public virtual string SceneName{ get { return this.name; } }

	protected virtual string SaveKeyPrefix{ get { return this.name; } }

	public abstract int LevelNumber{ get; }

	public abstract int WorldNumber{ get; }

	public abstract bool IsUnlocked{ get; }

	public void DeleteSaveData ()
	{
		OnDeleteSaveData ();
		CompletedTimes.Delete ();
	}

	protected abstract void OnDeleteSaveData ();
}
