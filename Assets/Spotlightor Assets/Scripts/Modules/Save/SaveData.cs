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

[System.Serializable]
public class SaveData
{
	[System.Serializable]
	public class StringIntDictionary : SerializableDictionary<string, int>
	{
		
	}

	[System.Serializable]
	public class StringFloatDictionary : SerializableDictionary<string, float>
	{

	}

	[System.Serializable]
	public class StringStringDictionary : SerializableDictionary<string, string>
	{

	}

	[SerializeField]
	private StringIntDictionary ints;
	[SerializeField]
	private StringFloatDictionary floats;
	[SerializeField]
	private StringStringDictionary strings;

	public Dictionary<string, int> Ints{ get { return ints.Dictionary; } }

	public Dictionary<string, float> Floats{ get { return floats.Dictionary; } }

	public Dictionary<string, string> Strings{ get { return strings.Dictionary; } }

	public static SaveData Empty {
		get {
			SaveData saveData = new SaveData ();
			saveData.Clear ();
			return saveData;
		}
	}

	public virtual void Clear ()
	{
		JsonUtility.FromJsonOverwrite (JsonUtility.ToJson (new SaveData ()), this);
	}
}
