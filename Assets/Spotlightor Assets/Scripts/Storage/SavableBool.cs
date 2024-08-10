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

public class SavableBool : Savable<bool>
{
	public SavableBool (string key, bool deaultValue) : base (key, deaultValue)
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

	protected override bool Load (string key)
	{
		return BasicDataTypeStorage.GetInt (key) > 0;
	}

	protected override void Save (string key, bool value)
	{
		BasicDataTypeStorage.SetInt (key, value == true ? 1 : 0);
	}

	public static implicit operator bool (SavableBool t)
	{
		return t.Value;
	}
}
