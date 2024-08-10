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

public class CappedIntArraySaver
{
	private int maxValue;
	private string key;
	private int[] values;

	public int ArraySize {
		get { return values.Length;}
	}

	public CappedIntArraySaver (int maxValue, int arraySize, string key)
	{
		this.maxValue = maxValue;
		this.key = key;
		values = new int[arraySize];
		
		Load ();
	}
	
	public void Load ()
	{
		int savedValue = BasicDataTypeStorage.GetInt (key);
		
		int divider = 1;
		for (int i = 0; i < ArraySize-1; i++)
			divider *= maxValue + 1;
		for (int i = 0; i < ArraySize; i++) {
			int decodedValue = savedValue / divider;
			values [ArraySize - i - 1] = decodedValue;
			
			savedValue -= decodedValue * divider;
			divider /= maxValue + 1;
		}
	}

	public int GetValueAt (int index)
	{
		return values [Mathf.Clamp (index, 0, ArraySize - 1)];
	}
	
	public void SetValueAt (int index, int newValue)
	{
		values [Mathf.Clamp (index, 0, ArraySize - 1)] = Mathf.Clamp (newValue, 0, maxValue);
	}
	
	public void Save ()
	{
		int saveValue = 0;
		int offset = 1;
		for (int i = 0; i < values.Length; i++) {
			saveValue += values [i] * offset;
			offset *= maxValue + 1;
		}
		BasicDataTypeStorage.SetInt (key, saveValue);
	}

	public void Clear ()
	{
		for (int i = 0; i < values.Length; i++)
			values [i] = 0;

		BasicDataTypeStorage.DeleteInt (key);
	}
}
