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

public class SavableInt : Savable<int>
{
	public SavableInt (string key, int defaultValue):base(key, defaultValue)
	{
	}

	protected override void Delete (string key)
	{
		BasicDataTypeStorage.DeleteInt (key);
	}

	protected override bool HasBeenSavedWithKey (string key)
	{
		return BasicDataTypeStorage.HasKey (key);
	}

	protected override int Load (string key)
	{
		return BasicDataTypeStorage.GetInt (key);
	}

	protected override void Save (string key, int value)
	{
		BasicDataTypeStorage.SetInt (key, value);
	}

	public static implicit operator int (SavableInt t)
	{
		return t.Value;
	}

	public void UpdateMax (int value)
	{
		this.Value = Mathf.Max (this.Value, value);
	}
	
	public void UpdateMin (int value)
	{
		this.Value = Mathf.Min (this.Value, value);
	}
}
