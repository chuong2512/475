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

public class MultiIntValuesEncoder
{
	private class RangedInt
	{
		private int value = 0;

		public int Min{ get; private set; }

		public int Max{ get; private set; }

		public int Value { 
			get{ return this.value;}
			set {
				this.value = Mathf.Clamp (value, Min, Max);
			} 
		}

		public int ZeroBasedValue { 
			get { return Value - Min; } 
			set { Value = Min + value;}
		}

		public int PossibleValuesCount{ get { return Max - Min + 1; } }

		public RangedInt (int min, int max, int value)
		{
			this.Max = max;
			this.Min = Mathf.Min (min, max - 1);
			this.Value = value;
		}
	}
	private List<RangedInt> rangedIntSequence = new List<RangedInt> ();

	public void AddRangedInt (int min, int max, int value)
	{
		rangedIntSequence.Add (new RangedInt (min, max, value));
	}

	public int this [int index] {
		get {
			if (MathAddons.IsInRange (index, 0, rangedIntSequence.Count - 1))
				return rangedIntSequence [index].Value;
			else
				return -1;
		}
		set { 
			if (MathAddons.IsInRange (index, 0, rangedIntSequence.Count - 1))
				rangedIntSequence [index].Value = value;
		}
	}

	public int EncodeToSingleInt ()
	{
		int encodedInt = 0;
		int offset = 1;
		foreach (RangedInt rangedInt in rangedIntSequence) {
			encodedInt += rangedInt.ZeroBasedValue * offset;
			offset *= rangedInt.PossibleValuesCount;
		}
		if (offset < 0)
			Debug.LogError ("MultiIntValuesEncoder max encoded int > int.Max");
		return encodedInt;
	}

	public void DecodeFromSingleInt (int encodedInt)
	{
		int divider = 1;
		rangedIntSequence.ForEach (rangedInt => divider *= rangedInt.PossibleValuesCount);
		
		for (int i = rangedIntSequence.Count-1; i >= 0; i--) {
			RangedInt rangedInt = rangedIntSequence [i];

			divider /= rangedInt.PossibleValuesCount;
			if (divider == 0)
				divider = 1;
			int decodedValue = encodedInt / divider;
			rangedInt.ZeroBasedValue = decodedValue;

			encodedInt -= decodedValue * divider;
		}
	}
}
