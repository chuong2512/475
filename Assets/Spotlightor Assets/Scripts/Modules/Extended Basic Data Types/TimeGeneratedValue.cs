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
using System;

public class TimeGeneratedValue
{
	private float generationInterval = 60 * 30;
	private string key = "timed value";

	public float ProgressToNextIntegerValue {
		get {
			float progress = (float)TimeSpanToNextIntegerValue.TotalSeconds / GenerationInterval;
			return 1 - Mathf.Clamp01 (progress);
		}
	}

	public TimeSpan TimeSpanToNextIntegerValue {
		get {
			int nextIntegerValue = Mathf.Max (Mathf.CeilToInt (GeneratedValue), 1);
			TimeSpan timeSpanToNextIntegerValue = TimeSpan.FromSeconds ((double)nextIntegerValue * (double)generationInterval);

			TimeSpan timeSpan = DateTime.Now.Subtract (GenerateStartDateTime);
			return timeSpanToNextIntegerValue.Subtract (timeSpan);
		}
	}

	public float GeneratedValue {
		get {
			float generatedValue = 0;
			if (IsGenerating) {
				TimeSpan timeSpan = DateTime.Now.Subtract (GenerateStartDateTime);
				generatedValue = (float)timeSpan.TotalSeconds / GenerationInterval;
				generatedValue = Mathf.Max (0, generatedValue);
			}
			return generatedValue;
		}
	}

	public float GenerationInterval { 
		get { return generationInterval; } 
		set { generationInterval = Mathf.Max (0.01f, value); }
	}

	public bool IsGenerating{ get { return BasicDataTypeStorage.HasKey (key); } }

	private DateTime GenerateStartDateTime {
		get { return BasicDataTypeStorage.GetDateTimeWithFloatPrecision (key);}
		set { BasicDataTypeStorage.SetDateTimeWithFloatPrecision (key, value);}
	}

	public TimeGeneratedValue (float generationInterval, string key)
	{
		this.key = key;
		GenerationInterval = Mathf.Max (1, generationInterval);
	}

	public void StartGenerating ()
	{
		if (!IsGenerating) {
			GenerateStartDateTime = DateTime.Now;
		}
	}

	public void StopGenerating ()
	{
		BasicDataTypeStorage.DeleteDateTimeWithFlotPrecision (key);
	}

	public void HarvestGeneratedValue (float harvestValue)
	{
		if (IsGenerating) {
			harvestValue = Mathf.Clamp (harvestValue, 0, GeneratedValue);
			double secondsPassed = harvestValue * GenerationInterval;
			GenerateStartDateTime = GenerateStartDateTime.AddSeconds ((double)secondsPassed);
		}
	}
}
